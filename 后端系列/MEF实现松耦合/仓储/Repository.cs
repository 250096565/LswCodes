using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 仓储
{
    /// <summary>
    /// 数据仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Export(typeof(IRepository<BaseEntity>))]
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        [Import]
        protected IUnitOfWork Context { get; set; }

        //注册MEF
        public Repository()
        {
            Register.Regisgter().ComposeParts(this);
        }

        public T Save(T entity)
        {
            if (entity == null) throw new ArgumentException("entity null");
            Context.Save(entity);
            return entity;
        }
    }

    public static class Register
    {
        public static CompositionContainer Regisgter()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            return container;
        }
    }
}
