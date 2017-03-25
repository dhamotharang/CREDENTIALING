using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.ActivityLogging
{
    public interface IActivityLogger
    {
        void Log(AuditMessage auditMessage);
    }
}
