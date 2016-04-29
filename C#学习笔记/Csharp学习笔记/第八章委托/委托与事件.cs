using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 第八章委托
{
    public class 委托与事件
    {

        static void Main(string[] args)
        {
            GoodMan goodman = new GoodMan();
            Friend f1 = new Friend("小明");
            Friend f2 = new Friend("小红");
            //注册
            goodman.MarryEvent += new GoodMan.MarryHandler(f1.SendMessage);
            goodman.MarryEvent += new GoodMan.MarryHandler(f2.SendMessage);
            //移除
            goodman.MarryEvent -= new GoodMan.MarryHandler(f1.SendMessage);
            goodman.OnMarryEvent("下课来我办公室,小明就不要来了");
        }

    }

    class MarrayEventArgs : EventArgs
    {
        public string Message;

        public MarrayEventArgs(string msg)
        {
            Message = msg;
        }
    }

    class GoodMan
    {
        public delegate void MarryHandler(object sender, MarrayEventArgs e);

        public event MarryHandler MarryEvent;

        public virtual void OnMarryEvent(string msg)
        {
            //判断是否绑定了事件
            if (MarryEvent != null)
            {
                //触发事件
                MarryEvent(this, new MarrayEventArgs(msg));
            }
        }
    }

    class Friend
    {
        public string Name { get; set; }

        public Friend(string name)
        {
            this.Name = name;
        }

        //事件处理
        public void SendMessage(object s, MarrayEventArgs e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(this.Name + ": 好的好的");
        }

    }


    #region ---------------第八章委托----------------
    /*
         *         internal delegate void DelegateTest();
         * static void Main(string[] args)
        {
            //多播委托
            DelegateTest doTest;
            doTest = new DelegateTest(Program.Method1);
            doTest += new DelegateTest(new Program().Method2);
            doTest();
        }
         *   private static void Method1()
        {
            Console.WriteLine("这是静态方法");
        }

        public void Method2()
        {
            Console.WriteLine("这是实例方法");
        }

        public static void StartDelegateTest(DelegateTest param)
        {
            //显示调用 
            param.Invoke();
        }
         */

    #endregion
}
