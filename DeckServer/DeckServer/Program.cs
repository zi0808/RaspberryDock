// ------------------------------------------------------
// 2013112501 김용현 Yonghyun Kim
// Deck Server Application (Win32/C#)
// ------------------------------------------------------
// Please note that Comments are written in English.
// 주석은 영어로 작성되어있습니다.
// ------------------------------------------------------
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace DeckServer
{
    using HWND = IntPtr;
    internal static class NativeMethods
    {
        // http://msdn.microsoft.com/en-us/library/ms681944(VS.85).aspx
        /// <summary>
        /// Allocates a new console for the calling process.
        /// </summary>
        /// <returns>nonzero if the function succeeds; otherwise, zero.</returns>
        /// <remarks>
        /// A process can be associated with only one console,
        /// so the function fails if the calling process already has a console.
        /// </remarks>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int AllocConsole();

        // http://msdn.microsoft.com/en-us/library/ms683150(VS.85).aspx
        /// <summary>
        /// Detaches the calling process from its console.
        /// </summary>
        /// <returns>nonzero if the function succeeds; otherwise, zero.</returns>
        /// <remarks>
        /// If the calling process is not already attached to a console,
        /// the error code returned is ERROR_INVALID_PARAMETER (87).
        /// </remarks>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int FreeConsole();
    }
    /// <summary>Contains functionality to get all the open windows.</summary>
    public static class USR32WIN
    {
        static SoundPlayer AppSound = new SoundPlayer();
        public static IDictionary<HWND, string> MyWindows;
        public static int CurrentProc = -1;
        public static HWND HW_CurrentProc;
        /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
        /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
        public static void UpdateOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();
            List<HWND> remlist = new List<HWND>();
            while (SocketHandler.connStat == SocketHandler._CONN_STAT_CONN)
            {
                EnumWindows(delegate (HWND hWnd, int lParam)
                {
                    if (hWnd == shellWindow) return true;
                    if (!IsWindowVisible(hWnd)) return true;

                    int length = GetWindowTextLength(hWnd);
                    if (length == 0) return true;

                    StringBuilder builder = new StringBuilder(length);
                    GetWindowText(hWnd, builder, length + 1);

                    windows[hWnd] = builder.ToString();
                    return true;

                }, 0);
                if (MyWindows == null)
                {
                    MyWindows = new Dictionary<HWND, string>();
                }
                foreach (HWND key in windows.Keys)
                {
                    if (!MyWindows.ContainsKey(key) &&
                        Exists(key))
                    {
                        string wname = windows[key];
                        MyWindows.Add(key, wname);
                        Program.AppUpdate(key, wname, true);
#if DEBUG
                        Console.WriteLine("Added [KEY {0}] [TITLE {1}]", key, wname);
#endif
                        Thread.Sleep(10);
                    }
                }
                foreach (HWND key in MyWindows.Keys)
                {
                    if (!Exists(key))
                    {
                        string wname = windows[key];
                        remlist.Add(key);
                        Program.AppUpdate(key, wname, false);
#if DEBUG
                        Console.WriteLine("Removed [KEY {0}] [TITLE {1}]", key, wname);
#endif
                        Thread.Sleep(10);
                    }
                }
                foreach (HWND rem in remlist)
                    MyWindows.Remove(rem);
                remlist.Clear();
            }
#if DEBUG
            // Debug Function : Display Enumerated Windows to VS IDE Output
            foreach (KeyValuePair<HWND, string> W in MyWindows)
            {
                Console.WriteLine(string.Format("[KEY {0}] [TITLE {1}]\n", W.Key, W.Value));
            }
#endif 
        }
        public static void UpdateCurrentWin(HWND handle)
        {
            HW_CurrentProc = handle;
        }
        /// <summary>
        /// Operations with Win32 Window.
        /// </summary>
        /// <param name="CMD">Command to send (see 'Program' class)</param>
        /// <param name="Target">IntPtr of the Window</param>
        public static void WinOP(string CMD, HWND Target)
        {
            KeyValuePair<HWND, string> myTarget = MyWindows.FirstOrDefault(x => x.Key == Target);
            switch(CMD)
            {
                case Program._CMD_W_CLOSE:
                    SendMessage(myTarget.Key, WM_CLOSE, HWND.Zero, HWND.Zero);
                    Program.AppUpdate(myTarget.Key, myTarget.Value, false);
                    break;
                case Program._CMD_W_MAX:
                    ShowWindow(Target, SW_MAXIMIZE);
                    break;
                case Program._CMD_W_MIN:
                    ShowWindow(Target, SW_MINIMIZE);
                    break;
            }
        }
        /// <summary>
        /// Operations with Win32 Window. (with name string)
        /// </summary>
        /// <param name="CMD">Command to send (see 'Program' class)</param>
        /// <param name="Target">Target Window's Name</param>
        public static void WinOP(string CMD, string Target)
        {
            WinOP(CMD, FindWithName(Target));
        }
        public static HWND FindWithName(string N, bool Explicit = false)
        {
            if (Explicit)
                return MyWindows.FirstOrDefault(x => x.Value == N).Key;
            else
                return MyWindows.FirstOrDefault(x => x.Value.Contains(N)).Key;
        }
        /// <summary>
        /// Media Related Control Operations.
        /// </summary>
        /// <param name="CMD">Media Commands to Execute</param>
        public static void MediaOP(string CMD)
        {
            Console.WriteLine(CMD);
            uint msg = 0;
            switch(CMD)
            {
                case Program._CMD_MUL_VOL_MUTE:
                    msg = APPCOMMAND_VOLUME_MUTE;
                    break;
                case Program._CMD_MUL_VOL_DN:
                    msg = APPCOMMAND_VOLUME_DOWN;
                    break;
                case Program._CMD_MUL_VOL_UP:
                    msg = APPCOMMAND_VOLUME_UP;
                    break;
                case Program._CMD_MUL_PB_NEXT:
                    msg = APPCOMMAND_MEDIA_NEXTTRACK;
                    break;
                case Program._CMD_MUL_PB_PAUSE:
                    msg = APPCOMMAND_MEDIA_PLAY_PAUSE;
                    break;
                case Program._CMD_MUL_PB_PREV:
                    msg = APPCOMMAND_MEDIA_PREVIOUSTRACK;
                    break;
            }
            if (msg != 0)
                SendMessageW(HW_CurrentProc, WM_APPCOMMAND, HW_CurrentProc,
                    (HWND)msg);
        }
        public static bool Exists(HWND window)
        {
            return IsWindow(window);
        }

        private const int WM_APPCOMMAND = 0x319;
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_MEDIA_PLAY_PAUSE = 0xE0000;
        private const int APPCOMMAND_MEDIA_NEXTTRACK = 0xB0000;
        private const int APPCOMMAND_MEDIA_PREVIOUSTRACK = 0xC0000;
        private const int APPCOMMAND_MEDIA_STOP = 0xD0000;
        private const uint WM_CLOSE = 0x0010;
        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;
        private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        // Keyboard Simulation
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
   UIntPtr dwExtraInfo);
        // Enumerate Current Windows
        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);
        // Get Window Title
        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);
        // Get Length of Window's Text
        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);
        // Is WIndow Visible ?
        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(HWND hWnd);
        [DllImport("USER32.DLL")]
        private static extern bool IsIconic(HWND hWnd);
        // Send WM Messages
        [DllImport("USER32.DLL")]
        private static extern HWND SendMessage(HWND hWnd, uint Msg, HWND wParam, HWND lParam);
        // Send AppCommands
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(HWND hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindow(IntPtr hWnd);

    }
    static class Program
    {
        // Application Specific
        public const string _APP_SYNC_SIGNAL = "appsync";
        public const string _APP_ADD = "appadd";
        public const string _APP_REM = "apprem";
        public const string _GOODBYE = "byebye";
        public const string _APP_SYNC_END = "appsync_e ";
        // Win32 OS Commands ( Op and Arg )
        public const string _CMD_W_CLOSE = "wclose";
        public const string _CMD_W_MAX = "wmax";
        public const string _CMD_W_MIN = "wmin";
        public const string _CMD_RUN = "run";
        // Multimedia Commands ( Op Only )
        public const string _CMD_MUL_VOL_UP = "mvup";
        public const string _CMD_MUL_VOL_DN = "mvdn";
        public const string _CMD_MUL_VOL_MUTE = "mmute";
        public const string _CMD_MUL_PB_PAUSE = "mpbpause";
        public const string _CMD_MUL_PB_NEXT = "mpbnxt";
        public const string _CMD_MUL_PB_PREV = "mpbprev";
        public const string _CMD_MUL_PB_STOP = "mpbstop";
        // Hotkey Command ( Op And Arg(s) )
        public const string _CMD_HOTKEY = "hotk";
        // .Net Key Codes
        // Keycodes (Special Keys )
        public const string _KEYC_CTRL = "^";
        public const string _KEYC_SHIFT = "+";
        public const string _KEYC_ALT = "&";
        public const string _KEYC_NUMLK = "{NUMLOCK}";
        public const string _KEYC_LEFT = "{LEFT}";
        public const string _KEYC_RIGHT = "{RIGHT}";
        public const string _KEYC_UP = "{UP}";
        public const string _KEYC_DOWN = "{DOWN}";
        public const string _KEYC_INS = "{INSERT}";
        // Predefined Key Combinations
        public const string _HOTK_UNDO = "^Z";
        public const string _HOTK_CUT = "^X";
        public const string _HOTK_COPY = "^C";
        public const string _HOTK_PASTE = "^V";
        public const string _HOTK_TASKMAN = "^&{DEL}";
        public static string _KEYC_FUNC(uint FUNCNUM)
        {
            return "{F" + FUNCNUM.ToString() + "}";
        }
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void KillThreads()
        {
            
        }
        public static void ShowError(string ErrMsg)
        {
            MessageBox.Show(ErrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void AppUpdate(HWND Ptr, string appname, bool add)
        {
            string cmdstring = string.Format("{0} {1} {2}", add ? _APP_ADD :
                _APP_REM, Ptr, appname);
            SocketHandler.Send(cmdstring);
        }
        public static void AppSync()
        {
            foreach(KeyValuePair<HWND,string> HK in USR32WIN.MyWindows)
            {
                string cmdstring = string.Format("{0} {1} {2}", _APP_ADD,
                    HK.Key, HK.Value);
                SocketHandler.Send(cmdstring);
                Thread.Sleep(10);
            }
            SocketHandler.Send(_APP_SYNC_END);
        }

        public static async void StartScan()
        {
            await Task.Run(() => USR32WIN.UpdateOpenWindows());
        }

        public static void CommandInterpret(string Command)
        {
            /* Command Format
             * [operation] [target] [arguments . . .]
             *  operation : basic operation command ( run, close, etc )
             *  target : operation target (HWND, Path)
             *  arguments : additional arguments
             */
            string[] CommandSplit = Command.Split(' ');
            // Operation
            string OpCode = CommandSplit[0];
            switch (OpCode)
            {
                case _CMD_MUL_VOL_MUTE:
                case _CMD_MUL_VOL_UP:
                case _CMD_MUL_VOL_DN:
                case _CMD_MUL_PB_NEXT:
                case _CMD_MUL_PB_PAUSE:
                case _CMD_MUL_PB_PREV:
                    USR32WIN.MediaOP(OpCode);
                    break;
                case _APP_SYNC_SIGNAL:
                    StartScan();
                    break;
                case _GOODBYE:
                    SocketHandler.Disconnect();
                    Application.Exit();
                    break;
            }
            if (CommandSplit.Length < 2)
                return;
            // If Command has 2 or more splits
            string Targ = CommandSplit[1];
            string Arg = CommandSplit.Length > 2 ? " " + CommandSplit[2] : "";
            int ParsedTarg;
            bool ParseSuccess;
            ParseSuccess = int.TryParse(Targ, out ParsedTarg);
                
            switch (OpCode)
            {
                case _CMD_RUN:
                    Process.Start(Targ, Arg);
                    break;
                case _CMD_W_CLOSE:
                case _CMD_W_MAX:
                case _CMD_W_MIN:
                    if (ParseSuccess)
                        USR32WIN.WinOP(OpCode, (HWND)ParsedTarg);
                    else
                        USR32WIN.WinOP(OpCode, Targ);
                    break;
                case _CMD_HOTKEY:
                    SendKeys.SendWait(Targ);
                    break;
            }
            // Argument
            if (CommandSplit.Length < 3)
                return;

        }
    }
}
