using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Billing.Validation.Attributes
{
    public class CheckLengthRangeAttribute : ValidationAttribute, IClientValidatable
    {
        public CheckLengthRangeAttribute(string errMsg):base(errMsg)
        {
                
        }

        public override bool IsValid(object value)
        {
            return true;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            
            string errorMessage = ErrorMessageString;

            // The value we set here are needed by the jQuery adapter
            ModelClientValidationRule checkLengthRangeRule = new ModelClientValidationRule();
            checkLengthRangeRule.ErrorMessage = errorMessage;

            checkLengthRangeRule.ValidationType = "checklengthrange"; // This is the name the jQuery adapter will use
            //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
            //checkLengthRangeRule.ValidationParameters.Add("otherpropertyname", otherPropertyName);
            //checkLengthRangeRule.ValidationParameters.Add("sizeerrormsg", sizeErrorMessage);
            //checkLengthRangeRule.ValidationParameters.Add("exterrormsg", extensionErrorMessage);

            yield return checkLengthRangeRule;
        }
    }
}