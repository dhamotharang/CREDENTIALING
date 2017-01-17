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
        public string MaxPastDays { get; set; }
        public string MinPastDays { get; set; }
        public bool IsRequired { get; set; }

        private DateTime EligibleMaxDate 
        {
            get 
            {
                if (!String.IsNullOrWhiteSpace(MaxPastDays))
                    return DateTime.Now.AddDays(Convert.ToInt32(MaxPastDays));
                if (MaxPastYear == null)
                    return DateTime.MinValue;
                
                return DateTime.Now.AddYears(Convert.ToInt32(MaxPastYear));
            }
        }

        private DateTime EligibleMinDate
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(MinPastDays))
                    return DateTime.Now.AddDays(Convert.ToInt32(MinPastDays)); 
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

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, EligibleMaxDate.Equals(DateTime.MaxValue) ? EligibleMaxDate.ToString("MM/dd/yyyy") : EligibleMaxDate.AddDays(1).ToString("MM/dd/yyyy"), EligibleMinDate.Equals(DateTime.MinValue) ? EligibleMinDate.ToString("MM/dd/yyyy") : EligibleMinDate.AddDays(-1).ToString("MM/dd/yyyy"));
        } 

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var model = new ModelClientValidationRule { ValidationType = "startdate", ErrorMessage = this.FormatErrorMessage(metadata.DisplayName) };
            model.ValidationParameters.Add("maxdate", EligibleMaxDate.Date);
            model.ValidationParameters.Add("mindate", EligibleMinDate.Date);
            model.ValidationParameters.Add("isrequiredproperty", IsRequired);

            return new List<ModelClientValidationRule> { model };
        }
    }
}