using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository
{
    /// <summary>
    /// Author: Venkat
    /// Date:   20/10/2014
    /// Generic Repository for Basic CRUD Operations
    /// All Repository classes should implement this interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        
            
            Task<IEnumerable<T>> GetAllAsync();
            IEnumerable<T> GetAll();


            IEnumerable<T> GetAll(string includeProperties = "");
            Task<IEnumerable<T>> GetAllAsync(string includeProperties = "");
        
            Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
            IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        
            IEnumerable<T> Get(Expression<Func<T, bool>> predicate, string includeProperties = "");
            Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, string includeProperties = "");

            T Find(params object[] keys);
            Task<T> FindAsync(params object[] keys);

            Task<T> FindAsync(Expression<Func<T, bool>> predicate);
            T Find(Expression<Func<T, bool>> predicate);

            Task<T> FindAsync(Expression<Func<T, bool>> predicate, string includeProperties = "");
            T Find(Expression<Func<T, bool>> predicate, string includeProperties = "");

            Task<T> FindAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);
            T Find(Expression<Func<T, bool>> predicate, params string[] includeProperties);

            bool Any(Expression<Func<T, bool>> predicate);
            Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

            T Create(T t);

            void Update(T t);

            void Delete(object id);

            void Delete(T t);

            Task<int> SaveAsync();
            int Save();

            Task<int> CountAsync();
            int Count();

    }
 }

