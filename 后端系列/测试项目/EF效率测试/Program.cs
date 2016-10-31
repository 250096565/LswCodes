using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;

namespace EF效率测试
{
    internal enum aaaaa
    {
        Adfaf=1,
        Adfadfaf=2,
        Adfasdfadf=3
    }

    internal class Program
    {
        private static void Main(string[] args)
        {

            foreach (var temp in Enum.GetValues(typeof(aaaaa)))
            {
                string aaa = ((aaaaa) temp).ToString();
            }



            Newtonsoft.Json.JsonSerializerSettings setting = new Newtonsoft.Json.JsonSerializerSettings();
            JsonConvert.DefaultSettings = () =>
            {
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                return setting;
            };

            var p = new OutputModel()
            {
                Data = new List<Person>()
                {
                    new Person() {Name = "小明", Age = "eee", Time = DateTime.Now},
                    new Person() {Name = "小绿", Age = "xxx", Time = DateTime.Now}
                }
                //Data = new Person() { Name = "小明", Age = "eee", Time = DateTime.Now }
            };
            Stopwatch watch = new Stopwatch();

            watch.Start();

            for (int i = 0; i < 50000; i++)
            {
                //Ha(p, new[] {"Name"});

                string reslt = JsonConvert.SerializeObject(p);
            }

            watch.Stop();

            Console.Write("耗时：" + watch.ElapsedMilliseconds + "ms");

            //string s = " dfaf adfff   afdf  ";

            //var replace = s.Replace(" ", "");

            //using (var dbcontext = new SAHZSQEntities())
            //{
            //    var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
            //    var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            //    mappingCollection.GenerateViews(new List<EdmSchemaError>());
            //}

            //using (var entities = new SAHZSQEntities())
            //{
            //    var result = entities.UserRole.Where(o => true).Take(5000).ToList();
            //    var result1 = entities.User.Where(o => true).Take(5000).ToList();

            //}
        }

        private static void Ha(OutputModel output, string[] param)
        {
            if (param == null || param.Length <= 0)
            {
                return;
            }
            Type type = null;

            if (output.Data is IList)
            {
                var ss = output.Data as IList;
                if (ss.Count > 0)
                {
                    type = ss[0].GetType();
                    List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();

                    foreach (var temp in ss)
                    {
                        var instance = new ExpandoObject() as IDictionary<string, Object>;
                        foreach (var property in type.GetProperties())
                        {
                           
                                if (!((IList) param).Contains(property.Name)) continue;
                          
                            object value = property.GetValue(temp, null);
                            instance.Add(property.Name, value);
                        }
                        result.Add(instance);
                    }
                    output.Data = result;
                }
            }
            else
            {
                if (output.Data == null) return;
                type = output.Data.GetType();
                var result = new ExpandoObject() as IDictionary<string, Object>;
                foreach (var property in type.GetProperties())
                {
                    if (!((IList) param).Contains(property.Name)) continue;
                    object value = property.GetValue(output.Data, null);
                    result.Add(property.Name, value);
                }
                output.Data = result;
            }
        }


        private class Person
        {
            public string Name { get; set; }
            public string Age { get; set; }
            public DateTime Time { get; set; }
        }

        private class OutputModel
        {
           
            public object Data { get; set; }
        }
    }
}