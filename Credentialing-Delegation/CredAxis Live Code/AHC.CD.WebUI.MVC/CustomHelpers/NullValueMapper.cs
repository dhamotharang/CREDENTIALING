using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.CustomHelpers
{
    public class NullValueMapper
    {
        public string NullStringMap(string val, string returnValue)
        {
            if (val == null || val == "")
                return returnValue;
            else
                return val.ToString();
        }
    }
}