using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using AutoMapper;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Reflection;

namespace DTO转换测试
{
    interface IPersonService
    {
        List<PersonDto> GetAll();
    }
    class PersonService : IPersonService
    {
        public List<PersonDto> GetAll()
        {
            return new List<PersonDto>() { new PersonDto() { Age = "23", Gender = "男", Name = "小明" } };
        }
    }

    class Program
    {
        public IPersonService Service { get; set; }

        public Program(IPersonService s)
        {
            IocManager ioc = new IocManager();

            ioc.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            this.Service = s;
        }

        static void Main(string[] args)
        {
            new AutoConfig().Config();
            //Mapper.CreateMap<Person, PersonDto>();
            PersonDto qqq = new PersonDto();
            Mapper.Initialize(o =>
            {
                o.CreateMap(typeof(Person), typeof(PersonDto));
                o.CreateMap(typeof(PersonDto), typeof(Person));
            });



            //Mapper.Initialize(o => o.CreateMap<PersonDto, Person>().ForMember(entity => entity.Name, opt => opt.Ignore()));

            //var mapper = ObjectMapperManager.DefaultInstance.GetMapper<Person, PersonDto>();

            //List<Person> per = new List<Person>() { new Person() { Age = "23", Gender = "男", Name = "小明" } };
            // Person person = new Person() { Age = "23", Gender = "男", Name = "小明" };
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //PersonDto dto = person.MapTo<PersonDto>();//<PersonDto>();

            PersonDto personDto = new PersonDto() { Age = "23", Gender = "男", Name = "小明" };
            var per = Mapper.Map<Person>(personDto);
            /* for (int i = 0; i < 100000; i++)
             {
                 //PersonDto dto = ConvertToDto.ConvertTo<Person, PersonDto>(per, personDto);
                 dto = mapper.Map(per[0]);
                 //dto = Mapper.Map<PersonDto>(per[0]);
             }*/
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


    }


    public class Pitem
    {
        public string Name { get; set; }
        public string Gender { get; set; }
    }
    
    public class PersonDto
    {

        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }

        //public string PitemName { get; set; }
        //public string PitemGender { get; set; }

    }

    public class AutoMapperAttribute : Attribute
    {
        public static List<Type> From { get; set; }
        public static List<Type> To { get; set; }

        public AutoMapperAttribute(Type from, Type to)
        {
            //From.Add(from);
            // To.Add(to);
        }
    }

    public class AutoConfig
    {
        ITypeFinder _typeFinder = new TypeFinder();

        public void Config()
        {
            var types = _typeFinder.Find(type =>
                type.IsDefined(typeof(AutoMapperAttribute))
                );

            foreach (var type in types)
            {
                var obj = type.GetCustomAttributes<AutoMapperAttribute>();

                /*if (obj.Length > 0)
                {
                    AutoMapperAttribute auto = (AutoMapperAttribute)obj[0];
                }*/
            }
        }
    }

    public class Idto
    {

    }

}
