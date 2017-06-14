using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.CustomHelpers
{
    public class DateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var convertedValue = ConvertToDateString(value.AttemptedValue.ToString());
            var date = DateTime.ParseExact(convertedValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //var date = DateTime.ParseExact(convertedValue, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

            return date;
        }

        private string ConvertToDateString(string date)
        {
            if (date != null)
            {
                string format = "MM/dd/yyyy";
                //string format = "M/d/yyyy h:mm:ss tt";
               //Regex r = new Regex(@"\d{1,2}/\d{1,2}/\d{4}.*");
                 Regex r = new Regex(@"\d{2}/\d{2}/\d{4}");

                var isValid = r.IsMatch(date);

                if (isValid)
                {
                    return date;
                }
                else
                {
                    System.Globalization.DateTimeFormatInfo dti = new System.Globalization.DateTimeFormatInfo();
                    DateTime convertedDate = Convert.ToDateTime(date).Date;
                    //DateTime convertedDate = Convert.ToDateTime(date);

                    return convertedDate.ToString(format, dti);
                }

            }
            else
            {
                return null;
            }

        }
    }
    public class NullableDateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value != null && !String.IsNullOrWhiteSpace(value.AttemptedValue.ToString()))
            {
                var convertedValue = ConvertToDateString(value.AttemptedValue.ToString());
                var date = DateTime.ParseExact(convertedValue, "M/d/yyyy", CultureInfo.InvariantCulture).Date;
                //var date = DateTime.ParseExact(convertedValue, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                return date;
            }
            return null;
        }

        private string ConvertToDateString(string date)
        {
            if (date != null)
            {
                string format = "MM/dd/yyyy";
                //string format = "MM/dd/yyyy h:mm:ss tt";
                Regex r = new Regex(@"^\d{1,2}[/-]\d{1,2}[/-]\d{4}.*");
                Regex r1 = new Regex(@"^\d{4}[/-]\d{1,2}[/-]\d{1,2}.*");
                var isValid = r.IsMatch(date);
                var isOtherValid = r1.IsMatch(date);
                if (isValid || isOtherValid)
                {
                    DateTime convertedDate;
                    System.Globalization.DateTimeFormatInfo dti = new System.Globalization.DateTimeFormatInfo();
                    convertedDate = Convert.ToDateTime(date).Date;
                    //convertedDate = Convert.ToDateTime(date);

                    return convertedDate.ToString(format, dti);
                }
            }

            return null;

        }
    }
  
}