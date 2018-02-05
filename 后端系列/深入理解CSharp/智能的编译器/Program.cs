using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace 智能的编译器
{

    class Program
    {




        public string Name { get; set; }
        static void Main(string[] args)
        {
            int allPageNum = Convert.ToInt32(Math.Ceiling(500 * 1.0f / 20));

            int defaultProgress = 1;
            while (Convert.ToInt32(defaultProgress) <= allPageNum)
            {
                for (int i = Convert.ToInt32(defaultProgress); i <= allPageNum; i += 10)
                {
                    var i1 = i;
                    Parallel.For(i, i + 10, c =>
                    {
                        Console.WriteLine(c.ToString());
                    });
                    Console.WriteLine("走完咯");
                    defaultProgress += 10;
                }
            }

            CancellationTokenSource cts = new CancellationTokenSource();

            var task = Task.Factory.StartNew(() =>
              {
                  for (var i = 0; i > 1000; i++)
                  {
                      Task.Delay(100, cts.Token);
                  }
              }, cts.Token);
            cts.Cancel();
            if (task.IsCanceled)
                return;

            Program p = new Program { Name = "小明" };




            var per = new { Name = "小明", Age = 23 };



            var dict = new Dictionary<string, string>()
            {
                {"key","value" },
                {"key1","value1" }
            };
        }
    }
}
