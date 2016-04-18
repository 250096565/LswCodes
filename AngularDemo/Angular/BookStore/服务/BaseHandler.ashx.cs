using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace BookStore.服务
{
    /// <summary>
    /// BaseHandler 的摘要说明
    /// </summary>
    public class BaseHandler : IHttpHandler
    {
        /*用反射封装一个顶层Handler,实现类似于MVC的开发方式。其它的类来继承此类即可.有以下约定
                *必须有一个action参数 
                *有一个方法与action同名
                *必须传HttpContext 参数
                *方法访问权限为public
                */

        public void ProcessRequest(HttpContext context)
        {


            //拿到action参数
            string action = context.Request["action"];
            //拿到继承此类的程序集 (正在运行的)
            Type ctllType = this.GetType();

            //拿到actiono字符串值对应的方法描述
            MethodInfo methodAction = ctllType.GetMethod(action);
            //没有此方法
            if (methodAction == null)
            {
                throw new Exception("action不存在");
            }

            //执行此方法
            methodAction.Invoke(this, new object[] { context });
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}