using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.CustomHelpers
{
    
    /// <summary>
    /// Author: Venkat
    /// Date: 13/01/2015
    /// Custom HTML Helper for masking the Model Property value based on the allowed roles.
    /// Usage:  @Html.MaskField("Property Name", Property Value)
    /// Returns Masked string data
    /// Note: In cshtml file need to use the namespace for custom helper ex: @using AHC.CD.WebUI.MVC.CustomHelpers
    /// </summary>
    public static class FieldMaskHelper
    {
        public static string MaskField(this HtmlHelper htmlHelper, string propertyName, string propertyValue)
        {
            // Get the list of roles allowed for a given property
            List<string> propertyRoles = GetRolesForProperty(propertyName);

            // Verify whether the user in role allowed to view the property value
            bool isAllowed = propertyRoles.Any(role => htmlHelper.ViewContext.HttpContext.User.IsInRole(role));

            // Mask the property value if user in role not allowed to view
            if (!isAllowed)
            {
                // Need to provide the masking string
                propertyValue = "!@#!@#!@#!@#";
            }
            return propertyValue;
        }

        private static List<string> GetRolesForProperty(string propertyName)
        {
            
            // Required to get the Allowd roles for the property from data store.
            Dictionary<string, List<string>> propertyRoles = new Dictionary<string, List<string>>();
            propertyRoles.Add("SecretCode1", new List<string> { "admin", "hr", "guest" });
            propertyRoles.Add("SecretCode2", new List<string> { "admin" });
            propertyRoles.Add("SecretCode3", new List<string> { "hr" });
            return propertyRoles[propertyName];
        }
    }
}