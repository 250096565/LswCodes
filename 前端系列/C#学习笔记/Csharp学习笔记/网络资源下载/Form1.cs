using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 网络资源下载.Properties;

namespace 网络资源下载
{
    //使用EAP实现网络资源下载
    public partial class Form1 : Form
    {
        //依次是 当前下载字节、本地保存地址、网络资源总字节、读取缓存直接、缓存字节、本地流、网络返回Response
        public int DownLoadSize = 0;
        public string downLoadPath = null;
        private long totalSize = 0;
        const int BufferSize = 2048;
        byte[] BufferRead = new byte[BufferSize];
        private FileStream fileStream = null;
        private HttpWebResponse response = null;


        #region 4.0中的TAP
        //4.0中的TAP
        private CancellationTokenSource cts = null;
        private Task task = null;
        private SynchronizationContext sc;
        #endregion
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "http://ghost.25pp.com/soft/ppwin_android.exe";
            this.btnPause.Enabled = false;
            GetTotalSize();
            downLoadPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" +
                           Path.GetFileName(textBox1.Text.Trim());
            if (File.Exists(downLoadPath))
            {//如果本地已经有此文件
                FileInfo fileInfo = new FileInfo(downLoadPath);
                DownLoadSize = (int)fileInfo.Length;
                progressBar1.Value = (int)((float)DownLoadSize / (float)totalSize * 100);//计算百分比
            }
            //使BackGroundWorker类支持ReportProgress和Supportsanellation操作
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;


        }

