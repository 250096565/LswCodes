using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCP服务器.Properties;

namespace TCP服务器
{
    public partial class Form1 : Form
    {
        private const int Port = 51388;
        private TcpListener tcpLister = null;
        IPAddress ipaddress;
        private TcpClient tcpClient = null;
        private NetworkStream networkStream = null;
        private BinaryReader reader;
        private BinaryWriter writer;


        //声明委托
        //显示消息
        private delegate void ShowMessage(string str);
        private ShowMessage ShowMessageCallback;

        //清空消息
        private delegate void ResetMessage();
        private ResetMessage ResetMessageCallBack;


        public Form1()
        {
            InitializeComponent();

            //实例化委托
            ShowMessageCallback = new ShowMessage(showMessage);
            ResetMessageCallBack = new ResetMessage(resetMessage);

            ipaddress = IPAddress.Loopback;
            txtIP.Text = ipaddress.ToString();
            txtPort.Text = Port.ToString();
        }


        //显示消息
        private void showMessage(string str)
        {
            txtMessage.Text += str + "\n";
        }

        /// <summary>
        /// 请空消息
        /// </summary>
        private void resetMessage()
        {
            txtContent.Text = string.Empty;
        }







        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSend_Click(object sender, EventArgs e)
        {
            Task task = new Task(SendMessage);
            task.Start(txtContent.Text);
        }

        private void SendMessage(object state)
        {
            txtMessage.Invoke(ShowMessageCallback, "正在发送");
            try
            {
                writer.Write(state.ToString());
                Thread.Sleep(5000);
                writer.Flush();
                txtMessage.Invoke(ShowMessageCallback, "发送完毕");
                txtContent.Invoke(ResetMessageCallBack, null);
                txtMessage.Invoke(ShowMessageCallback, state.ToString());
            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }
                txtMessage.Invoke(ShowMessageCallback, "断开连接");
                //从开线程
                Task task = new Task(AcceptClientConnect);
                task.Start();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tcpLister = new TcpListener(ipaddress, Port);
            tcpLister.Start();//开始监听

            //启动线程接收请求
            Task task = new Task(AcceptClientConnect);
            task.Start();
        }


        /// <summary>
        /// 接收请求
        /// </summary>
        private void AcceptClientConnect()
        {
            try
            {
                txtMessage.Invoke(ShowMessageCallback, "等待连接");
                tcpClient = tcpLister.AcceptTcpClient();
                if (tcpLister != null)
                {
                    txtMessage.Invoke(ShowMessageCallback, "接收到连接");
                    networkStream = tcpClient.GetStream();
                    reader = new BinaryReader(networkStream);
                    writer = new BinaryWriter(networkStream);
                }
            }
            catch
            {
                txtMessage.Invoke(ShowMessageCallback, "停止监听");
            }
        }

        /// <summary>
        /// 关闭请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            tcpLister.Stop();
        }

        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtContent.Text = string.Empty;
        }


        //接收消息
        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                string receivemessage = reader.ReadString(); //从网络流中读出字符串
                txtMessage.Invoke(ShowMessageCallback, receivemessage);
            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }
                //从开线程
                Task task = new Task(AcceptClientConnect);
                task.Start();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (writer != null)
            {
                writer.Close();
            }
            if (tcpClient != null)
            {
                tcpClient.Close();
            }
            //从开线程
            Task task = new Task(AcceptClientConnect);
            task.Start();
        }
    }
}
