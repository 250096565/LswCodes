using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 第八章委托._12章匿名方法与迭代器
{
    public class 迭代器
    {
        public void Main()
        {
            Persons pers = new Persons();
            foreach (Person p in pers)
            {
                Console.WriteLine(p.Name);
                ;
            }

            Person per = new Person { Name = "小明" };

            Console.WriteLine( per.Name);
        }
    }


    //Person类
    public class Person
    {

        public string Name { get; set; }

        public Person(string name)
        {
            Name = name;
        }

        public Person()
        {

        }
    }

    public class Persons : IEnumerable
    {
        private Person[] listPerson;

        public Persons()
        {
            listPerson = new Person[]
            {
                new Person("小明"),
                new Person("小红"),
                new Person("小李")
            };
        }
        //索引器
        public Person this[int index] => listPerson[index];

        public int Count => listPerson.Length;


        public IEnumerator GetEnumerator()
        {
            //实现接口
            for (int index = 0; index < listPerson.Length; index++)
            {
                //C# 2.0的实现方法
                yield return listPerson[index];
            }
        }
    }


    //1.0中的实现 
    public class PersonIterator : IEnumerator
    {
        private readonly Persons persons;
        private int index;
        private Person current;


        internal PersonIterator(Persons _persons)
        {
            this.persons = _persons;
            index = 0;
        }

        public bool MoveNext()
        {
            if (index + 1 > persons.Count)
            {
                return false;
            }
            this.current = persons[index];
            index++;
            return true; ;
        }

        public void Reset()
        {
            index = 0;
        }

        public object Current => this.current;

    }
}
