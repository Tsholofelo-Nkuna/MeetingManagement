using MeetingManagement.DAL;
using MeetingManagement.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace MeetingManagement.Repository
{
    public interface IRepository<TEntity>
    {
        TEntity? FindById(long id);
        long Add(TEntity entity);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter);
        long Update(TEntity entity);
        bool Delete(long Id);
    }
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity<long>
    {
        private readonly DbSet<TEntity> _set;
        private readonly WebDbContext _webDbContext;
        public GenericRepository(WebDbContext dbContext) {
            this._webDbContext = dbContext;
            this._set = this._webDbContext.Set<TEntity>();
        }

        public long Add(TEntity entity)
        {
            this._set.Add(entity);
            this._webDbContext.SaveChanges();
            return entity.Id;

        }

        public TEntity? FindById(long id)
        {
             return _set.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter)
        {
            return this._set.AsNoTracking().Where(filter); 
        }

        public long Update(TEntity entity)
        {
            this._set.Update(entity);
            this._webDbContext.SaveChanges();
            return entity.Id;
        }

        public bool Delete(long id)
        {
            var toBeDeleted = this._set.FirstOrDefault(x => x.Id == id);
            if (toBeDeleted != null)
            {
                this._webDbContext.Remove(toBeDeleted);
                return this._webDbContext.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
