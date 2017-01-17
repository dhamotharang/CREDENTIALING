using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PortalTemplate.Areas.Portal.CustomHelpers
{
    static public class TextPhoneNumberFormat
    {
        public static string ConvertToPhoneString(this HtmlHelper Helper, string convertedPhoneString = "")
        {
            if (convertedPhoneString == null ||convertedPhoneString == "")
            {
                return "-";
            }
            if (convertedPhoneString.Length != 10)
            {
                //TODO:log Error
                //once error component is completed by components team,log error
                return convertedPhoneString;
            }
            return '(' + convertedPhoneString.Substring(0, 3) + ')' +' '+ convertedPhoneString.Substring(3, 3) + "-" + convertedPhoneString.Substring(6, 4);
        }
    }
}