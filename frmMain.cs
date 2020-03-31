using CursorControl.Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CursorControl.Properties;

namespace CursorControl
{
    public partial class frmMain : Form
    {
        #region 参数
        Timer timer = new Timer();
        bool flag = true;
        int counter = 0;
        CursorEntity.PONITAPI p = new CursorEntity.PONITAPI();
        Keys hotKey ;
        #endregion

        public frmMain()
        {
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TimerMission);
        }

        //窗口加载
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.lblCursor.Text = $"当前坐标：X-{p.X},Y-{p.Y}";
            this.lblExpendTime.Text = $"运行状态：{counter}";
            chkAuto.Checked = Settings.Default.IsAutoRun;
            chkHotKey.Checked = Settings.Default.IsHot;
            hotKey = Settings.Default.HotKey;
            if (chkHotKey.Checked)
            {
                SetHotKey(hotKey);
                txtHotKey.Enabled = false;
                txtHotKey.Text = hotKey.ToString();
            }
            if (chkAuto.Checked)
            {
                this.btnRun.PerformClick();
            }
        }

        //运行按钮
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                timer.Start();
                flag = !flag;
                this.btnRun.Text  = "结束";
                this.btnRun.BackColor = Color.FromArgb(250, 177, 160);
            }
            else
            {
                timer.Stop();
                flag = !flag;
                counter = 0;
                p.X = 0;
                p.Y = 0;
                this.lblExpendTime.Text = $"运行状态：{counter}";
                this.lblCursor.Text = $"当前坐标：X-{p.X},Y-{p.Y}";
                this.btnRun.Text = "开始";
                this.btnRun.BackColor = Color.Transparent;
                MessageBox.Show("已停止");
            }
        }

        //通知栏图标双击
        private void notify_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        //最小化按钮
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }

        }

        //开机自启选项
        private void chkAuto_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAuto.Checked == true)
                {
                    AutoStart(true);
                    MessageBox.Show("已设置开机自启");
                }
                else
                {
                    AutoStart(false);
                    MessageBox.Show("已取消开机自启");
                }
                Settings.Default.IsAutoRun = chkAuto.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show("开机自动启动服务注册被拒绝!请确认有系统管理员权限!" + ex.Message);
                chkAuto.Checked = !chkAuto.Checked;
                return;
            }
        }

        //设置快捷键文本
        private void txtHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            hotKey = e.KeyCode;
        }

        //复选框启用快捷键
        private void chkHotKey_Click(object sender, EventArgs e)
        {
            if (chkHotKey.Checked)
            {
                if (txtHotKey.Text == "")
                {
                    MessageBox.Show("输入快捷键");
                    chkHotKey.Checked = false;
                    txtHotKey.Focus();
                    return;
                }
                txtHotKey.Enabled = false;
                SetHotKey(hotKey);
            }
            else
            {
                txtHotKey.Enabled = true;
                HotKeyEntity.UnregisterHotKey(Handle, 102);
            }
            Settings.Default.HotKey = hotKey;
            Settings.Default.IsHot = chkHotKey.Checked;
        }

        //通知栏退出
        private void tsmiQuit_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            Environment.Exit(0);
        }

        //关闭窗口保存所有的设置
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        /// <summary>
        /// 开机自启
        /// </summary>
        /// <param name="isAutoRun"></param>
        public void AutoStart(bool isAutoRun = true)
        {

            string path = Application.ExecutablePath;
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (isAutoRun)
                rk2.SetValue("System Security", path); //rk2.DeleteValue("OIMSServer", false);
            else
                rk2.DeleteValue("System Security", false);
            rk2.Close();
            rk.Close();
        }

        /// <summary>
        /// 光标移动
        /// </summary>
        /// <param name="dx">相对坐标X</param>
        /// <param name="dy">相对坐标Y</param>
        private void MoveCursor(int dx, int dy)
        {
            CursorEntity.GetCursorPos(ref p);
            CursorEntity.mouse_event(CursorEntity.MOUSEEVENTF_MOVE, dx, dy, 0, 0);
            this.lblCursor.Text = $"当前坐标：X-{p.X},Y-{p.Y}";
        }

        /// <summary>
        /// 计时器任务光标任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TimerMission(object sender, EventArgs args)
        {
            counter += 1;
            this.lblExpendTime.Text = $"运行状态：{counter}";
            MoveCursor(1, 0);
            MoveCursor(-1, 0);
        }

        /// <summary>
        /// 覆写快捷键执行任务
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 102:
                            btnRun.PerformClick();
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 设置快捷键
        /// </summary>
        /// <param name="key">Keys keyCode</param>
        private void SetHotKey(Keys key)
        {
            //注册热键Alt+D，Id号为102。HotKey.KeyModifiers.Alt也可以直接使用数字1来表示。
            HotKeyEntity.RegisterHotKey(Handle, 102, HotKeyEntity.KeyModifiers.Alt, key);
        }

        
    }
}
