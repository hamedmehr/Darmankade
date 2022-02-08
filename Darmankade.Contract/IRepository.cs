using System;
using System.Linq;
using System.Linq.Expressions;

namespace Darmankade.Contract
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        TEntity GetByID(Guid ID);
        IQueryable<TEntity> FindeByCondition(Expression<Func<TEntity, bool>> expertion);
        TEntity Cretae(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(TEntity entity);

    }
}
