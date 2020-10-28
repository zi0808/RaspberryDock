namespace DeckServer
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label_Stat = new System.Windows.Forms.Label();
            this.Group_Logs = new System.Windows.Forms.GroupBox();
            this.List_Logs = new System.Windows.Forms.ListBox();
            this.BtnconnectToggle = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Text_MyIP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Label_ClientIP = new System.Windows.Forms.Label();
            this.BtnClearLogs = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandInterpreterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rpiPic = new System.Windows.Forms.PictureBox();
            this.Group_Logs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rpiPic)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_Stat
            // 
            this.Label_Stat.AutoSize = true;
            this.Label_Stat.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Stat.Location = new System.Drawing.Point(23, 27);
            this.Label_Stat.Name = "Label_Stat";
            this.Label_Stat.Size = new System.Drawing.Size(322, 26);
            this.Label_Stat.TabIndex = 1;
            this.Label_Stat.Text = "CONNECTION STATUS HERE";
            this.Label_Stat.Click += new System.EventHandler(this.Label1_Click);
            // 
            // Group_Logs
            // 
            this.Group_Logs.Controls.Add(this.List_Logs);
            this.Group_Logs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Group_Logs.Location = new System.Drawing.Point(18, 246);
            this.Group_Logs.Name = "Group_Logs";
            this.Group_Logs.Size = new System.Drawing.Size(382, 237);
            this.Group_Logs.TabIndex = 3;
            this.Group_Logs.TabStop = false;
            this.Group_Logs.Text = "기록";
            // 
            // List_Logs
            // 
            this.List_Logs.FormattingEnabled = true;
            this.List_Logs.ItemHeight = 20;
            this.List_Logs.Location = new System.Drawing.Point(10, 24);
            this.List_Logs.Name = "List_Logs";
            this.List_Logs.Size = new System.Drawing.Size(358, 204);
            this.List_Logs.TabIndex = 0;
            this.List_Logs.SelectedIndexChanged += new System.EventHandler(this.List_Logs_SelectedIndexChanged);
            // 
            // BtnconnectToggle
            // 
            this.BtnconnectToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnconnectToggle.Location = new System.Drawing.Point(18, 203);
            this.BtnconnectToggle.Name = "BtnconnectToggle";
            this.BtnconnectToggle.Size = new System.Drawing.Size(157, 37);
            this.BtnconnectToggle.TabIndex = 4;
            this.BtnconnectToggle.Text = "CONNECT";
            this.BtnconnectToggle.UseVisualStyleBackColor = true;
            this.BtnconnectToggle.Click += new System.EventHandler(this.BtnconnectToggle_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Text_MyIP);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(18, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 63);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "서버 (Win32)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "IP |";
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // Text_MyIP
            // 
            this.Text_MyIP.Location = new System.Drawing.Point(68, 29);
            this.Text_MyIP.Name = "Text_MyIP";
            this.Text_MyIP.Size = new System.Drawing.Size(300, 26);
            this.Text_MyIP.TabIndex = 0;
            this.Text_MyIP.TextChanged += new System.EventHandler(this.Text_MyIP_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rpiPic);
            this.groupBox2.Controls.Add(this.Label_ClientIP);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(18, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(382, 63);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "클라이언트 (RPi)";
            // 
            // Label_ClientIP
            // 
            this.Label_ClientIP.AutoSize = true;
            this.Label_ClientIP.Font = new System.Drawing.Font("Arial Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_ClientIP.Location = new System.Drawing.Point(6, 17);
            this.Label_ClientIP.Name = "Label_ClientIP";
            this.Label_ClientIP.Size = new System.Drawing.Size(280, 33);
            this.Label_ClientIP.TabIndex = 2;
            this.Label_ClientIP.Text = "IP | 000.000.000.000";
            // 
            // BtnClearLogs
            // 
            this.BtnClearLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearLogs.Location = new System.Drawing.Point(185, 203);
            this.BtnClearLogs.Name = "BtnClearLogs";
            this.BtnClearLogs.Size = new System.Drawing.Size(215, 37);
            this.BtnClearLogs.TabIndex = 8;
            this.BtnClearLogs.Text = "기록 지우기";
            this.BtnClearLogs.UseVisualStyleBackColor = true;
            this.BtnClearLogs.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 486);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "RASPBERRY DOCK | 2013112501 김용현";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(410, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MenuStrip1_ItemClicked);
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processListToolStripMenuItem,
            this.commandInterpreterToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.applicationToolStripMenuItem.Text = "부가 기능";
            this.applicationToolStripMenuItem.Click += new System.EventHandler(this.ApplicationToolStripMenuItem_Click);
            // 
            // processListToolStripMenuItem
            // 
            this.processListToolStripMenuItem.Name = "processListToolStripMenuItem";
            this.processListToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.processListToolStripMenuItem.Text = "프로세스 목록";
            this.processListToolStripMenuItem.Click += new System.EventHandler(this.processListToolStripMenuItem_Click);
            // 
            // commandInterpreterToolStripMenuItem
            // 
            this.commandInterpreterToolStripMenuItem.Name = "commandInterpreterToolStripMenuItem";
            this.commandInterpreterToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.commandInterpreterToolStripMenuItem.Text = "명령 해석기";
            this.commandInterpreterToolStripMenuItem.Click += new System.EventHandler(this.commandInterpreterToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exitToolStripMenuItem.Text = "프로그램 종료";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // rpiPic
            // 
            this.rpiPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rpiPic.Image = global::DeckServer.Properties.Resources.Rpi_Icon_BW;
            this.rpiPic.InitialImage = global::DeckServer.Properties.Resources.Rpi_Icon;
            this.rpiPic.Location = new System.Drawing.Point(300, 13);
            this.rpiPic.Name = "rpiPic";
            this.rpiPic.Size = new System.Drawing.Size(76, 44);
            this.rpiPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rpiPic.TabIndex = 11;
            this.rpiPic.TabStop = false;
            this.rpiPic.Click += new System.EventHandler(this.PictureBox1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 517);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnClearLogs);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BtnconnectToggle);
            this.Controls.Add(this.Group_Logs);
            this.Controls.Add(this.Label_Stat);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "RASPBERRY DOCK SERVER";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Group_Logs.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rpiPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label_Stat;
        private System.Windows.Forms.GroupBox Group_Logs;
        private System.Windows.Forms.ListBox List_Logs;
        private System.Windows.Forms.Button BtnconnectToggle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label Label_ClientIP;
        private System.Windows.Forms.Button BtnClearLogs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandInterpreterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Text_MyIP;
        private System.Windows.Forms.PictureBox rpiPic;
    }
}

