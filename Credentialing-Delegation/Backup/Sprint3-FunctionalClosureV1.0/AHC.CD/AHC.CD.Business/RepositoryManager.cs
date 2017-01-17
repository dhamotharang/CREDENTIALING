using AHC.CD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business
{
    internal class RepositoryManager : IRepositoryManager
    {
        private IRepository repository = null;

        public RepositoryManager(IUnitOfWork uow)
        {
            this.repository = uow.GetRepository();
        }
        
        public async Task<IEnumerable<TObject>> GetAllAsync<TObject>() where TObject : class
        {
            try
            {
                return await repository.GetAllAsync<TObject>();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<TObject>> GetAsync<TObject>(System.Linq.Expressions.Expression<Func<TObject, bool>> predicate) where TObject : class
        {
            try
            {
                return await repository.GetAsync<TObject>(predicate);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IEnumerable<TObject>> GetAsync<TObject>(System.Linq.Expressions.Expression<Func<TObject, bool>> predicate, string includeProperties = "") where TObject : class
        {
            try
            {
                return await repository.GetAsync<TObject>(predicate, includeProperties);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
