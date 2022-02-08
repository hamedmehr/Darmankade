using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Darmankade.Contract;
using Darmankade.Model;

namespace Darmankade.Application
{

    public class Repository<TEntity> : IRepository<TEntity>
            where TEntity : BaseEntity
    {
        public DbContext DBContext { get; set; }
        public Repository(DbContext dBContext)
        {
            DBContext = dBContext;
        }

        public TEntity Cretae(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.IsDeleted = false;

            DBContext.Set<TEntity>().Add(entity);
            try
            {
                DBContext.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool Delete(TEntity entity)
        {
            entity.DeleteDate = DateTime.Now;
            entity.IsDeleted = true;
            return Update(entity) != null;
        }

        public IQueryable<TEntity> FindeByCondition(Expression<Func<TEntity, bool>> expertion)
        {
            return DBContext.Set<TEntity>().Where(expertion).Where(x => x.IsDeleted == false).AsNoTracking();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DBContext.Set<TEntity>().Where(x => x.IsDeleted == false).AsNoTracking();
        }

        public TEntity GetByID(Guid ID)
        {
            return DBContext.Set<TEntity>().FirstOrDefault(x => x.IsDeleted == false && x.ID.Equals(ID));
        }

        public TEntity Update(TEntity entity)
        {
            entity.ModifyDate = DateTime.Now;
            entity.CreateDate = GetByID(entity.ID).CreateDate;

            var en = DBContext.ChangeTracker.Entries<TEntity>().Where(x => x.Entity.ID.Equals(entity.ID)).FirstOrDefault();

            if (en != null && en.Entity != entity)
            {
                DBContext.Entry(en.Entity).CurrentValues.SetValues(entity);
                DBContext.Set<TEntity>().Update(en.Entity);
            }
            else
            {
                DBContext.Set<TEntity>().Update(entity);
            }
            try
            {
                DBContext.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
