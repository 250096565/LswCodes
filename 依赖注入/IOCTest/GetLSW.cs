using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCTest
{
    /// <summary>
    /// 寻找梁士伟
    /// </summary>
    public class GetLSW
    {

        private ILSWFinder finder;
        public GetLSW(ILSWFinder finder)
        {
            this.finder = finder;
        }

        public List<Person> Get()
        {
            var list = finder.FindAll();

            return list.Where(o => o.Name == "梁士伟").ToList();
        }



    }


    /// <summary>
    /// 在家里找
    /// </summary>
    public class GetLSWbyHome : ILSWFinder
    {
        public List<Person> FindAll()
        {
            return new List<Person>() {
               new Person { Name="小娜"},
               new Person { Name="追风筝的人"},
               new Person { Name="梁士伟"}
           };
        }
    }


    /// <summary>
    /// 在公司找
    /// </summary>
    public class GetLSWbyWork : ILSWFinder
    {
        public List<Person> FindAll()
        {
            return new List<Person>() {
               new Person { Name="天涯的海风"},
               new Person { Name="风铃"},
               new Person { Name="梁士伟"}, new Person { Name="梁士伟"}
           };
        }
    }


    //接口
    public interface ILSWFinder { List<Person> FindAll(); }
    //人类
    public class Person
    {
        public string Name { get; set; }
    }
}
