using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 实体层;
using 服务层;

namespace 服务层
{
    public class ProductService : IService<Person>
    {


        List<Person> listPerson = new List<Person>() {
            new Person() { Id=1,Name="追风筝的人", Age="21"},
            new Person() { Id=2,Name="天涯的海风", Age="5"},
            new Person() { Id=3,Name="风铃", Age="18"},
            new Person() { Id=4,Name="小娜", Age="20"}
        };
        public bool Add(Person model)
        {
            listPerson.Add(model);
            return true;
        }

        public bool Delete(int id)
        {
            listPerson.Remove(listPerson.Where(o => o.Id == id).FirstOrDefault());
            return true;
        }

        public List<Person> Select()
        {
            return listPerson;
        }

        public bool Update(Person model)
        {
            listPerson.Remove(model);
            listPerson.Add(model);
            return true;
        }
    }
}
