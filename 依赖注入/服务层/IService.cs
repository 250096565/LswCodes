using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 服务层
{
    public interface IService<T>
    {
        bool Add(T model);
        bool Update(T model);
        bool Delete(int id);
        List<T> Select();
    }
}
