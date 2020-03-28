namespace CursorControl
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnRun = new System.Windows.Forms.Button();
            this.lblCursor = new System.Windows.Forms.Label();
            this.lblExpendTime = new System.Windows.Forms.Label();
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.txtHotKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkHotKey = new System.Windows.Forms.CheckBox();
            this.cmsNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.SystemColors.Control;
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRun.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRun.ForeColor = System.Drawing.Color.Black;
            this.btnRun.Location = new System.Drawing.Point(12, 19);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(65, 89);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "开始";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblCursor
            // 
            this.lblCursor.AutoSize = true;
            this.lblCursor.Location = new System.Drawing.Point(83, 26);
            this.lblCursor.Name = "lblCursor";
            this.lblCursor.Size = new System.Drawing.Size(107, 12);
            this.lblCursor.TabIndex = 1;
            this.lblCursor.Text = "当前坐标：X-0,Y-0";
            // 
            // lblExpendTime
            // 
            this.lblExpendTime.AutoSize = true;
            this.lblExpendTime.Location = new System.Drawing.Point(83, 47);
            this.lblExpendTime.Name = "lblExpendTime";
            this.lblExpendTime.Size = new System.Drawing.Size(71, 12);
            this.lblExpendTime.TabIndex = 1;
            this.lblExpendTime.Text = "运行状态：0";
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.cmsNotify;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "Cursor";
            this.notify.Visible = true;
            this.notify.DoubleClick += new System.EventHandler(this.notify_DoubleClick);
            // 
            // cmsNotify
            // 
            this.cmsNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiQuit});
            this.cmsNotify.Name = "cmsNotify";
            this.cmsNotify.ShowImageMargin = false;
            this.cmsNotify.Size = new System.Drawing.Size(76, 26);
            // 
            // tsmiQuit
            // 
            this.tsmiQuit.Name = "tsmiQuit";
            this.tsmiQuit.Size = new System.Drawing.Size(75, 22);
            this.tsmiQuit.Text = "退出";
            this.tsmiQuit.Click += new System.EventHandler(this.tsmiQuit_Click);
            // 
            // chkAuto
            // 
            this.chkAuto.AutoSize = true;
            this.chkAuto.Location = new System.Drawing.Point(83, 69);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(72, 16);
            this.chkAuto.TabIndex = 3;
            this.chkAuto.Text = "开机自启";
            this.chkAuto.UseVisualStyleBackColor = true;
            this.chkAuto.Click += new System.EventHandler(this.chkAuto_Click);
            // 
            // txtHotKey
            // 
            this.txtHotKey.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtHotKey.Location = new System.Drawing.Point(147, 91);
            this.txtHotKey.Name = "txtHotKey";
            this.txtHotKey.Size = new System.Drawing.Size(39, 21);
            this.txtHotKey.TabIndex = 4;
            this.txtHotKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHotKey_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Alt +";
            // 
            // chkHotKey
            // 
            this.chkHotKey.AutoSize = true;
            this.chkHotKey.Location = new System.Drawing.Point(83, 94);
            this.chkHotKey.Name = "chkHotKey";
            this.chkHotKey.Size = new System.Drawing.Size(15, 14);
            this.chkHotKey.TabIndex = 6;
            this.chkHotKey.UseVisualStyleBackColor = true;
            this.chkHotKey.Click += new System.EventHandler(this.chkHotKey_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 127);
            this.Controls.Add(this.chkHotKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHotKey);
            this.Controls.Add(this.chkAuto);
            this.Controls.Add(this.lblExpendTime);
            this.Controls.Add(this.lblCursor);
            this.Controls.Add(this.btnRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cursor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.cmsNotify.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblCursor;
        private System.Windows.Forms.Label lblExpendTime;
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.ContextMenuStrip cmsNotify;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
        private System.Windows.Forms.CheckBox chkAuto;
        private System.Windows.Forms.TextBox txtHotKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkHotKey;
    }
}

