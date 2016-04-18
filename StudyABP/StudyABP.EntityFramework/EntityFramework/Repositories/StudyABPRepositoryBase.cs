using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace StudyABP.EntityFramework.Repositories
{
    public abstract class StudyABPRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<StudyABPDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected StudyABPRepositoryBase(IDbContextProvider<StudyABPDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class StudyABPRepositoryBase<TEntity> : StudyABPRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected StudyABPRepositoryBase(IDbContextProvider<StudyABPDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
