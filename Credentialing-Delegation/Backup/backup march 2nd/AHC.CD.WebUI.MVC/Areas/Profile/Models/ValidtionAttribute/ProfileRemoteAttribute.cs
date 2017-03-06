using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Unity.Mvc5;
using Microsoft.Practices.Unity.Configuration;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ProfileRemoteAttribute : ValidationAttribute, IClientValidatable
    {
        public string AdditionalField { get; set; }
        public bool IsRequired { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public ProfileRemoteAttribute(string controller, string action, bool isRequired, string additionalField = null) : base()
        {
            this.Controller = controller;
            this.Action = action;
            this.IsRequired = isRequired;
            this.AdditionalField = additionalField;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null && !IsRequired)
                return ValidationResult.Success;

            if (value == null)
                return new ValidationResult(this.ErrorMessage);

            List<object> propValues = new List<object>();
            propValues.Add(value);

            if (!(string.IsNullOrWhiteSpace(this.AdditionalField) || string.IsNullOrEmpty(this.AdditionalField)))
            {
                var additionalFields = this.AdditionalField.Split(',');

                foreach (var item in additionalFields)
                {
                    if (item.Equals("profileId"))
                        propValues.Add(HttpContext.Current.Request[AdditionalField]);
                    else
                        propValues.Add(validationContext.ObjectType.GetProperty(item).GetValue(validationContext.ObjectInstance, null).ToString());
                }
            }
            
            Type controller = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name.Equals(string.Format("{0}Controller", this.Controller)));
            if(controller != null)
            {
                MethodInfo action = controller.GetMethod(string.Format("{0}", this.Action));
                if(action != null)
                {
                    var myContainer = new UnityContainer();
                    myContainer.LoadConfiguration();
                    var instance = myContainer.Resolve(controller);
                    var response = action.Invoke(instance, propValues.ToArray());
                    
                    
                    if (response is JsonResult)
                    {
                        /// error here comes take long time to return data.....
                        object jsonData = ((JsonResult)response).Data;
                        if(jsonData is bool)
                        {
                            return (bool)jsonData ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
                        }

                    }
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule()
            {
                // Use the default DataAnnotationsModelValidator error message.
                // This error message will be overridden by the string returned by
                // IsUID_Available unless "FAIL"  or "OK" is returned in 
                // the Validation Controller.
                ErrorMessage = ErrorMessage,
                ValidationType = "profileremote"
            };

            rule.ValidationParameters["url"] = String.Format(@"/{0}/{1}", Controller, Action);
            rule.ValidationParameters["parametername"] = AdditionalField;
            rule.ValidationParameters["isrequiredproperty"] = IsRequired;
            return new ModelClientValidationRule[] { rule };
        }
    }
}