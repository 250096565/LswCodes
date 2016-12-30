using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Tool.Response;

namespace MvcCustommade
{
    public class LimitPropsContractResolver
    {



        private static object Filtered(object obj, string[] props, bool retain)
        {

            if (obj == null)
                return null;
            Type type = null;
            if (obj is IList)
            {
                var dataList = obj as IList;
                if (dataList.Count > 0)
                {
                    type = dataList[0].GetType();
                    List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();

                    foreach (var temp in dataList)
                    {
                        var instance = new ExpandoObject() as IDictionary<string, Object>;
                        foreach (var property in type.GetProperties())
                        {
                            if (retain)
                            {
                                if (!((IList)props).Contains(property.Name.ToLower())) continue;
                            }
                            else
                            {
                                if (((IList)props).Contains(property.Name)) continue;
                            }

                            object value = property.GetValue(temp, null);
                            instance.Add(property.Name, value);
                        }
                        result.Add(instance);
                    }
                    obj = result;
                }
            }
            else
            {

                type = obj.GetType();
                var result = new ExpandoObject() as IDictionary<string, Object>;
                foreach (var property in type.GetProperties())
                {
                    if (retain)
                    {
                        if (!((IList)props).Contains(property.Name.ToLower())) continue;
                    }
                    else
                    {
                        if (((IList)props).Contains(property.Name.ToLower())) continue;
                    }
                    object value = property.GetValue(obj, null);
                    result.Add(property.Name, value);

                }
                obj = result;
            }

            return obj;
        }

        /// <summary>
        /// 过滤无用的属性
        /// </summary>
        /// <param name="output">返回的数据</param>
        /// <param name="props">过滤的属性数组</param>
        /// <param name="retain">过滤的数组是包含还是不包含</param>
        public static object CreateProperties(object output, string[] props, bool retain)
        {
            if (props == null || props.Length <= 0)
            {
                return output;
            }
            if (output == null)
            {
                return null;
            }
            for (int i = 0; i < props.Length; i++)
            {
                props[i] = props[i].ToLower();
            }
            if (output is OutputModel)
            {
                var outputModel = output as OutputModel;
                if (outputModel.Data == null)
                {
                    return outputModel;
                }
                outputModel.Data = Filtered(outputModel.Data, props, retain);
                return outputModel;

                #region 初始版

                //Type type = null;

                //if (outputModle.Data is IList)
                //{
                //    var ss = outputModle.Data as IList;
                //    if (ss.Count > 0)
                //    {
                //        type = ss[0].GetType();
                //        List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();

                //        foreach (var temp in ss)
                //        {
                //            var instance = new ExpandoObject() as IDictionary<string, Object>;
                //            foreach (var property in type.GetProperties())
                //            {
                //                if (retain)
                //                {
                //                    if (!((IList)props).Contains(property.Name)) continue;
                //                }
                //                else
                //                {
                //                    if (((IList)props).Contains(property.Name)) continue;
                //                }

                //                object value = property.GetValue(temp, null);
                //                instance.Add(property.Name, value);
                //            }
                //            result.Add(instance);
                //        }
                //        outputModle.Data = result;
                //    }
                //}
                //else
                //{
                //    if (outputModle.Data == null) return;
                //    type = outputModle.Data.GetType();
                //    var result = new ExpandoObject() as IDictionary<string, Object>;
                //    foreach (var property in type.GetProperties())
                //    {
                //        if (retain)
                //        {
                //            if (!((IList)props).Contains(property.Name)) continue;
                //        }
                //        else
                //        {
                //            if (((IList)props).Contains(property.Name)) continue;
                //        }
                //        object value = property.GetValue(outputModle.Data, null);
                //        result.Add(property.Name, value);

                //    }
                //    outputModle.Data = result;
                //} 

                #endregion
            }
            else
            {
                output = Filtered(output, props, retain);
                return output;
            }

        }
    }
}
