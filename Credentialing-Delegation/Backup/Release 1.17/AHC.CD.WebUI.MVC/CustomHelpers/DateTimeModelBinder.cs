using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                var date = DateTime.ParseExact(value.AttemptedValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                return date;
            }
            return null;
        }
    }
}