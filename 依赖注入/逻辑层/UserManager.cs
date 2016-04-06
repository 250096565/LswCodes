using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 实体层;
using 服务层;

namespace 逻辑层
{
    public class UserManager
    {
        private IService<Person> service;
        public UserManager(IService<Person> IService)
        {
            this.service = IService;
        }

        public bool Add(Person model)
        {
            return service.Add(model);
        }


        public List<Person> Query()
        {
            return service.Select();
        }

        public bool Modify(Person model)
        {
            return service.Update(model);
        }

        public bool Del(int id)
        {
            return service.Delete(id);
        }
    }
}
