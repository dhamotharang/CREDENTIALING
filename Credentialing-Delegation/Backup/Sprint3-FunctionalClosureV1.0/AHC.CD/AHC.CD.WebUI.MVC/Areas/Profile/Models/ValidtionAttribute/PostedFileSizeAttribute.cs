using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PostedFileSizeAttribute : ValidationAttribute, IClientValidatable
    {
        public int AllowedSize { get; set; }
        public bool IsRequired { get; set; }

        public override bool IsValid(object value)
        {
            try
            {
                if (value == null && !IsRequired)
                    return true;
                
                HttpPostedFileBase file = value as HttpPostedFileBase;
                if (file != null)
                {
                    var fileName = file.FileName;
                    if (file.ContentLength > AllowedSize)
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var model = new ModelClientValidationRule { ValidationType = "postedfilesize", ErrorMessage = this.ErrorMessage };
            model.ValidationParameters.Add("allowedfilesize", AllowedSize);
            model.ValidationParameters.Add("isrequiredproperty", IsRequired);
            model.ValidationParameters.Add("propertyname", metadata.PropertyName);
            return new List<ModelClientValidationRule> { model };
        }
    }
}