using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunOwin
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:10002"))
            {
                System.Console.WriteLine("启动站点：http://localhost:10002");
                System.Console.ReadLine();
            }
        }
    }
}
