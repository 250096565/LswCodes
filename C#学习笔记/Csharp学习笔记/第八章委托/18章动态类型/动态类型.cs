using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Drawing;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace 第八章委托._18章动态类型
{
    public class 动态类型
    {
        public void Main()
        {



            //使用ExpandoObject来实现动态行为

            dynamic expand = new ExpandoObject();
            expand.Name = "小明";
            expand.Age = 23;
            expand.SayHi = (Func<int, int>)(x => x + 1);




            //dynamic dyn = x => x + 1;
            //这样就不会报错
            dynamic dyn1 = (Func<int, int>)(x => x + 1);

            //调用动态类型方法
            aaa(1);
            aaa("");

        }

        //参数为动态类型
        public void aaa(dynamic temp)
        {
            Console.WriteLine(temp);
        }

    }


    //重写DynamicObject
    class DynamicType : DynamicObject
    {
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            //动态调用方法时此方法会被触发
            Console.WriteLine(binder.Name + "方法正在被调用");
            result = null;
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            //动态添加属性设置值时此方法会被触发
            Console.WriteLine(binder.Name + "属性被设置,值为" + value);
            return true;
        }
    }


    class DynamicType2 : IDynamicMetaObjectProvider
    {
        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            Console.WriteLine("开始获取元数据");
            return new MetaDynamic(parameter, this);
        }
    }

    class MetaDynamic : DynamicMetaObject
    {

        //继承父类的构造函数
        internal MetaDynamic(Expression ex, DynamicType2 value) : base(ex, BindingRestrictions.Empty, value) { }


        public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
        {
            //获取真正的对象
            DynamicType2 target = (DynamicType2)base.Value;
            Expression self = Expression.Convert(base.Expression, typeof(DynamicType2));
            var restrictions = BindingRestrictions.GetInstanceRestriction(self, target);
            //获取绑定方法名
            Console.WriteLine(binder.Name + "方法被调用了");

            return new DynamicMetaObject(self, restrictions);
        }

    }
}
