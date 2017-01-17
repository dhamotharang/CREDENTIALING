using System;
using System.Collections.Generic;
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

            var date = DateTime.ParseExact(value.AttemptedValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            return date;
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
                var date = DateTime.ParseExact(convertedValue, "MM/dd/yyyy", CultureInfo.InvariantCulture).Date;
                return date;
            }
            return null;
        }

        private string ConvertToDateString(string date)
        {
            if (date != null)
            {
                string format = "MM/dd/yyyy";
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

                   return convertedDate.ToString(format, dti);
               }
                
            }
            else
            {
                return null;
            }           

        }
    }
}