using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace DeckServer
{
    public delegate void Connstat(int Connstat);
    public delegate string MsgRecv();
    public delegate void MsgSend(string S);
    public delegate void MakeLog(string S);
    public delegate void UpdateLog(string S, int index = -1);
    public static class SocketHandler
    {
        public const int _EP_PORT = 8000;
        public const int _CONN_STAT_NC = 0;
        public const int _CONN_STAT_CONN = 1;
        public const int _CONN_STAT_TRY = 2;
        public const int _CONN_STAT_ERR = -1;
        public static string MyIPAddr;
        public static string ClIPAddr;
        static IPAddress IPtest;
        static byte[] RcvBuffer;
        static byte[] SndBuffer;
        static Socket socket_sv;
        static Socket socket_cl;
        static string in_msg;
        static string out_msg;
        static int RecvBytes;
        static int SendBytes;
        static Stopwatch SWatch;
        public static string EMessage;
        public static float ConnectionTime = 0;
        public static Thread svThread;
        public static Thread svConnThread;
        public static int connStat;
        public static bool Abort = false;
        public static event Connstat ConnStatNotify;
        public static event MsgRecv MsgRecvNotify;
        public static event MakeLog SendLog;
        public static event UpdateLog UpdLog;
        public static void GetMyIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    MyIPAddr = ip.ToString();
                    break;
                }
            }
        }
        public static bool IsIPValid()
        {
            return IPAddress.TryParse(MyIPAddr, out IPtest);
        }
        public static async void Connect()
        {
            if (!IsIPValid())
            {
                Program.ShowError("입력한 값은 유효한 IPv4 주소가 아닙니다!");
                return;
            }
            SendLog?.Invoke("연걸 쓰레드 실행 | " + string.Format(" 포트 [ {0,5:00000} ]", _EP_PORT));
            ConnStatNotify?.Invoke(_CONN_STAT_TRY);
            await Task.Run(() => ConnectAsync());
            ConnStatNotify?.Invoke(connStat);
            if (connStat == _CONN_STAT_CONN)
            {
                ClIPAddr = socket_cl.RemoteEndPoint.ToString();
                SendLog?.Invoke("연결 수락 @ " + socket_cl.RemoteEndPoint);
                ConnStatNotify?.Invoke(connStat);
                //Program.StartScan();
                await Task.Run(() => Read());
            }
            if (EMessage != null)
            {
                if (EMessage.Length > 0 && !Abort)
                {
                    Program.ShowError(EMessage);
                    SendLog?.Invoke(EMessage);
                    EMessage = "";
                    Disconnect();
                }
            }
            ConnStatNotify?.Invoke(connStat);
        }
        static void ResetBuffer()
        {
            for (int i = 0; i < RcvBuffer.Length; i++)
                RcvBuffer[i] = 0;
            for (int i = 0; i < SndBuffer.Length; i++)
                SndBuffer[i] = 0;
        }
        public static void Kill()
        {/*
            if (svThread != null)
                svThread.Abort();
            if (svConnThread != null)
                svConnThread.Abort();*/
        }
        public static void ConnectAsync()
        {
            if (connStat == _CONN_STAT_CONN)
                return;
            connStat = _CONN_STAT_TRY;
            Thread.Sleep(10);
            SWatch = Stopwatch.StartNew();
            RcvBuffer = new byte[1500];
            SndBuffer = new byte[1500];
            ResetBuffer();
            socket_sv = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(MyIPAddr), _EP_PORT);
            try
            {
                socket_sv.Bind(ep);
                socket_sv.Listen(1);
                socket_sv.BeginAccept(new AsyncCallback(AcceptCallBack), socket_sv);
                while (SWatch.Elapsed <= TimeSpan.FromSeconds(600) &&
                    connStat == _CONN_STAT_TRY)
                {
                    if (SWatch.ElapsedMilliseconds % 100 == 0)
                    {
                        ConnectionTime = SWatch.ElapsedMilliseconds / 1000f;
                    }
                    if (Abort)
                    {
                        Abort = false;
                        throw new OperationCanceledException("사용자에 의한 취소");           
                    }   
                }
                if (connStat != _CONN_STAT_CONN && SWatch.Elapsed >= TimeSpan.FromSeconds(10))
                {
                    throw new TimeoutException();
                }
            }
            catch (Exception E)
            {
                if (E.GetType() == typeof(OperationCanceledException))
                    connStat = _CONN_STAT_NC;
                else
                {
                    connStat = _CONN_STAT_ERR;
                    EMessage = E.Message;
                }
                return;
            }
            finally
            {
            }
        }
        public static void AcceptCallBack(IAsyncResult AR)
        {
            try
            {
                if (connStat != _CONN_STAT_TRY)
                    return;
                Socket Listener = (Socket)AR.AsyncState;
                socket_cl = Listener.EndAccept(AR);
                connStat = _CONN_STAT_CONN;
            }
            catch (Exception E)
            {
            }
        }
        public static void Disconnect()
        {
            if (connStat == _CONN_STAT_NC ||
                connStat == _CONN_STAT_ERR)
                return;
            Send(Program._GOODBYE);
            if (socket_cl != null)
            {
                socket_cl.Close();
                socket_cl.Dispose();
            }
            if (socket_sv != null)
            {
                socket_sv.Close();
                socket_sv.Dispose();
            }
            connStat = _CONN_STAT_NC;
        }
        public static void Read()
        {
            while (socket_cl.Connected)
            {
                try
                {
                    RecvBytes = socket_cl.Receive(RcvBuffer);
                    if (RecvBytes > 0)
                    {
                        in_msg = Encoding.UTF8.GetString(RcvBuffer);
#if DEBUG
                        Console.WriteLine(RecvBytes + " Bytes Recv : " + in_msg);
#endif
                        Program.CommandInterpret(in_msg);
                        ResetBuffer();
                    }
                }
                catch(Exception E)
                {
                    if (!Abort)
                    {
                        Program.ShowError(E.Message);
                        EMessage = E.Message;
                    }
                    break;
                }
            }
            Disconnect();
        }
        public static void Send(string message)
        {
            if (socket_cl == null)
                return;
            if (connStat != _CONN_STAT_CONN)
                return;
            ResetBuffer();
            SndBuffer = Encoding.UTF8.GetBytes(message);
            try
            {
                socket_cl.Send(SndBuffer);
            }
            catch(SocketException E)
            {
                connStat = _CONN_STAT_ERR;
                Disconnect();
                Program.ShowError(E.Message);
            }
        }
    }
}
