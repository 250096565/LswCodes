using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 扩展方法
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = "123";
            str.ValidArg();
        }
    }

    public static class StringHelper
    {
        public static void ValidArg(this string arg)
        {
            if (arg == null || arg != "admin")
                throw new ArgumentException();
        }
    }
}
