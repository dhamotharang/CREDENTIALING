using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NumberGreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        public string DependentProperty { get; set; }

        public bool IsRequired { get; set; }

        public override bool IsValid(object value)
        {
            try
            {
                if (value == null && !IsRequired)
                    return true;

                string dependentProperty = HttpContext.Current.Request[DependentProperty];

                if (value != null && dependentProperty == "")
                    return true;

                return (Convert.ToInt32(value) >= Convert.ToInt32(dependentProperty));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, DependentProperty);
        }

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var model = new ModelClientValidationRule { ValidationType = "numbergreaterthan", ErrorMessage = this.FormatErrorMessage(metadata.DisplayName) };
            model.ValidationParameters.Add("dependentproperty", DependentProperty);
            model.ValidationParameters.Add("isrequiredproperty", IsRequired);

            return new List<ModelClientValidationRule> { model };
        }
    }
}