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
        Timer controlTimer = new Timer();
        bool flag = false;//按钮处于是否被点击的状态
        int counter = 0;
        CursorEntity.PONITAPI p = new CursorEntity.PONITAPI();
        Keys hotKey;
        DateTime timeStart;
        DateTime timeEnd;
        #endregion

        public frmMain()
        {
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TimerMission);
            controlTimer.Interval = 1000;
            controlTimer.Tick += new EventHandler(ControlTimer);
        }

        //窗口加载
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.lblCursor.Text = $"当前坐标：X-{p.X},Y-{p.Y}";
            this.lblExpendTime.Text = $"运行状态：{counter}";
            chkAuto.Checked = Settings.Default.IsAutoRun;
            chkHotKey.Checked = Settings.Default.IsHot;
            hotKey = Settings.Default.HotKey;
            dtpStart.Value = Settings.Default.dtpStartTimeData;
            dtpEnd.Value = Settings.Default.dtpEndTimeData;
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
            chkTime.Checked = Settings.Default.IsSetTime;
        }

        //运行按钮
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                controlTimer.Stop();
                timer.Stop();
                flag = false;
                counter = 0;
                p.X = 0;
                p.Y = 0;
                this.lblExpendTime.Text = $"运行状态：{counter}";
                this.lblCursor.Text = $"当前坐标：X-{p.X},Y-{p.Y}";
                this.btnRun.Text = "开始";
                this.btnRun.BackColor = Color.Transparent;
                chkTime.Enabled = true;
                chkAuto.Enabled = true;
                chkHotKey.Enabled = true;
                txtHotKey.Enabled = true;
                dtpStart.Enabled = true;
                dtpEnd.Enabled = true;
                MessageBox.Show("已停止");
            }
            else
            {
                if (chkTime.Checked)
                {
                    controlTimer.Start();
                }
                else
                {
                    timer.Start();
                }
                flag = true;
                this.btnRun.Text = "结束";
                this.btnRun.BackColor = Color.FromArgb(250, 177, 160);
                chkTime.Enabled = false;
                dtpStart.Enabled = false;
                dtpEnd.Enabled = false;
                chkAuto.Enabled = false;
                chkHotKey.Enabled = false;
                txtHotKey.Enabled = false;
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

        //复选框启用定时
        private void chkTime_Click(object sender, EventArgs e)
        {
            if (chkTime.Checked)
            {
                if (dtpStart.Value > dtpEnd.Value)
                {
                    MessageBox.Show("检查时间");
                    chkTime.Checked = false;
                    return;
                }
                timeStart = DateTime.Parse($"{dtpStart.Value.Hour}:{dtpStart.Value.Minute}:{dtpStart.Value.Second}");
                timeEnd = DateTime.Parse($"{dtpEnd.Value.Hour}:{dtpEnd.Value.Minute}:{dtpEnd.Value.Second}");
            }
            else
            {
                dtpStart.Enabled = true;
                dtpEnd.Enabled = true;
            }
            Settings.Default.IsSetTime = chkTime.Checked;
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

        /// <summary>
        /// 检测定时任务控制计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void ControlTimer(object sender, EventArgs arg)
        {
            if (DateTime.Now > timeStart && DateTime.Now < timeEnd)
            {
                timer.Stop();
                this.lblExpendTime.Text = $"运行状态：正在计划停止";
            }
            else
            {
                timer.Start();
            }
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            this.chkTime.Checked = false;
            Settings.Default.dtpStartTimeData = dtpStart.Value;
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            this.chkTime.Checked = false;
            Settings.Default.dtpEndTimeData = dtpEnd.Value;
        }
    }

    
}
