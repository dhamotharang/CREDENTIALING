using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class DateEndAttribute : ValidationAttribute, IClientValidatable
    {
        public string DateStartProperty { get; set; }
        public string MaxYear { get; set; }
        public bool IsRequired { get; set; }
        public bool IsGreaterThan { get; set; }

        private DateTime EligibleMaxDate
        {
            get
            {
                if (MaxYear == null)
                    return DateTime.MaxValue;

                return DateTime.Now.AddYears(Convert.ToInt32(MaxYear));
            }
        }

        public override bool IsValid(object value)
        {
            if (value == null && !IsRequired)
                return true;
            
            // Get Value of the DateStart property
            string dateStartString = HttpContext.Current.Request[DateStartProperty];

            DateTime dateEnd = (DateTime)value;
            DateTime dateStart = DateTime.ParseExact(dateStartString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //DateTime dateStart = DateTime.ParseExact(dateStartString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Meeting start time must be before the end time

            if(IsGreaterThan)
                return dateEnd.Date > dateStart.Date && dateEnd.Date <= EligibleMaxDate.Date;
            else
                return dateEnd.Date >= dateStart.Date && dateEnd.Date <= EligibleMaxDate.Date;
        }

        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    ValidationResult validationResult = ValidationResult.Success;
        //    try
        //    {
        //        if (value == null && !IsRequired)
        //            return validationResult;

        //        // Using reflection we can get a reference to the other date property, in this example the project start date
        //        var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.DateStartProperty);

        //        // Let's check that otherProperty is of type DateTime as we expect it to be
        //        if (otherPropertyInfo.PropertyType.Equals(new DateTime().GetType()))
        //        {
        //            DateTime toValidate = (DateTime)value;
        //            DateTime referenceProperty = (DateTime)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

        //            // if the end date is lower than the start date, than the validationResult will be set to false and return
        //            // a properly formatted error message
        //            if (toValidate.CompareTo(referenceProperty) < 1 && toValidate.CompareTo(EligibleMaxDate) > 1)
        //            {
        //                validationResult = new ValidationResult(ErrorMessageString);
        //            }
        //        }
        //        else
        //        {
        //            validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTime");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Do stuff, i.e. log the exception
        //        // Let it go through the upper levels, something bad happened
        //        throw ex;
        //    }

        //    return validationResult;
        //}

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, EligibleMaxDate.Equals(DateTime.MaxValue) ? EligibleMaxDate.ToString("MM/dd/yyyy") : EligibleMaxDate.AddDays(1).ToString("MM/dd/yyyy"));
        } 

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var model = new ModelClientValidationRule { ValidationType = "enddate", ErrorMessage = this.FormatErrorMessage(metadata.DisplayName) };
            model.ValidationParameters.Add("enddateproperty", DateStartProperty);
            model.ValidationParameters.Add("maxdate", EligibleMaxDate.Date);
            model.ValidationParameters.Add("isrequiredproperty", IsRequired);
            model.ValidationParameters.Add("isgreaterthan", IsGreaterThan);

            return new List<ModelClientValidationRule> { model };
        }
    }
}