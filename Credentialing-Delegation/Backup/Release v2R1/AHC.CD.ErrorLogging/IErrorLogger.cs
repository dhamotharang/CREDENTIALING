using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.ErrorLogging
{
    public interface IErrorLogger
    {
        void LogError(Exception exception);
    }
}
