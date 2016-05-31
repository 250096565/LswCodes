using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 第八章委托._19章多线程
{
    public class 多线程1
    {
        public void Main()
        {
            //无参的委托
            Thread backThread = new Thread(Worker);
            backThread.IsBackground = true;//后台线程,默认为前台
            backThread.Priority=ThreadPriority.AboveNormal;//优先级设置
            //线程开始
            backThread.Start();
            backThread.Join();//线程同步。
            Console.WriteLine("主线程结束");

            Thread backThread2 = new Thread(new ParameterizedThreadStart(Worker));
            //后面的是最大的堆栈大小   
            Thread  backThread3 = new Thread(new ParameterizedThreadStart(Worker),0);



            //线程池
            CancellationTokenSource cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(CallBackWorkItem,cts.Token);
            cts.Cancel();//取消操作
            ThreadPool.QueueUserWorkItem(CallBackWorkItem, "work");




        }

        public static void CallBackWorkItem(object obj)
        {
            CancellationTokenSource token = (CancellationTokenSource) obj;
            for (int i = 0; i < 100; i++)
            {
                if (token.IsCancellationRequested)
                {//判断有没有取消
                    return;
                }
                //打印线程ID
                Console.WriteLine("线程ID为: " + Thread.CurrentThread.ManagedThreadId);
            }
           
        }

        public static void Worker(object data)
        {
            //休眠一秒
            Thread.Sleep(1000);
            Console.WriteLine("后台线程结束了");
        }
    }
}
