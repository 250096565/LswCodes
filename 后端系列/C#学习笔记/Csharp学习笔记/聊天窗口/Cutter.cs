using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 聊天窗口
{
    public partial class Cutter : Form
    {
        public Cutter()
        {
            InitializeComponent();
        }


        #region 定义变量

        private Point DownPoint;
        private bool CatchFinished = false;
        private bool CatchStart = false;
        private Bitmap originBmp;//用来保存原始图像
        private Rectangle CatchRectangle;//用来保存截图的矩形

        #endregion



        private void Cutter_Load(object sender, EventArgs e)
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Cursor = Cursors.Cross;//改变鼠标样式
            originBmp = new Bitmap(this.BackgroundImage);//保存全屏图片
        }

        /// <summary>
        /// 鼠标点击控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cutter_MouseClick(object sender, MouseEventArgs e)
        {
            //如果按的是右键
            if (e.Button == MouseButtons.Right)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();//结束
            }
        }

        /// <summary>
        /// 鼠标按下事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cutter_MouseDown(object sender, MouseEventArgs e)
        {
            //左键按下开始画图/截图
            if (e.Button == MouseButtons.Left)
            {
                //如果捕捉没有开始
                if (!CatchStart)
                {
                    CatchStart = true;
                    //保存截图开始时鼠标所在的坐标
                    DownPoint = new Point(e.X, e.Y);
                }
            }

        }

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        private void Cutter_MouseMove(object sender, MouseEventArgs e)
        {   //确保过程开始
            if (!CatchStart) return;
            //新建一个图片对象,让它与屏幕图片相同
            Bitmap copyBmp = (Bitmap)originBmp.Clone();

            //获取按下的鼠标时的坐标
            Point newPoint = new Point(DownPoint.X, DownPoint.Y);

            Graphics g = Graphics.FromImage(copyBmp);
            Pen p = new Pen(Color.Red, 1);//新建 "笔"

            int width = Math.Abs(e.X - DownPoint.X);
            int height = Math.Abs(e.Y - DownPoint.Y);
            //算出距离

            //如果截图开始位置发生变化
            if (e.X < DownPoint.X)
            {
                newPoint.X = e.X;
            }
            if (e.Y < DownPoint.Y)
            {
                newPoint.Y = e.Y;
            }

            CatchRectangle = new Rectangle(newPoint, new Size(width, height));
            g.DrawRectangle(p, CatchRectangle);//将矩形画在画板上

            //释放当前画板

            g.Dispose();
            p.Dispose();

            Graphics g1 = this.CreateGraphics();

            g1.DrawImage(copyBmp, new Point(0, 0));

            g1.Dispose();
            copyBmp.Dispose();//释放复制的图片,防止大量内存消耗
        }

        //鼠标左键弹起事件
        private void Cutter_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            //如果截图已经开始,则截图完成
            if (!CatchStart) return;
            CatchStart = false;
            CatchFinished = true;
        }

        /// <summary>
        /// 鼠标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cutter_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !CatchFinished) return;
            Bitmap catchedBmp = new Bitmap(CatchRectangle.Width, CatchRectangle.Height);
            Graphics g = Graphics.FromImage(catchedBmp);

            g.DrawImage(originBmp, new Rectangle(0, 0, CatchRectangle.Width, CatchRectangle.Height), CatchRectangle,
                GraphicsUnit.Pixel);

            //将图片保存到剪切板中
            Clipboard.SetImage(catchedBmp);
            g.Dispose();

            CatchFinished = false;
            this.BackgroundImage = originBmp;
            catchedBmp.Dispose();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }



    }


    public class HotKey
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,  //窗口的句柄
            int id,       //快捷键ID,起到唯一标识快捷键的作用
            uint fsModifiers,
            Keys vk
            );



        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,  //窗口的句柄
            int id   //要取消的快捷键ID
        );
    }


    [Flags]
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Ctrl = 2,
        Shift = 4,
        WindowsKey = 8
    }






}
