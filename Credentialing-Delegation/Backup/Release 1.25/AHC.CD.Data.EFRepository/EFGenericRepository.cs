using AHC.CD.Data.Repository;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
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

        protected DbContext DbContext
        {
            get { return context; }
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

        public virtual async Task<TObject> FindAsync(Expression<Func<TObject, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<TObject> query = DbSet;

            foreach (var includeProperty in includeProperties)
            { query = query.Include(includeProperty); }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<TObject> query = DbSet;

            foreach (var includeProperty in includeProperties)
            { query = query.Include(includeProperty); }

            return query.FirstOrDefault(predicate);
        }

        public virtual TObject Create(TObject entry)
        {
            var newEntry = DbSet.Add(entry);
            //Save();
            return newEntry;

        }

        public IEnumerable<TObject> CreateRange(IEnumerable<TObject> tObjects)
        {
            tObjects = DbSet.AddRange(tObjects);
            return tObjects;
        }

        public virtual void Update(TObject entry)
        {
            DbSet.Attach(entry);
            context.Entry(entry).State = EntityState.Modified;
            //Save();
        }

        public virtual bool Any(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TObject, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
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
            catch (DbEntityValidationException ex)
            {
                throw new DatabaseValidationException(String.Join(", ", ex.EntityValidationErrors.SelectMany(m => m.ValidationErrors).Select(e => e.ErrorMessage)),
                                                        ExceptionMessage.DATABASE_VALIDATION_EXCEPTION,
                                                        ex);
            }
            catch (DbUpdateException ex)
            {
                var sqlException = (System.Data.SqlClient.SqlException)ex.InnerException.InnerException;

                var errors = new List<string>();
                foreach (SqlError item in sqlException.Errors)
                {
                    errors.Add(item.Message.Replace("Cannot insert duplicate key row in object 'dbo.", ""));
                }
                
                if(errors.Count > 1)
                    errors.RemoveAt(errors.Count - 1);

                throw new DatabaseValidationException(String.Join(", ", errors), ExceptionMessage.DATABASE_VALIDATION_EXCEPTION, ex);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
