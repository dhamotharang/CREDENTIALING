using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.UM.CustomHelpers
{
    static public class QueueTableHeaderHelper
    {
        public static IHtmlString QueueTableHeaderLabelFor(this HtmlHelper Helper,string LabelName, string ClassNames)
        {
            string htmlString = String.Format("<label class='{0}'>{1} &nbsp;<i class='fa fa-sort facility-queue-sort'></i></label><span class='styled-input'> <span></span></span>", ClassNames, LabelName);
            return new HtmlString(htmlString);
        }
        public static string ConvertToDateString(this HtmlHelper Helper, string convertedDateString="")
        {
            if(convertedDateString =="" ){
                return "";
            }
            return convertedDateString.Split(' ')[0];
        }
    }
}