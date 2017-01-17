using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.ErrorLogging
{
    internal class ElmahErrorLogger : IErrorLogger
    {
        public void LogError(Exception exception)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
        }
    }
}
