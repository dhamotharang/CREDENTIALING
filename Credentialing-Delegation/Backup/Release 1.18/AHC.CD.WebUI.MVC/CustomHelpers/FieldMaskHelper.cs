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
    /// Note: In cshtml file need to use the namespace for custom helper ex: @using AHC.CD.WebUI.MVC.CustomHelpers or need to add the namespace in views web.config file
    /// </summary>
    public static class FieldMaskHelper
    {
        public static string MaskField(this HtmlHelper htmlHelper, MaskProperty propertyName, string propertyValue)
        {
            
            // Get the list of roles allowed for a given property
            List<string> propertyRoles = GetRolesForProperty(propertyName);

            // Verify whether the user in role allowed to view the property value
            bool isAllowed = propertyRoles.Any(role => htmlHelper.ViewContext.HttpContext.User.IsInRole(role));

            // Mask the property value if user in role not allowed to view
            if (!isAllowed)
            {
                // Need to provide the masking string
                propertyValue = "######";
            }
            return propertyValue;
        }
        
        private static List<string> GetRolesForProperty(MaskProperty propertyName)
        {
            
            // Required to get the Allowd roles for the property from data store.
            Dictionary<MaskProperty, List<string>> propertyRoles = new Dictionary<MaskProperty, List<string>>();
            
            propertyRoles.Add(MaskProperty.CAQH, new List<string> { "PROVIDER", "HR", "CREDENTIALINGADMIN" });
            propertyRoles.Add(MaskProperty.NPI, new List<string> { "PROVIDER", "HR", "CREDENTIALINGADMIN" });
            propertyRoles.Add(MaskProperty.UPIN, new List<string> { "PROVIDER", "HR", "CREDENTIALINGADMIN" });
            propertyRoles.Add(MaskProperty.USMILE, new List<string> { "PROVIDER", "HR", "CREDENTIALINGADMIN" });
            propertyRoles.Add(MaskProperty.VISA, new List<string> { "PROVIDER", "HR", "CREDENTIALINGADMIN" });
            propertyRoles.Add(MaskProperty.GREENCARD, new List<string> { "PROVIDER", "HR", "CREDENTIALINGADMIN" });
            propertyRoles.Add(MaskProperty.NATIONALID, new List<string> { "PROVIDER", "HR", "CREDENTIALINGADMIN" });
            propertyRoles.Add(MaskProperty.ADDRESS, new List<string> { "PROVIDER", "HR", "CREDENTIALINGADMIN" });
            propertyRoles.Add(MaskProperty.SSN, new List<string> { "PROVIDER", "HR", "CREDENTIALINGADMIN" });
            
            return propertyRoles[propertyName];
        }
    }

    public enum MaskProperty
    {
        CAQH,
        NPI,
        UPIN,
        USMILE, 
        VISA,
        GREENCARD,
        NATIONALID, 
        ADDRESS, 
        SSN,
    }
}