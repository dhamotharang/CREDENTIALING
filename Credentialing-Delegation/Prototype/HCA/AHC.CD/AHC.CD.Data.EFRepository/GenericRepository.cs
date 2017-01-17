using AHC.CD.Data.Repository;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository
{
    class GenericRepository : IRepository
    {
        private EFEntityContext context = null;

        public GenericRepository()
        {
            context = new EFEntityContext();
        }
        
        public async Task<IEnumerable<TObject>> GetAllAsync<TObject>() where TObject : class
        {
            return await new EFGenericRepository<TObject>().GetAllAsync();
        }

        public async Task<IEnumerable<TObject>> GetAsync<TObject>(System.Linq.Expressions.Expression<Func<TObject, bool>> predicate) where TObject : class
        {
            return await new EFGenericRepository<TObject>().GetAsync(predicate);
        }
    }
}
