using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models
{
    public class PorfileUserAction
    {
        private static Dictionary<UserActionType, List<string>> userActionRoles = null;
        
        public static bool IsAuthorized(UserActionType userActionType, List<string> roles)
        {
            // Get the list of roles allowed for a given user action
            List<string> propertyRoles = GetRolesForUserAction(userActionType);

            // Verify whether the user in role allowed to view the property value
            return propertyRoles.Any(role => roles.Contains(role));
        }

        private static List<string> GetRolesForUserAction(UserActionType userActionType)
        {
            // Required to get the Allowd roles for the property from data store.
            if (userActionRoles == null)
            {
                userActionRoles = new Dictionary<UserActionType, List<string>>();

                userActionRoles.Add(UserActionType.AddSpeciality, new List<string> { "CCO" });
            }

            return userActionRoles[userActionType];
        }
    }

    public enum UserActionType
    {
        AddSpeciality
    }
}