        void GetTotalSize()
        {
            //获得文件大小 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(textBox1.Text);
            HttpWebResponse pon = (HttpWebResponse)request.GetResponse();
            totalSize = pon.ContentLength;
            pon.Dispose();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker;
            try
            {
                //进行下载操作
                //初始化HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(textBox1.Text);
                //如果文件已经下载一部分
                //服务器发送的数据应该以DownSize的值为基量进行http传输
                if (DownLoadSize != 0)
                {
                    request.AddRange(DownLoadSize);
                }
                //httpWebRequest实例对Response分配领域
                response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();//获取返回数据流
                int readSize = 0;
                while (true)
                {
                    if (bgWorker != null && bgWorker.CancellationPending == true)
                    {
                        //异步取消,停止下载
                        e.Cancel = true;
                        break;
                    }
                    //读取
                    readSize = responseStream.Read(BufferRead, 0, BufferRead.Length);
                    if (readSize > 0)
                    {
                        //写入
                        DownLoadSize += readSize;
                        int percentComplete = (int)((float)DownLoadSize / (float)totalSize * 100); //计算已下载百分比
                        fileStream.Write(BufferRead, 0, readSize);
                        //报告进度 
                        bgWorker.ReportProgress(percentComplete);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {//下载任务完成
            if (e.Error != null)
            {
                //出现错误
                MessageBox.Show(e.Error.Message);
                response.Dispose();

            }
            else if (e.Cancelled)
            {
                //中止下载
                MessageBox.Show($"下载暂停，下载的文件地址为：{downLoadPath} ,共下载{DownLoadSize}字节");
                response.Close();
                fileStream.Close();
                btnDown.Enabled = true;
                btnPause.Enabled = false;
            }
            else
            {
                //完成
                MessageBox.Show($"下载完毕! 本地地址为:{downLoadPath}");
                response.Dispose();
                fileStream.Dispose();
                btnDown.Enabled = false;
                btnPause.Enabled = false;
            }
        }

        private async void btnDown_Click(object sender, EventArgs e)
        {
            //下载
            if (!backgroundWorker1.IsBusy)
            {
                //没有在进行下载操作
                backgroundWorker1.RunWorkerAsync();//开始后台操作
                fileStream = new FileStream(downLoadPath, FileMode.OpenOrCreate);
                //设置断点下载,从上次中断的字节开始
                fileStream.Seek(DownLoadSize, SeekOrigin.Begin);
                btnDown.Enabled = false;
                btnPause.Enabled = true;
            }
            else
            {
                //正在下载 
                MessageBox.Show(Resources.Form1_btnDown_Click_正在下载_请等待);
            }


            #region C#4.0中的TAP
            //4.0中的
            //捕捉调用线程的同步上下文对象
            sc = SynchronizationContext.Current;
            cts = new CancellationTokenSource();
            //使用指定的操作初始新的Task
            task = new Task(() => DownLoadFileWithTAP(textBox1.Text, cts, new Progress<int>(p =>
            {
                sc.Post(new SendOrPostCallback((result) => progressBar1.Value = (int)result), p);
            })));
            task.Start();
            #endregion


            //5.0中的
            cts = new CancellationTokenSource();
            await DownLoadFileAsync(textBox1.Text, cts.Token, new Progress<int>(p => progressBar1.Value = p));

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            //暂停
            if (backgroundWorker1.IsBusy && backgroundWorker1.WorkerSupportsCancellation)
            {
                backgroundWorker1.CancelAsync();
            }

            #region C#4.0中的TAP
            //4.0中的
            cts.Cancel();
            #endregion
        }


        #region 5.0中的async await
        public async Task DownLoadFileAsync(string url, CancellationToken ct, IProgress<int> progress)
        {
            HttpWebRequest request2 = null;
            HttpWebResponse response2 = null;
            Stream responseStream = null;
            try
            {
                request2 = (HttpWebRequest)WebRequest.Create(url);
                if (DownLoadSize != 0)
                {
                    request2.AddRange(DownLoadSize);
                }
                response2 = (HttpWebResponse)await request2.GetResponseAsync();
                responseStream = response2.GetResponseStream();
                int readSize = 0;
                while (true)
                {
                    if (ct.IsCancellationRequested)
                    {
                        //调用同步上下文的POST方法,让主线程来执行更新UI的方法
                        sc.Post((state) =>
                        {
                            this.btnPause.Enabled = false;
                            this.btnDown.Enabled = true;
                        }, null);
                        break;
                    }

                    //各种读写
                    readSize = await responseStream.ReadAsync(BufferRead, 0, BufferRead.Length);
                    await fileStream.WriteAsync(BufferRead, 0, readSize);
                    //汇报进度
                    progress.Report(12456);

                    //下载成功的情况
                    sc.Post((state) =>
                    {
                        this.btnPause.Enabled = true;
                        this.btnDown.Enabled = false;
                    }, null);
                    //释放
                    response2.Dispose();
                    responseStream.Dispose();
                    break;
                }
            }
            catch (AggregateException ex)
            {
                //调用Cancel方法会抛出Operationcanceledexception异常
                //这里，任何的Operationcanceledexception都会被视为已处理
                ex.Handle(o => o is OperationCanceledException);
            }
        }


        #endregion


        #region C#4.0中的TAP方法
        //4.0中的
        public void DownLoadFileWithTAP(string url, CancellationTokenSource ct, IProgress<int> progress)
        {
            HttpWebRequest request2 = null;
            HttpWebResponse response2 = null;
            Stream responseStream = null;
            try
            {
                request2 = (HttpWebRequest)WebRequest.Create(url);
                if (DownLoadSize != 0)
                {
                    request2.AddRange(DownLoadSize);
                }
                response2 = (HttpWebResponse)request2.GetResponse();
                responseStream = response2.GetResponseStream();
                int readSize = 0;
                while (true)
                {
                    if (ct.IsCancellationRequested)
                    {
                        //调用同步上下文的POST方法,让主线程来执行更新UI的方法
                        sc.Post((state) =>
                        {
                            this.btnPause.Enabled = false;
                            this.btnDown.Enabled = true;
                        }, null);
                        break;
                    }

                    //各种读
                    //汇报进度
                    progress.Report(12456);

                    //下载成功的情况
                    sc.Post((state) =>
                    {
                        this.btnPause.Enabled = true;
                        this.btnDown.Enabled = false;
                    }, null);
                    //释放
                    response2.Dispose();
                    responseStream.Dispose();
                    break;
                }
            }
            catch (AggregateException ex)
            {
                //调用Cancel方法会抛出Operationcanceledexception异常
                //这里，任何的Operationcanceledexception都会被视为已处理
                ex.Handle(o => o is OperationCanceledException);
            }
        }
        #endregion

    }
}
