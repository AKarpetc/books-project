using book_editor.data.DB;
using book_editor.data.DB.Models.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.Utility
{
    public class Repository<T> :IRepository<T> where T : BaseTable
    {
        private readonly IDbSet<T> _dbset;
        private readonly BooksContext _dbContext;
        internal virtual IDbSet<T> DbSet => _dbset;
        private readonly IQueryable<T> existDbset;
        public Repository()
        {
            _dbContext = new BooksContext();
            _dbset = _dbContext.Set<T>();
            existDbset = _dbset.Where(x => x.IsDelete == false);
        }

        public void Create(T entity)
        {
            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Added;

            DbSet.Add(entity);
        }
        public void Update(T item)
        {
            var entry = _dbContext.Entry(item);
            DbSet.Attach(item);
            entry.State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            T entity = DbSet.Find(id);

            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Deleted;

            DbSet.Remove(entity);
        }

        public void Delete(T entity)
        {
            var entry = _dbContext.Entry(entity);
            entry.Entity.IsDelete = true;
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var entry = FindAll(predicate);
            Delete(entry);
        }

        public void Delete(IEnumerable<T> entity)
        {
            foreach (var ent in entity)
            {
                var entry = _dbContext.Entry(ent);
                entry.State = EntityState.Deleted;
                DbSet.Remove(ent);
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return existDbset.FirstOrDefault(predicate);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return existDbset.Where(predicate);
        }

        public T Get(int id)
        {
            return existDbset.FirstOrDefault(x=>x.Id==id);
        }

        public IQueryable<T> GetCollection()
        {
            return existDbset;
        }
        public IQueryable<T> GetCollectionWithDeleted()
        {
            return DbSet;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return existDbset.Where(predicate);
        }
    }
}
