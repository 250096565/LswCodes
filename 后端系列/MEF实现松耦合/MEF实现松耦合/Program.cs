using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEF实现松耦合
{
    class Program
    {
        [Import("Chinese")]
        public IPerson OChinese { get; set; }


        static void Main(string[] args)
        {
            //不用new就可以拿到对象
            Program program = new Program();
            program.MyCompostPart();
            var res = program.OChinese.SayHello("李雷");

            Console.WriteLine(res);
        }

        //宿主MEF并组合部件 
        void MyCompostPart()
        {
            var cataLog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(cataLog);
            //将部件(part)和宿主程序加到组合容器
            container.ComposeParts(this);
        }
    }

    //Person接口
    public interface IPerson
    {
        string SayHello(string name);
    }

    //中文
    [Export("Chinese", typeof(IPerson))]
    public class Chinese : IPerson
    {
        public string SayHello(string name)
        {
            return "你好 " + name;
        }
    }

    //英文
    [Export("American", typeof(IPerson))]
    public class American : IPerson
    {
        public string SayHello(string name)
        {
            return "hello " + name;
        }
    }

}
