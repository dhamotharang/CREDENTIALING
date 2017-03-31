﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Resources.Document
{
    public static class DocumentRootPath
    {
        #region Demograhics

        public static readonly string BIRTH_CERTIFICATE_PATH = @"\Documents\BirthCertificate";
        public static readonly string OTHER_LEGAL_NAME_PATH = @"\Documents\OtherLegalName";
        public static readonly string PROFILE_IMAGE_PATH = @"\Documents\ProfileImage";
        public static readonly string SSN_PATH = @"\Documents\SSN";
        public static readonly string DL_PATH = @"\Documents\DL";
        public static readonly string VISA_PATH = @"\Documents\Visa";
        public static readonly string GREEN_CARD_PATH = @"\Documents\GreenCard";
        public static readonly string NATIONAL_IDENTIFICATION_PATH = @"\Documents\NationalID";

        #endregion

        #region Identification And License

        public static readonly string STATE_LICENSE_PATH = @"\Documents\StateLicense";
        public static readonly string DEA_PATH = @"\Documents\DEA";
        public static readonly string CDSC_PATH = @"\Documents\CDSC";
        public static readonly string MEDICARE_PATH = @"\Documents\Medicare";
        public static readonly string MEDICAID_PATH = @"\Documents\Medicaid";

        #endregion        

        #region Specialty Board

        public static readonly string SPECIALITY_BOARD_PATH = @"\Documents\SpecialtyBoard";

        #endregion

        #region Hospital Privilege

        public static readonly string HOSPITAL_PRIVILEGE_PATH = @"\Documents\HospitalPrivilege";

        #endregion

        #region Professional Liability

        public static readonly string PROFESSIONAL_LIABILITY_PATH = @"\Documents\ProfessionalLiability";

        #endregion

        #region Work History

        public static readonly string PROFESSIONAL_WORK_EXPERIENCE_PATH = @"\Documents\ProfessionalWorkExperience";

        #endregion

        #region Education History

        public static readonly string EDUCATION_CERTIFICATE_PATH = @"\Documents\EducationCertificate";
        public static readonly string ECFMG_PATH = @"\Documents\ECFMG";
        public static readonly string CME_CERTIFICATION_PATH = @"\Documents\CMECertification";
        public static readonly string RESIDENCY_INTERNSHIP_PATH = @"\Documents\ResidencyInternship";
        public static readonly string PROGRAM_PATH = @"\Documents\Program";

        #endregion

        #region Disclosure Questions
        public static readonly string DISCLOSURE_QUESTION_ANSWER = @"\Documents\DisclosureQuestion";
        #endregion

        #region Document Repository

        public static readonly string OTHER_DOCUMENT_PATH = @"\Documents\OtherDocument";

        #endregion

        public static string CV_DOCUMENT_PATH = @"\Documents\CV";
        public static string CONTRACT_DOCUMENT_PATH = @"\Documents\Contract";
        public static string DELEGATED_LOADTOPLAN_DOCUMENT_PATH = @"\Documents\DelegatedLoadToPlan";
        public static string DOCUMENTATION_CHECKLIST_DOCUMENT_PATH = @"\Documents\DocumentChecklist";

        #region Profile Verification Document

        public static readonly string PSV_DOCUMENT_PATH = @"\ApplicationDocument\PSVDocuments";


        #endregion

        #region CCM Signature Document

        public static readonly string CCM_SIGNATURE_DOCUMENT_PATH = @"\ApplicationDocuments\CCMSignatureDocuments";

        #endregion

        #region Loading Document

        public static readonly string LOADING_DOCUMENT_PATH = @"\ApplicationDocuments\LoadingDocuments";

        #endregion

        #region CCM Attach Document

        public static readonly string CCM_ATTACH_DOCUMENT_PATH = @"\ApplicationDocuments\CCMAttachDocuments";

        #endregion

        #region Plan Form

        public static readonly string PLANFORM_DOCUMENT_PATH = @"\ApplicationDocument\AIPlanFormPdf";

        #endregion

        #region Plan Logo

        public static readonly string PLANLOGO_DOCUMENT_PATH = @"\ApplicationDocuments\PlanLogo";

        #endregion

        #region Document Check List

        public static readonly string DocumentCheckListPDFPATH = @"\ApplicationDocuments\DocumentCheckList";

        #endregion
    }
}