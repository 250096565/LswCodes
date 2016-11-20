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

namespace 聊天窗口
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Cutter cutter = null;

        #region 窗体事件
        //截图按钮点击事件的处理程序

        private void btnCutter_Click(object sender, EventArgs e)
        {
            //根据屏幕大小创建一个相同大小的图片
            Bitmap catchBmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            //创建画板
            Graphics g = Graphics.FromImage(catchBmp);

            //把屏幕图片复制到创建的空白图片中
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0),
                new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height));

            cutter = new Cutter
            {
                BackgroundImage = catchBmp,
                Width = Screen.AllScreens[0].Bounds.Width,
                Height = Screen.AllScreens[0].Bounds.Height
            }; //截图窗体
            //窗体设置与屏幕一样大小

            if (cutter.ShowDialog() != DialogResult.OK) return;
            IDataObject idata = Clipboard.GetDataObject();

            DataFormats.Format format = DataFormats.GetFormat(DataFormats.Bitmap);

            if (idata != null && idata.GetDataPresent(DataFormats.Bitmap))
            {
                richTextBox1.Paste(format);
                Clipboard.Clear();//清除剪切板的图片 
            }
        }




        #endregion

        //在窗体加载的时候注册快捷键
        private void Form1_Load(object sender, EventArgs e)
        {
            uint ctrlHotKey = (uint)(KeyModifiers.Alt | KeyModifiers.Ctrl);

            //注册快捷键为Ctrl+alt+C
            HotKey.RegisterHotKey(Handle, 100, ctrlHotKey, Keys.C);
        }



        //窗体关闭时取消快捷键注册 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HotKey.UnregisterHotKey(Handle, 100);
        }



        //快捷键按下时执行的方法
        private void GlobalKeyProcess()
        {
            this.WindowState = FormWindowState.Minimized;

            Thread.Sleep(200);

            btnCutter.PerformClick();
        }


        //监视windows消息
        protected override void WndProc(ref Message m)
        {
            //如果m.msg的值为0x0312,则表示用户按下了快捷键

            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {
                case WM_HOTKEY:
                    if (m.WParam.ToString() == "100")
                    {
                        GlobalKeyProcess();
                    }
                    break;
            }

            //将系统消息传递至父类的WndPorc
            base.WndProc(ref m);
        }

    }


}
