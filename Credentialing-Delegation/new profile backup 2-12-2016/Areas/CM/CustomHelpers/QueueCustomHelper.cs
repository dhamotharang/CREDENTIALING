using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace PortalTemplate.Areas.CM.CustomHelpers
{
    static public class QueueCustomHelper
    {
        // ----Method to get the Tick Mark/Cross Mark on the basis of Either True or False in Queue----
        public static IHtmlString GetDataIcon(this HtmlHelper helper, bool queueBool)
        {
            string htmlString = null;
            //string htmlString = String.Format("<label class='{0}'>{1} &nbsp;<i class='fa fa-sort facility-queue-sort'></i></label><span class='styled-input'> <span></span></span>", ClassNames, LabelName);
            
            if (queueBool)
            {
                htmlString = String.Format("<label class='label label-success'><i class='fa fa-check'></i></label>");
            }
            else
            {
                htmlString = String.Format("<label class='label label-warning'><i class='fa fa-times'></i></label>");
            }

            return new HtmlString(htmlString);

        }
    }
}