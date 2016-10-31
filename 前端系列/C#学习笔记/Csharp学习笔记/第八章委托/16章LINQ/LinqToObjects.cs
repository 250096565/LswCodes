using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace 第八章委托._16章LINQ
{
    public class LinqToObjects
    {
        //这里是模拟的Main方法
        public void Main()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 8, 7 };

            Query(list);
        }

        public void Query(List<int> list)
        {

            //使用Linq to Objects查询
            var queryResult = from item in list where item % 2 == 0 select item;

            foreach (var i in queryResult)
            {
                Console.WriteLine("item:" + i);
            }
        }
    }
}
