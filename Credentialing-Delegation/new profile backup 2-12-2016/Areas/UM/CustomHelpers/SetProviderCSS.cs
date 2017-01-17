using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;

namespace PortalTemplate.Areas.UM.CustomHelpers
{
    public static class SetProviderCSS
    {
        public static string SetProviderCss(this HtmlHelper Helper,AuthorizationProviderViewModel provider,string TagType)
        {
            string ReturnVal = "";
           if(provider.IsUsePCP)
           {
               if(TagType.ToLower().Equals("readonly"))
               {
                   ReturnVal = "readonly";
               }
               else
               {
                   ReturnVal = "form-control input-xs ProviderSearchDropdown variable_width_providername  mandatory_input_field loser_field read_only_field";
               }
               
           }
           else
           {
               if (TagType.ToLower().Equals("readonly"))
               {
                   ReturnVal = "false";
               }
               else
               {
                   ReturnVal = "form-control input-xs ProviderSearchDropdown variable_width_providername mandatory_input_field loser_field";
               }
              
           }
           return ReturnVal;
        }
    }
}