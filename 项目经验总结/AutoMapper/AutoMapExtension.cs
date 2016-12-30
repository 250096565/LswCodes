using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
namespace AutoMapperExt.AutoMapper
{
    /// <summary>
    /// dtoManager后期可以被弃用,这里提供了更为简便的映射方式
    /// </summary>
    public static class AutoMapExtension
    {

        public static TDestination MapTo<TDestination>(this object source)
        {
            try
            {
                return Mapper.Map<TDestination>(source);
            }
            catch
            {
                return ConvertObjectToList<TDestination>(source);
            }
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            try
            {
                return Mapper.Map(source, destination);
            }
            catch (Exception)
            {

                return ConvertObjectToList<TDestination>(source);
            }
        }

        private static TO ConvertObjectByEntity<TO>(object entity)
        {
            if (entity == null)
            {
                return default(TO);
            }

            try
            {
                Type type = typeof(TO);
                TO t = CreateInstance<TO>(type.Assembly.FullName, type.FullName);
                PropertyInfo[] propertyInfos = entity.GetType().GetProperties();

                PropertyInfo[] toPropertyInfo = type.GetProperties();
                foreach (PropertyInfo propertyInfo in toPropertyInfo)
                {
                    PropertyInfo tpropertyInfo = propertyInfos.FirstOrDefault(o => o.Name == propertyInfo.Name);
                    if (tpropertyInfo == null)
                    {
                        continue;
                    }
                    object value = GetPropertyValue(entity, tpropertyInfo.Name);
                    if (value == null)
                    {
                        continue;
                    }
                    propertyInfo.SetValue(t, value, null);
                }
                return t;
            }
            catch (Exception)
            {

                return default(TO);
            }
        }

        private static TO ConvertObjectToList<TO>(object entitys)
        {
            try
            {
                Type typeTo = typeof(TO);

                TO too = (TO)Assembly.Load(typeTo.Assembly.FullName).CreateInstance(typeTo.FullName);
                if (too is IList)
                {

                    var toList = too as IList;
                    var listT = entitys as IList;
                    if (listT == null || listT.Count == 0)
                    {
                        return too;
                    }

                    Type toType = typeTo.GenericTypeArguments[0];
                    PropertyInfo[] arrPropertyTo = toType.GetProperties();
                    var assemblyTo = Assembly.Load(toType.Assembly.FullName);
                    Type typeT = null;
                    foreach (var item in listT)
                    {
                        var to = assemblyTo.CreateInstance(toType.FullName);// CreateInstance(tfirst.Assembly.FullName, tfirst.FullName);

                        if (typeT == null)
                            typeT = item.GetType();
                        PropertyInfo[] arrPropertyT = typeT.GetProperties();
                        foreach (var pro in arrPropertyT)
                        {
                            object vaule = pro.GetValue(item);
                            PropertyInfo property = arrPropertyTo.FirstOrDefault(o => o.Name == pro.Name);
                            if (property != null)
                                property.SetValue(to, vaule);
                        }
                        toList.Add(to);
                    }

                    return (TO)toList;

                }//if (too is IList)
                else
                {
                    return ConvertObjectByEntity<TO>(entitys);
                }
            }
            catch
            {
                return default(TO);
            }
        }


        /// <summary>
        /// 获取属性值
        /// </summary>
        private static object GetPropertyValue(object entity, string propertyName)
        {
            PropertyInfo pi = entity.GetType().GetProperty(propertyName);

            if (pi == null)
            { return null; }

            return pi.GetValue(entity, null);
        }

        private static T CreateInstance<T>(string assemblyName, string fullName)
        {
            try
            {
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);
                return (T)ect;
            }
            catch
            {
                return default(T);
            }
        }
       
    }
}
