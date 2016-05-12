using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace 第八章委托._15章扩展方法
{
    class 扩展方法
    {
        public void Main()
        {
            object obj = new object();
            obj.ExtFunction();
        }
    }

    static class Custom
    {
        public static void ExtFunction(this Object obj)
        {
            Console.WriteLine("这是扩展的");
        }
    }
}
