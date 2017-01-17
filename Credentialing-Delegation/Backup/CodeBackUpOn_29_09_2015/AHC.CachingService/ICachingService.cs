using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CachingService
{
    public interface ICachingService
    {
        T Get<T>(string identity);
        void Set<T>(string identity, T data);
        void Remove(string identity);
    }
}
