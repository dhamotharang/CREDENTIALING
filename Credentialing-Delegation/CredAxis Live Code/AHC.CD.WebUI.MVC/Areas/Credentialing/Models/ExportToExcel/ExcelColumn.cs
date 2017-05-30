using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.ExportToExcel
{
    public class ExcelColumn
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string MaxColumnWidth { get; set; }
        public string FormatCode { get; set; }
        public bool IsWrapped { get; set; }
    }
}