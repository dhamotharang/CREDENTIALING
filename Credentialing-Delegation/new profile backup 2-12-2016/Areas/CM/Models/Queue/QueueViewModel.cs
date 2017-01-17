using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.CM.CustomHelpers;

namespace PortalTemplate.Areas.CM.Models.Queue
{
    public class QueueViewModel
    {
        [Display(Name = "MBR ID")]
        public string MemberID { get; set; }

        [Display(Name = "MBR Name")]
        public string MemberName { get; set; }

        [Display(Name = "DOB")]
        public string DateOfBirth { get; set; }

        [Display(Name = "PCP")]
        public string PCP { get; set; }

        [Display(Name = "LOB")]
        public string LineOfBusiness { get; set; }

        [Display(Name = "PBP")]
        public string PlanBenefitPortal { get; set; }

        [Display(Name = "DUE DT")]
        public string DueDate { get; set; }

        [Display(Name = "EPS ID")]
        public string EpisodeID { get; set; }

        [Display(Name = "EPS CLOSE DT")]
        public string EpisodeCloseDate { get; set; }

        [Display(Name = "CASE ID")]
        public string CaseID { get; set; }

        [Display(Name = "CASE CLOSE DT")]
        public string CaseCloseDate { get; set; }

        [Display(Name = "PGM")]
        public string Program { get; set; }

        [Display(Name = "STATUS")]
        public string Status { get; set; }

        [Display(Name = "DAYS IN EPS")]
        public int DaysInEpisode { get; set; }

        [Display(Name = "DAYS IN CASE")]
        public int DaysInCase { get; set; }

        [Display(Name = "PREF TIME")]
        public string PreferenceTime { get; set; }

        [Display(Name = "SUC O/R")]
        public string SuccessfulOutreach { get; set; }

        [Display(Name = "UNSUC O/R")]
        public string UnsuccessfulOutreach { get; set; }

        [Display(Name = "ASSIGNED TO")]
        public string AssignedTo { get; set; }

        [Display(Name = "NOTE")]
        public string Note { get; set; }

        [Display(Name = "STATE")]
        public string State { get; set; }

        [Display(Name = "COUNTY")]
        public string County { get; set; }

        [Display(Name = "COPD")]
        public bool IsCOPD { get; set; }

        [Display(Name = "DM")]
        public bool IsDM { get; set; }

         [Display(Name = "HF")]
        public bool IsHF { get; set; }

         [Display(Name = "CVD")]
        public bool IsCVD { get; set; }

        [Display(Name = "MRA")]
        public double MRA { get; set; }

         [Display(Name = "CLAIMS")]
        public string Claims { get; set; }

        [Display(Name = "HRA")]
        public double HRA { get; set; }

        [Display(Name = "ER VISIT")]
        public double ERVisit { get; set; }

        [Display(Name = "HOSPITAL")]
        public double Hospital { get; set; }

         [Display(Name = "TYPE")]
        public string Type { get; set; }
    }
}