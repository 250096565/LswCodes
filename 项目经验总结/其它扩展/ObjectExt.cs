using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Ext
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class ObjectExt
    {

        /// <summary>
        /// 获取attribute的值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enu"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object ToAttributeValue<T>(this object enu, string propertyName) where T : class
        {
            Type type = enu.GetType();
            FieldInfo info = type.GetField(enu.ToString());
            T attribute = info.GetCustomAttributes(typeof(T), true)[0] as T;

            var att = attribute.GetType();
            var pro = att.GetProperty(propertyName);

            return pro.GetValue(attribute);
        }
    }
}
