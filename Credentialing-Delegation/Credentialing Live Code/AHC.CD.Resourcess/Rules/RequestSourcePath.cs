using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AHC.CD.Resources.Rules
{
    public static class RequestSourcePath
    {
        public static string RequestSource = ConfigurationManager.AppSettings["RequestSourcePath"].ToString();
    }
}
