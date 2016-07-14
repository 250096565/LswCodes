using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CLR创建线程数
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Stopwatch watch = new Stopwatch();
            watch.Start();
            new Program().CreateThread();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);*/

            string date = "adf@.adsf.com456";
            //DateTime daa = Convert.ToDateTime(date);

            if (!Program.IsEmail(date))
            {
                Console.WriteLine("false");
                return;
            }
            Console.WriteLine("true");

        }

        /// <summary>
        /// 检查邮箱
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^\w+@\.(\w+)\.((com)|(COM)|(cn)|(CN))$");
           
        }


        public static bool IsPassport(string str)
        {
            return Regex.IsMatch(str, @"^P\d{7}$|G\d{8}$");
        }


        //判断是否为正确的时间格式
        public static bool IsDateTime(string date)
        {
            try
            {
                Convert.ToDateTime(date);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        void CreateThread()
        {
            Thread th = new Thread(Null);
            Thread t3 = new Thread(Null);

            th.Start();
            t3.Start();



        }

        void Null()
        {

        }
    }
}
