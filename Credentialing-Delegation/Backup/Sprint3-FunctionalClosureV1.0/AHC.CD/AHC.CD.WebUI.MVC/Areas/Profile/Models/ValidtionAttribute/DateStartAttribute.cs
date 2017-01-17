using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class DateStartAttribute : ValidationAttribute, IClientValidatable
    {
        public string MaxPastYear { get; set; }
        public string MinPastYear { get; set; }
        public bool IsRequired { get; set; }

        private DateTime EligibleMaxDate 
        {
            get 
            {
                if (MaxPastYear == null)
                    return DateTime.MinValue;
                
                return DateTime.Now.AddYears(Convert.ToInt32(MaxPastYear));
            }
        }

        private DateTime EligibleMinDate
        {
            get
            {
                if (MinPastYear == null)
                    return DateTime.MaxValue;

                return DateTime.Now.AddYears(Convert.ToInt32(MinPastYear));
            }
        }

        public override bool IsValid(object value)
        {
            if (value == null && !IsRequired)
                return true;
            
            if (value == null)
                return false;

            DateTime dateStart = (DateTime)value;
            // Meeting must start in the future time.
            return (dateStart.Date >= EligibleMinDate.Date && dateStart.Date <= EligibleMaxDate.Date );
        }

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var model = new ModelClientValidationRule { ValidationType = "startdate", ErrorMessage = this.ErrorMessage };
            model.ValidationParameters.Add("maxdate", EligibleMaxDate.Date);
            model.ValidationParameters.Add("mindate", EligibleMinDate.Date);
            model.ValidationParameters.Add("isrequiredproperty", IsRequired);

            return new List<ModelClientValidationRule> { model };
        }
    }
}