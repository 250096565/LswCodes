using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 委托
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());




            var action = GetDelegate();
            //在这里GetDelegate方法已经执行结束了,而action仍然存在 ,i变量的生成周期被延长
            action();
            action();

        }

        public static Action GetDelegate()
        {
            int i = 0;
            Action action = delegate ()
            {
                i++; Console.WriteLine(i);
            };
            //方法结束,i应该被收回,但是action委托中捕获了i变量,并且把action委托实例进行了返回
            return action;
        }
    }
}
