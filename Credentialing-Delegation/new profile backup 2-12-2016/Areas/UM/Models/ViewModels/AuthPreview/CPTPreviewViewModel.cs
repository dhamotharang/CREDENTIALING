using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class CPTPreviewViewModel
    {


        public string CPTCode { get; set; }
        

        [Display(Name = "CPT/HCPCS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CPTCodeModifier
        {
            get
            {
                string code = CPTCode;
                if (Modifier != null)
                {
                    code = code + " - " + Modifier;
                }
                return code;

            }
        }

        [Display(Name = "Desc", ShortName = "CPT Desc")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CPTDesc { get; set; }

        public int? Modifier { get; set; }

        [Display(Name = "Desc", ShortName = "CPT Desc")]
        [DisplayFormat(NullDisplayText = "-")]
        public string POSSpecificCPTDesc
        {
            
            get
            {
                //SN EVAL 1 TIME(S) A WEEK FOR 5 WEEK, FOR A TOTAL OF 5 UNIT(S)
                string Description = CPTDesc;
                if (Discipline != null && RequestedUnits != null && Range1 != null && NumberPer != null && Range2 != null && TotalUnits != null)
                {
                    Description = Discipline + " " + RequestedUnits + " TIME(S) A " + Range1 + " FOR " + NumberPer + " " + Range2 + ", FOR A TOTAL OF " + TotalUnits + " UNIT(S)";
                }
                else if (Discipline != null)
                {
                    Description = Discipline;
                }
                return Description;
            }

        }

        public string Discipline { get; set; }

        [Display(Name = "Units")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? RequestedUnits { get; set; }

        public string Range1 { get; set; }

        public int? NumberPer { get; set; }

        public string Range2 { get; set; }

        [Display(Name = "Total Units")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? TotalUnits { get; set; }

        //  public string PlainLanguage { get; set; }
    
    }
}