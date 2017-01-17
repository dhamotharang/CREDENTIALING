using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.ActivityLogging
{
    public class ActivityLogFactory
    {
        public static readonly ActivityLogFactory Instance = new ActivityLogFactory();
        private ActivityLogFactory(){}
        public IActivityLogger GetActivityLogger()
        {
            return new DBActivityLogger();
        }
    }
}
