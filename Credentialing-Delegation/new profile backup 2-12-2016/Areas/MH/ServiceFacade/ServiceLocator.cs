using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.ServiceFacade
{
    public class ServiceLocator
    {
        public string Locate(string parameter)
        {
            string locator;
            switch (parameter)
            {
                case "Member":
                    locator = "MemberServiceWebAPIURL";
                    break;
                case "CMS":
                    locator = "CMSServiceWebAPIURL";
                    break;
                case "Provider":
                    locator = "ProviderServiceWebAPIURL";
                    break;
                case "Facility":
                    locator = "FacilityDataServiceWebAPIURL";
                    break;
                default:
                    locator = "";
                    break;
            }
            return locator;
        }
    }
}