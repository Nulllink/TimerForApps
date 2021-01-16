namespace TimerForApps
{
    partial class Lists
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lists));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.whiteListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.timer1 = new System.Timers.Timer();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 684);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(631, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 20);
            this.toolStripStatusLabel1.Text = "Event";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.whiteListToolStripMenuItem, this.blackListToolStripMenuItem, this.controlListToolStripMenuItem, this.writeToolStripMenuItem, this.addAppToolStripMenuItem, this.deleteToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(631, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // whiteListToolStripMenuItem
            // 
            this.whiteListToolStripMenuItem.Name = "whiteListToolStripMenuItem";
            this.whiteListToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
            this.whiteListToolStripMenuItem.Text = "White List";
            this.whiteListToolStripMenuItem.Click += new System.EventHandler(this.whiteListToolStripMenuItem_Click);
            // 
            // blackListToolStripMenuItem
            // 
            this.blackListToolStripMenuItem.Name = "blackListToolStripMenuItem";
            this.blackListToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.blackListToolStripMenuItem.Text = "Black List";
            this.blackListToolStripMenuItem.Click += new System.EventHandler(this.whiteListToolStripMenuItem_Click);
            // 
            // controlListToolStripMenuItem
            // 
            this.controlListToolStripMenuItem.Name = "controlListToolStripMenuItem";
            this.controlListToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.controlListToolStripMenuItem.Text = "Control List";
            this.controlListToolStripMenuItem.Click += new System.EventHandler(this.whiteListToolStripMenuItem_Click);
            // 
            // writeToolStripMenuItem
            // 
            this.writeToolStripMenuItem.Name = "writeToolStripMenuItem";
            this.writeToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.writeToolStripMenuItem.Text = "Write";
            this.writeToolStripMenuItem.Click += new System.EventHandler(this.writeToolStripMenuItem_Click);
            // 
            // addAppToolStripMenuItem
            // 
            this.addAppToolStripMenuItem.Name = "addAppToolStripMenuItem";
            this.addAppToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.addAppToolStripMenuItem.Text = "Add App";
            this.addAppToolStripMenuItem.Click += new System.EventHandler(this.addAppToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.columnHeader1});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(0, 28);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(631, 656);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "App Name";
            this.columnHeader1.Width = 504;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // Lists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 709);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Lists";
            this.Text = "Lists";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Lists_FormClosed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Timers.Timer timer1;

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripMenuItem whiteListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAppToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem controlListToolStripMenuItem;
    }
}