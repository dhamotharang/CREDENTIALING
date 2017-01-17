using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Billing.Validation.Attributes
{
    public class ValidateFileAttribute : ValidationAttribute,IClientValidatable
    {
        string otherPropertyName;     
        string extensionErrorMessage;
        string sizeErrorMessage;
        public ValidateFileAttribute(string otherPropertyName, string errorMessage, string extensionErrorMessage,string sizeErrorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
            this.sizeErrorMessage = sizeErrorMessage;
            this.extensionErrorMessage = extensionErrorMessage;
        }
        public override bool IsValid(object value)
        {
            //int maxContent = 1024 * 1024; //1 MB
            //string[] sAllowedExt = new string[] { ".jpg", ".gif", ".png" };


            //var file = value as HttpPostedFileBase;

            //if (file == null)
            //    return false;
            //else if (!sAllowedExt.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            //{
            //    ErrorMessage = extensionErrorMessage + string.Join(", ", sAllowedExt);
            //    return false;
            //}
            //else if (file.ContentLength > maxContent)
            //{
            //    ErrorMessage = sizeErrorMessage + (maxContent / 1024).ToString() + "MB";
            //    return false;
            //}
            //else
            //    return true;
            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            //string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
            string errorMessage = ErrorMessageString;

            // The value we set here are needed by the jQuery adapter
            ModelClientValidationRule validateFileRule = new ModelClientValidationRule();
            validateFileRule.ErrorMessage = errorMessage;
          
            validateFileRule.ValidationType = "validatefile"; // This is the name the jQuery adapter will use
            //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
            validateFileRule.ValidationParameters.Add("otherpropertyname", otherPropertyName);
            validateFileRule.ValidationParameters.Add("sizeerrormsg", sizeErrorMessage);
            validateFileRule.ValidationParameters.Add("exterrormsg",extensionErrorMessage);

            yield return validateFileRule;

            
        }
    }
}