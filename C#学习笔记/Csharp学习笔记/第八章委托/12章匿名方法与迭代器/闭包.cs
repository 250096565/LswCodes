using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 第八章委托._12章匿名方法与迭代器
{
    public class 闭包
    {
        //闭包委托
        delegate void ClosureDelegate();

        //模拟的Main方法
        public void Main()
        {
            ClosureDelegate one = DoClosureDelegateInstance();

            one();  
            /*
             进行do...方法时会先调用two委托实例,方法执行完毕后count变量应该会被销毁.但是还有匿名对象的引用。所以
             * 它的生命周期被延长。
             */
        }

        //闭包延长变量的生命周期



        private static ClosureDelegate DoClosureDelegateInstance()
        {
            //这里定义了外部变量
            int count = 1;
            ClosureDelegate two = () =>
            {//匿名方法
                Console.WriteLine(count);
                count++;
            };

            //调用委托
            two();
            return two;
        }
    }
}
