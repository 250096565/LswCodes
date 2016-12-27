using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 仓储
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        void RollBack();
        void Save<T>(T entity) where T : BaseEntity;
    }

}
