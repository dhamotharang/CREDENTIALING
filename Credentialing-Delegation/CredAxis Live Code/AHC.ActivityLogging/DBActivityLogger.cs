using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.ActivityLogging
{
    internal class DBActivityLogger : IActivityLogger
    {
        public void Log(AuditMessage auditMessage)
        {
            using (DBActivityLoggerContext ctx = new DBActivityLoggerContext())
            {
                try
                {
                    ctx.AuditMessages.Add(auditMessage);
                    ctx.SaveChanges();
                }
                catch (Exception)
                {
                    // suppress any exceptions while logging
                    //throw;
                }
            }
        }
    }

    internal class DBActivityLoggerContext : DbContext
    {
        public DBActivityLoggerContext()
            : base("LogConnection") { }


        public DbSet<AuditMessage> AuditMessages { get; set; }
    }
}
