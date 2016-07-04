using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class DbContextFactory
    {
        private static DbContext Context { get; set; }

        private DbContextFactory()
        {

        }

        public static DbContext GetDbContext()
        {
            if (Context == null)
            {
                Context = new StudyEntities();

            }
            return Context;
        }

    }
}
