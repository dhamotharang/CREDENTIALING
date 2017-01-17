using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Attributes
{
    public class ProfileActionValidator
    {
        public static bool IsAuthorized(ProfileActionType userActionType, List<string> roles, bool containsHashed)
        {
            if (roles == null)
                return true;
            
            // Get the list of roles allowed for a given user action
            var component = new SiteActionsComponent();

            var propertyRoles = component.GetRolesForSiteActions(userActionType, containsHashed);

            // Verify whether the user in role allowed to view the property value
            return propertyRoles.Any(role => roles.Contains(role));

            //return true;
        }

        public static bool IsMasked(List<string> roles)
        { 
            if (roles == null)
                return true;

            // Verify whether the user in role allowed to view the property value
            return roles.Any(r => r.Equals("TL") || r.Equals("CCM") || r.Equals("MGT") || r.Equals("HR"));
        }

        public static bool IsMask(List<string> roles)
        {
            if (roles == null)
                return true;

            bool isPro = roles.Any(r => r.Equals("TL") || r.Equals("CCM") || r.Equals("MGT") || r.Equals("HR") || r.Equals("CCO"));

            // Verify whether the user in role allowed to view the property value
            return roles.Any(r => r.Equals("TL") || r.Equals("CCM") || r.Equals("MGT") || r.Equals("HR") || r.Equals("CCO"));
        }

    }
}