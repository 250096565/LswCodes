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
using Tcp客户端.Properties;

namespace Tcp客户端
{
    public partial class Form1 : Form
    {

        //tcp对象及网络流与id
        private TcpClient tcpClient = null;
        private NetworkStream networkStream = null;
        private BinaryReader reader = null;
        private BinaryWriter writer = null;
        private const int Port = 51388;
        private IPAddress ipaddress;


        //显示回调 
        private delegate void ShowMessage(string str);
        private ShowMessage showMessageBack;

        //清空消息回调 
        private delegate void ResetMessage();

        private ResetMessage resetMessageBack;


        public Form1()
        {
            InitializeComponent();

            //显示消息
            showMessageBack = new ShowMessage(showMessage);

            //清空消息
            resetMessageBack = new ResetMessage(resetMessage);


            ipaddress = IPAddress.Loopback;
            txtPort.Text = ipaddress.ToString();
            txtPort.Text = Port.ToString();
        }

        private void showMessage(string str)
        {
            txtMessage.Text += Environment.NewLine + str;
        }


        private void resetMessage()
        {
            txtContent.Text = string.Empty;
        }


        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConn_Click(object sender, EventArgs e)
        {
            Task task = new Task(ConnectToServer);
            task.Start();
        }


        /// <summary>
        /// 连接服务器方法
        /// </summary>
        private void ConnectToServer()
        {
            try
            {
                if (string.IsNullOrEmpty(txtIP.Text) || string.IsNullOrEmpty(txtPort.Text))
                {
                    MessageBox.Show(Resources.Form1_ConnectToServer_请先输入服务器IP与端口号);
                    return;
                }
                IPAddress ipadddress = IPAddress.Parse(txtIP.Text);
                tcpClient = new TcpClient();
                tcpClient.Connect(ipadddress, int.Parse(txtPort.Text));
                Task.Delay(1000);//延时一秒
                if (tcpClient != null)
                {
                    MessageBox.Show(Resources.Form1_ConnectToServer_连接成功);
                    networkStream = tcpClient.GetStream();
                    reader = new BinaryReader(networkStream);
                    writer = new BinaryWriter(networkStream);
                }
            }
            catch
            {
                MessageBox.Show(Resources.Form1_ConnectToServer_连接失败);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            Task task = new Task(receiveMessage);
            task.Start();
        }

        private void receiveMessage()
        {
            try
            {
                string str = reader.ReadString();
                txtMessage.Invoke(showMessageBack, str);
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
            }
        }

        //断开连接
        private void btnStop_Click(object sender, EventArgs e)
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
        }

        //发送消息
        private void btnSend_Click(object sender, EventArgs e)
        {
            Thread task = new Thread(SendMessage);
            task.Start("客户端  " + DateTime.Now.ToLocalTime() + Environment.NewLine + "   " + txtContent.Text);
        }

        private void SendMessage(object state)
        {
            try
            {
                writer.Write(state.ToString());
                Thread.Sleep(1000);
                writer.Flush();

                txtContent.Invoke(resetMessageBack, null);
                txtMessage.Invoke(showMessageBack, state.ToString());
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
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMessage.Text = string.Empty;
        }
    }
}
