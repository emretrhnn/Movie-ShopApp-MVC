using System.Diagnostics;
using System.Linq.Expressions;
using Core.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.EntityFramework.Bases
{
    /// EN: An abstract class that uses the Repository Design Pattern to provide a convenient and central way 
    /// to perform CRUD (Create, Read, Update, and Delete) operations on all entities for database operations.
    public abstract class RepoBase<TEntity> : IRepoBase<TEntity> where TEntity : class, new()
    {
        #region DbContext object constructor injection
        protected readonly DbContext _db;

        protected RepoBase(DbContext db)
        {
            _db = db;
        }
        #endregion

        public virtual IQueryable<TEntity> Query(bool isNoTracking = false)
        {
            if (isNoTracking)
                return _db.Set<TEntity>().AsNoTracking();
            return _db.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, bool isNoTracking = false)
        {
            return Query(isNoTracking).Where(predicate);
        }
        public virtual List<TEntity> GetAll()
        {
            return _db.Set<TEntity>().ToList();
        }

        public virtual IQueryable<TRelationalEntity> Query<TRelationalEntity>() where TRelationalEntity : class, new()
        {
            return _db.Set<TRelationalEntity>();
        }

        public virtual void Add(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Add(entity);
            if (save)
                Save();
        }

        public virtual void Update(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Update(entity);
            if (save)
                Save();
        }

        public virtual void Delete<TRelationalEntity>(Expression<Func<TRelationalEntity, bool>> predicate, bool save = false) where TRelationalEntity : class, new()
        {
            var entities = Query<TRelationalEntity>().Where(predicate).ToList();
            _db.Set<TRelationalEntity>().RemoveRange(entities);
            if (save)
                Save();
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true)
        {
            var entities = Query(predicate).ToList(); // The ToList method is called after the query is created and returns the records that return the query result as a list of objects.
            _db.Set<TEntity>().RemoveRange(entities);

            if (save)
                Save();
        }

        public virtual int Save()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (Exception exc)
            {
                #region Exception handling
                string message = "Exception: " + exc.Message;
                if (exc.InnerException is not null)
                    message += " | Inner Exception: " + exc.InnerException.Message;
                Debug.WriteLine(message);
                #endregion

                throw;
            }
        }

        public void Dispose()
        {
            _db?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
