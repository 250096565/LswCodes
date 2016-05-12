using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace 表达式树结构
{
    class Program
    {
        static void Main(string[] args)
        {
            /*//表达式的参数
            ParameterExpression a = Expression.Parameter(typeof(int), "a");
            ParameterExpression b = Expression.Parameter(typeof(int), "b");

            //表达式树的主体 --在里进行参数传入时所做的操作
            BinaryExpression be = Expression.Add(a, b);

            //构造表达式树
            Expression<Func<int, int, int>> expressionTree = Expression.Lambda<Func<int, int, int>>(be, a, b);

            //分析树结构,获取表达式树的主体部分
            BinaryExpression body = (BinaryExpression) expressionTree.Body;

            //左节点,一个节点就是一个表达式对象
            ParameterExpression left = (ParameterExpression) body.Left;

            //右节点
            ParameterExpression reght = (ParameterExpression) body.Left;

            //输出表达式树结构
            Console.WriteLine($"表达式树结构为:{expressionTree}");
            Console.WriteLine($"表达式树主体为 \n {expressionTree.Body}");
            Console.WriteLine($"表达式树左节点为 :{left.Name} \n 节点类型为:{left.Type}");*/

            


            //将lambda表达式构造成表达式树
            Expression<Func<int, int, int>> expressionTree = (a, b) => a + b;

            //获得表达式树参数
            Console.WriteLine($"参数一 {expressionTree.Parameters[0]} \n参数二 {expressionTree.Parameters[1]}");

            //表达式树的主体 
            BinaryExpression body = (BinaryExpression)expressionTree.Body;

            //左节点
            ParameterExpression left = (ParameterExpression)body.Left;
            Console.WriteLine($"表达式树结构为:{expressionTree}");
            Console.WriteLine($"表达式树主体为 \n {expressionTree.Body}");
            Console.WriteLine($"表达式树左节点为 :{left.Name} \n 节点类型为:{left.Type}");


            //调用Compile方法生成lambda表达式的委托
            Func<int, int, int> doit = expressionTree.Compile();

            //调用
            int result = doit(2, 3);

            Console.WriteLine($"结果为{result}");

        }
    }
}
