using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.CredAxis.Enums;

namespace PortalTemplate.Areas.CredAxis.Models.SpecialtyViewModel
{
    public class SpecialtyDetailViewModel
    {

        [Key]
        public int? SpecialtyDetailId { get; set; }

        [Display(Name = "SPECIALTY TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SpecialtyType { get; set; }


        [Display(Name = "SPECIALTY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Specialty { get; set; }

        [Display(Name = "PERCENTAGE OF TIME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PercentageOfTime { get; set; }

        [Display(Name = "TAXONOMY CODE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string TaxonomyCode { get; set; }

        [Display(Name = "DO YOU WISH TO BE LISTED IN THE DIRECTORY UNDER THIS SPECIALTY ? ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DirectorySpecialty { get; set; }

        [Display(Name = "HMO")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public bool Hmo { get; set; }

        [Display(Name = "POS")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public bool Pos { get; set; }

        [Display(Name = "PPO")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public bool Ppo { get; set; }
                

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////
        /// </summary>
        [Display(Name = "BOARD CERTIFIED?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool BoardCertified { get; set; }

        [Display(Name = "BOARD NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string BoardName { get; set; }

        [Display(Name = "CERTIFICATE NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Certificatenumber { get; set; }

        [Display(Name = "INITIAL CERTIFICATION DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CertificationDate { get; set; }

        [Display(Name = "LAST RE-CERTIFICATION DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReCertificationDate { get; set; }

        [Display(Name = "EXPIRATION DATE")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string ExpirationDate { get; set; }

        [Display(Name = "SUPPORTING DOCUMENT")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string SupportingDocumentPath { get; set; }

        [Display(Name = "If not board certified, select one *")]
        public BoardCertificationExamStatus SpecialtyBoardExamStatus { get; set; }

        [Display(Name = "Date Of Exam")]
        public string ExamDate { get; set; }

        [Display(Name = "Reason For Not Taking Exam")]
        public string ReasonForNotTakingExam { get; set; }

        [Display(Name = "Remark For Failing The Exam")]
        public string RemarkForExamFail { get; set; }

    }
}