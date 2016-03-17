using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary dic = ConfigurationManager.GetSection("secion1") as IDictionary;
            Console.WriteLine(dic["sec1"]);
        }
    }
}
