using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 仓储
{
    public class UnitOfWorkContextBase:IUnitOfWork
    {

        //public abstract IEnumerable<DbContext> 

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            throw new NotImplementedException();
        }

        public void Save<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
