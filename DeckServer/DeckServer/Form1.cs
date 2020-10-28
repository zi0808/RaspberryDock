using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DeckServer
{

    public partial class Form1 : Form
    {
        Stopwatch StopWatchTimer;
        Color CError = new Color();

        public Form1()
        {
            CError = Color.FromArgb(255, 100, 100);
            InitializeComponent();
            SocketHandler.ConnStatNotify += SocketHandler_ConnStatNotify;
            SocketHandler.SendLog += SocketHandler_SendLog;
            SocketHandler.UpdLog += SocketHandler_UpdLog;
            SocketHandler.GetMyIP();
            Text_MyIP.Text = SocketHandler.MyIPAddr;
            List_Logs.Items.Clear();
        }
        void ConnectFromHere()
        {
            Text_MyIP.Enabled = false;
            SocketHandler.Connect();
        }
        private void SocketHandler_UpdLog(string S, int index = -1)
        {
            if (index == -1)
                List_Logs.Items[List_Logs.Items.Count - 1] = S;
            else
                List_Logs.Items[index] = S;
            ListScrollUpdate();
            List_Logs.Refresh();
        }

        private void SocketHandler_SendLog(string S)
        {
            string _my_s = string.Copy(S);
            List_Logs.Items.Add(_my_s);
            ListScrollUpdate();
            List_Logs.Refresh();
        }

        private void SocketHandler_ConnStatNotify(int Connstat)
        {
            Object RM = null;
            
            switch(Connstat)
            {
                case SocketHandler._CONN_STAT_CONN:
                    Label_Stat.Text = "연결됨";
                    BtnconnectToggle.Text = "연결 해제";
                    Label_ClientIP.Text = SocketHandler.ClIPAddr;
                    RM = Properties.Resources.ResourceManager.GetObject("Rpi_Icon");
                    BtnClearLogs.Enabled = true;
                    BtnconnectToggle.Enabled = true;
                    break;
                case SocketHandler._CONN_STAT_TRY:
                    Label_Stat.Text = "연결 중...";
                    BtnconnectToggle.Text = "중단";
                    RM = Properties.Resources.ResourceManager.GetObject("Rpi_Icon_BW");
                    BtnClearLogs.Enabled = false;
                    break;
                case SocketHandler._CONN_STAT_NC:
                case SocketHandler._CONN_STAT_ERR:
                    Label_Stat.Text = "연결되지 않음";
                    BtnconnectToggle.Text = "연결";
                    RM = Properties.Resources.ResourceManager.GetObject("Rpi_Icon_BW");
                    Text_MyIP.Enabled = true;
                    BtnClearLogs.Enabled = true;
                    BtnconnectToggle.Enabled = true;
                    break;
            }
            Bitmap IconIM = (Bitmap)RM;
            rpiPic.Image = IconIM;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnconnectToggle_Click(object sender, EventArgs e)
        {
            switch(SocketHandler.connStat)
            {
                case SocketHandler._CONN_STAT_ERR:
                case SocketHandler._CONN_STAT_NC:
                    ConnectFromHere();
                    break;
                case SocketHandler._CONN_STAT_CONN:
                    SocketHandler.Abort = true;
                    SocketHandler.Disconnect();
                    break;
                case SocketHandler._CONN_STAT_TRY:
                    SocketHandler.Abort = true;
                    SocketHandler.Disconnect();
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List_Logs.Items.Clear();
        }
        
        void ListScrollUpdate()
        {
            int visibleItems = List_Logs.ClientSize.Height / List_Logs.ItemHeight;
            List_Logs.TopIndex = Math.Max(List_Logs.Items.Count - visibleItems + 1, 0);
        }

        private void List_Logs_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            USR32WIN.UpdateCurrentWin(Handle);
            ConnectFromHere();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && SocketHandler.connStat == SocketHandler._CONN_STAT_TRY)
            {
                SocketHandler.Abort = true;
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void commandInterpreterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void processListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string command = Program._CMD_W_CLOSE + " " + 
                "카카오톡";
            Program.CommandInterpret(command);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string command = Program._CMD_RUN + " " +
                "cmd";
            Program.CommandInterpret(command);
        }

        private void MuteButton_Click(object sender, EventArgs e)
        {
            USR32WIN.UpdateCurrentWin(Handle);
            string command = Program._CMD_MUL_VOL_MUTE;
            Program.CommandInterpret(command);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SocketHandler.Kill();
        }

        private void Label_ServerIP_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Text_MyIP_TextChanged(object sender, EventArgs e)
        {
            SocketHandler.MyIPAddr = Text_MyIP.Text;
            if (SocketHandler.IsIPValid())
                Text_MyIP.BackColor = Color.White;
            else
                Text_MyIP.BackColor = CError;
        }

        private void ApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void PictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
