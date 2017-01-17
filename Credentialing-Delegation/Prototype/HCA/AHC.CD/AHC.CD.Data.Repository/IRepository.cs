using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<TObject>> GetAllAsync<TObject>() where TObject : class;
        Task<IEnumerable<TObject>> GetAsync<TObject>(Expression<Func<TObject, bool>> predicate) where TObject : class;
    }
}
