using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.UM.CustomHelpers
{
   static public class AuthorizationCustomFormat
    {
        public static string FormatLevelRate(this HtmlHelper Helper, string levelRate = "")
        {
            if (levelRate == null || levelRate == "")
            {
                return "-/-";
            }
            switch (levelRate.ToUpper())
            {
                case "LEVEL 1":
                    return levelRate.ToUpper() + "/150";
                case "LEVEL 2":
                    return levelRate.ToUpper() + "/250";
                case "LEVEL 3":
                    return levelRate.ToUpper() + "/350";
                case "LEVEL 4":
                    return levelRate.ToUpper() + "/450";
                case "LEVEL 5":
                    return levelRate.ToUpper() + "/550";
                default:
                    return levelRate.ToUpper();
            }
        }
    }
}