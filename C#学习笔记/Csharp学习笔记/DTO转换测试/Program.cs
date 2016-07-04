using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmitMapper;

namespace DTO转换测试
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(o => o.CreateMap<Person, PersonDto>());

            Mapper.Initialize(o => o.CreateMap<PersonDto, Person>().ForMember(entity => entity.Name, opt => opt.Ignore()));

            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<Person, PersonDto>();
            List<Person> per = new List<Person>() { new Person() { Age = "23", Gender = "男", Name = "小明", Pitem = new P1() { Gender = "24", Name = "小红" } } };
            Stopwatch watch = new Stopwatch();
            watch.Start();
            PersonDto dto = new PersonDto();
            //PersonDto personDto = new PersonDto() { Age = "23", Gender = "男", Name = "小李" };
            for (int i = 0; i < 100000; i++)
            {
                //PersonDto dto = ConvertToDto.ConvertTo<Person, PersonDto>(per, personDto);
                dto = mapper.Map(per[0]);
                //dto = Mapper.Map<PersonDto>(per[0]);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }

        public P1 Pitem { get; set; }
    }

    public class P1
    {
        public string Name { get; set; }
        public string Gender { get; set; }
    }

    public class PersonDto
    {

        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }

        public string PitemName { get; set; }
        public string PitemGender { get; set; }

    }

    public class ConvertToDto
    {
        public static To ConvertTo<T, To>(T t, To to)
        {
            Type t2 = to.GetType();
            Type t1 = t.GetType();
            PropertyInfo[] pty = t1.GetProperties();
            PropertyInfo[] pty2 = t2.GetProperties();
            Object obj = Activator.CreateInstance(t2);
            Object objs = Activator.CreateInstance(t1);
            foreach (var propertyInfo in pty)
            {
                foreach (var propertyInfo2 in pty2)
                {
                    if (propertyInfo.Name.Equals(propertyInfo2.Name))
                    {
                        propertyInfo2.SetValue(obj, propertyInfo.GetValue(objs));
                    }
                }
            }
            return to;

        }
    }

}
