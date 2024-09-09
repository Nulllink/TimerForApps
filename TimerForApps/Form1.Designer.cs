namespace TimerForApps
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listView1 = new System.Windows.Forms.ListView();
            this.AppName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsFinderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.listsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.killToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.inBlackListToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AppName,
            this.Time,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(0, 24);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(461, 465);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // AppName
            // 
            this.AppName.Tag = "AppName";
            this.AppName.Text = "AppName";
            this.AppName.Width = 204;
            // 
            // Time
            // 
            this.Time.Tag = "Time";
            this.Time.Text = "Time";
            this.Time.Width = 58;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ProcName";
            this.columnHeader2.Width = 125;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "OpenTime";
            this.columnHeader3.Width = 68;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipTitle = "Opened";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "TimerForApps";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_BalloonTipShown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.processToolStripMenuItem,
            this.saveLogsToolStripMenuItem,
            this.searchToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(461, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logsFinderToolStripMenuItem1,
            this.listsToolStripMenuItem1,
            this.settingsToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // logsFinderToolStripMenuItem1
            // 
            this.logsFinderToolStripMenuItem1.Name = "logsFinderToolStripMenuItem1";
            this.logsFinderToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
            this.logsFinderToolStripMenuItem1.Text = "Logs Finder";
            this.logsFinderToolStripMenuItem1.Click += new System.EventHandler(this.LogsFinderToolStripMenuItem_Click);
            // 
            // listsToolStripMenuItem1
            // 
            this.listsToolStripMenuItem1.Name = "listsToolStripMenuItem1";
            this.listsToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
            this.listsToolStripMenuItem1.Text = "Lists";
            this.listsToolStripMenuItem1.Click += new System.EventHandler(this.listsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.settingsToolStripMenuItem.Text = "Settings Info About";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem1,
            this.killToolStripMenuItem1,
            this.inBlackListToolStripMenuItem1});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // killToolStripMenuItem1
            // 
            this.killToolStripMenuItem1.Name = "killToolStripMenuItem1";
            this.killToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.killToolStripMenuItem1.Text = "Kill";
            this.killToolStripMenuItem1.Click += new System.EventHandler(this.killToolStripMenuItem_Click);
            // 
            // inBlackListToolStripMenuItem1
            // 
            this.inBlackListToolStripMenuItem1.Name = "inBlackListToolStripMenuItem1";
            this.inBlackListToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.inBlackListToolStripMenuItem1.Text = "In black list";
            this.inBlackListToolStripMenuItem1.Click += new System.EventHandler(this.inBlackListToolStripMenuItem_Click);
            // 
            // saveLogsToolStripMenuItem
            // 
            this.saveLogsToolStripMenuItem.Name = "saveLogsToolStripMenuItem";
            this.saveLogsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.saveLogsToolStripMenuItem.Text = "SaveLogs";
            this.saveLogsToolStripMenuItem.Click += new System.EventHandler(this.saveLogsToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel6});
            this.statusStrip1.Location = new System.Drawing.Point(0, 489);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(461, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel2.Text = "0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel1.Text = "0:0:0";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLabel4.Text = "SaveLogs: True";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(74, 17);
            this.toolStripStatusLabel3.Text = "Search: False";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(36, 17);
            this.toolStripStatusLabel6.Text = "Event";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 5000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 511);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "TimerForApps 12.9";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader AppName;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveLogsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsFinderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem listsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem killToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem inBlackListToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
    }
}

