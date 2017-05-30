using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.CustomHelpers
{
    public class DateFormatTimeSlice
    {
        public string DateStringFormatTimeSlice(string val, string returnValue)
        {
            if (val == null || val == "")
            {
                return returnValue;
            }
            else
            {
                int start = 0;
                val = val.Replace("T", " ");
                int length = val.IndexOf(" ");
                string CropedDate = val.Substring(start, length);
                string[] formats = {"dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd", 
                   "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy"};
                string converted = DateTime.ParseExact(CropedDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
                return converted;
            }
        }
    }
}