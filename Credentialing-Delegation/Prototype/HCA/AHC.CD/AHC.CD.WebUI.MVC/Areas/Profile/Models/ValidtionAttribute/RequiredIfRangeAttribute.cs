using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class RequiredIfMonthGreaterThan : ValidationAttribute, IClientValidatable
    {
        public string StartDependentProperty { get; set; }
        public string EndDependentProperty { get; set; }
        public int Range { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                var startPropertyInfo = validationContext.ObjectType.GetProperty(this.StartDependentProperty);
                var endPropertyInfo = validationContext.ObjectType.GetProperty(this.EndDependentProperty);



                // Let's check that otherProperty is of type DateTime as we expect it to be
                if (startPropertyInfo.PropertyType.Equals(new DateTime().GetType()) && endPropertyInfo.PropertyType.Equals(new DateTime().GetType()))
                {
                    string toValidate = (string)value;
                    DateTime referenceStartProperty = (DateTime)startPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                    DateTime referenceEndProperty = (DateTime)endPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                    // if the end date is lower than the start date, than the validationResult will be set to false and return
                    // a properly formatted error message


                    if ((String.IsNullOrEmpty(toValidate) && ((((referenceEndProperty.Year - referenceStartProperty.Year) * 12) + referenceEndProperty.Month - referenceStartProperty.Month) >= Range)))
                    {
                        validationResult = new ValidationResult(ErrorMessageString);
                    }
                }
                else
                {
                    validationResult = new ValidationResult("An error occurred while validating the property. Property is not of type DateTime");
                }
            }
            catch (Exception ex)
            {
                // Do stuff, i.e. log the exception
                // Let it go through the upper levels, something bad happened
                throw ex;
            }

            return validationResult;
        }

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var model = new ModelClientValidationRule { ValidationType = "requiredifmonthgreaterthan", ErrorMessage = this.ErrorMessage };
            model.ValidationParameters.Add("startdateproperty", StartDependentProperty);
            model.ValidationParameters.Add("enddateproperty", EndDependentProperty);
            model.ValidationParameters.Add("range", Range);

            return new List<ModelClientValidationRule> { model };
        }
    }
}