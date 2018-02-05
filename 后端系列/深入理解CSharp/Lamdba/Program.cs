using System;
using System.Linq.Expressions;

namespace Lamdba
{
    class Program
    {

        delegate T MyFunc<T>();

        static void WriteResult<T>(MyFunc<T> function)
        {
            Console.WriteLine(function());
        }

        static void Main(string[] args)
        {
            WriteResult(() =>
            {
                if (DateTime.Now.Hour < 12)
                {
                    return 10;
                }
                return new object();
            });


            Expression first = Expression.Constant(2);
            Expression second = Expression.Constant(2);

            Expression add = Expression.Add(first, second);

            Console.WriteLine(add);


            var compiled = Expression.Lambda<Func<int>>(add).Compile();

            Console.WriteLine(compiled());



            Expression<Func<int, int, int>> fuc = (x, y) => x + y;

            var f = fuc.Compile();
            Console.WriteLine(f(1, 2));

        }
    }
}