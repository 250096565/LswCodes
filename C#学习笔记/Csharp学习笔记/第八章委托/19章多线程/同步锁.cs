using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 第八章委托._19章多线程
{
    class 同步锁
    {
        public static int Tickets { get; set; } = 100;
        //辅助对象
        public static object Obj = new object();

        public void Main()
        {
            ThreadPool.QueueUserWorkItem(Spending);
            ThreadPool.QueueUserWorkItem(Spending2);
        }

        public static void Spending(object obj)
        {//消费一
            while (true)
            {
                try
                {

                    Monitor.Enter(Obj);//获得排他锁
                    if (Tickets > 0)
                    {                       
                        Console.WriteLine($"消费一 {Tickets--}");
                    }
                }
                finally
                {

                    Monitor.Exit(Obj);//释放排他锁
                }
            };
        }

        public static void Spending2(object obj)
        {//消费二
            while (true)
            {
                try
                {
                    Monitor.Enter(Obj);//获得排他锁
                    if (Tickets > 0)
                    {
                        Console.WriteLine($"消费二 {Tickets--}");
                    }
                }
                finally
                {

                    Monitor.Exit(Obj);//释放排他锁
                }
            };
        }
    }
}
