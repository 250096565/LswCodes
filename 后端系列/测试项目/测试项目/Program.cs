using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 测试项目
{
    class Program
    {
        private static bool _isDone = false;
        static SemaphoreSlim _sem = new SemaphoreSlim(3);    // 我们限制能同时访问的线程数量是3
        public static List<Person> Persons = new List<Person>()
        {
            new Person() {Id = 1, Status = true, Name = "小明"},
            new Person() {Id = 2, Status = true, Name = "小红"},
            new Person() {Id = 3, Status = true, Name = "小绿"},
            new Person() {Id = 4, Status = true, Name = "小兰"},
            new Person() {Id = 5, Status = true, Name = "小李"},
            new Person() {Id = 6, Status = true, Name = "小王"},
        };

        static void Main(string[] args)
        {
            for (int i = 0; i < 6; i++) new Thread(Enter).Start(i);
            Console.WriteLine(Persons);

        }

        static void Enter(object i)
        {
            if (!_isDone)
            {
                var person = Persons.FirstOrDefault(o => o.Status);
                person.Name = person.Name + (int)i;
                person.Status = false;
                _isDone = true;
            }

        }
    }

    public class Person
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
    }
}
