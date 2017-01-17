using AHC.CD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository
{
    /// <summary>
    /// Author: Venkat
    /// Date:   20/10/2014
    /// Implementation of IGenericRepository for Entity Framework CRUD Operations
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    internal class EFGenericRepository<TObject> : IGenericRepository<TObject> where TObject : class
    {
        private DbContext context = null;
        private DbSet<TObject> dbSet = null;

        public EFGenericRepository()
        {
            this.context = new EFEntityContext();
            this.dbSet = this.context.Set<TObject>();
        }
        public EFGenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TObject>();
        }

        public DbSet<TObject> DbSet
        {
            get { return dbSet; }
        }

        public virtual IEnumerable<TObject> GetAll()
        {
            
            
            return DbSet.AsEnumerable<TObject>();
        }

        

       

        public virtual async Task<IEnumerable<TObject>> GetAllAsync()
        {
            return await DbSet.ToListAsync<TObject>();
        }

        public IEnumerable<TObject> GetAll(string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return query.AsEnumerable<TObject>();
        }

        public async Task<IEnumerable<TObject>> GetAllAsync(string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return await query.ToListAsync<TObject>();
        }


        public virtual IEnumerable<TObject> Get(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsEnumerable<TObject>();
        }

        public virtual async Task<IEnumerable<TObject>> GetAsync(Expression<Func<TObject, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync<TObject>();
        }


        public virtual IEnumerable<TObject> Get(Expression<Func<TObject, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;

            query = query.Where(predicate);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return query.AsEnumerable<TObject>();
        }

        public virtual async Task<IEnumerable<TObject>> GetAsync(Expression<Func<TObject, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;

            query = query.Where(predicate);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return await query.ToListAsync<TObject>();
        }

        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual async Task<TObject> FindAsync(params object[] keys)
        {
            return await DbSet.FindAsync(keys);
        }


        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual async Task<TObject> FindAsync(Expression<Func<TObject, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return query.FirstOrDefault(predicate);
        }

        public virtual async Task<TObject> FindAsync(Expression<Func<TObject, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual TObject Create(TObject entry)
        {
            var newEntry = DbSet.Add(entry);
            //Save();
            return newEntry;

        }
        public virtual void Update(TObject entry)
        {
            DbSet.Attach(entry);
            context.Entry(entry).State = EntityState.Modified;
            //Save();
        }

        public virtual void Delete(object id)
        {
            TObject entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TObject entry)
        {
            if (context.Entry(entry).State == EntityState.Detached)
            {
                DbSet.Attach(entry);
            }
            DbSet.Remove(entry);
            //Save();
        }

        public virtual int Count()
        {
            return DbSet.Count();
        }
        public virtual async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        public virtual int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public virtual async Task<int> SaveAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                
                throw;
            }
        }




        
    }

}
