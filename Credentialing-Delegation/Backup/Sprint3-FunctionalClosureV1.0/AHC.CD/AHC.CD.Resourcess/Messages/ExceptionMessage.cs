using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Resources.Messages
{
    public static class ExceptionMessage
    {
        
        public static readonly string PROVIDER_SAVE_EXCEPTION = "Unable to save provider information";
        public static readonly string FOLDER_CREATE_EXCEPTION = "Unable to create folder for provider document";
        public static readonly string DOCUMENT_SAVE_EXCEPTION = "Unable to save provider document file";
        public static string CV_UPLOADED_EXCEPTION = "UNABLE TO UPLOAD CV";
        
        public static string HOSPITAL_EXISTS = "Provider not found with the given Provider ID";
        public static string SSN_EXISTS = "This SSN no already exists";
        public static string UNABLE_TO_CREATE_FOLDER = "Unable to Create Folder for Document";
        public static string CREATE_FILE_EXCEPTION = "Unable to Create a Document";

        public static string DATABASE_VALIDATION_EXCEPTION = "Database validation found on add or update";

        #region Profile

        public static string PROFILE_NOT_FOUND = "Profile not found with the given profileID";
        public static string PROFILE_BY_ID_GET_EXCEPTION = "Unable to get profile for the given profileID";
        public static string ACTIVE_PROFILES_GET_EXCEPTION = "Unable to get all active profile";
        public static string ALL_PROFILE_GET_EXCEPTION = "Unable to get all profile";

        public static string PROFILE_ADD_UPDATE_EXCEPTION= "Sorry for Inconvenience !!!! Please Try Again Later...";


        #region Profile Document

        public static string PROFILE_DOCUMENT_NOT_FOUND = "Profile document not found with the given path";
        public static string PROFILE_DOCUMENT_CREATE_EXCEPTION = "Unable to add document to the profile";
        public static string PROFILE_DOCUMENT_UPDATE_EXCEPTION = "Unable to update document to the profile";

        #endregion

        #region Demographics

        public static string PROFILE_IMAGE_UPDATE_EXCEPTION = "Unable to update image to the profile";
        public static string HOME_ADDRESS_CREATE_EXCEPTION = "Unable to add home address to the profile";
        public static string HOME_ADDRESS_UPDATE_EXCEPTION = "Unable to update home address to the profile";
        public static string HOME_ADDRESS_PREFERENCE_SET_EXCEPTION = "Unable to set the home address preference to the profile";
        public static string OTHER_LEGAL_NAME_CREATE_EXCEPTION = "Unable to add other legal name to the profile";
        public static string OTHER_LEGAL_NAME_UPDATE_EXCEPTION = "Unable to update other legal name to the profile";
        public static string PERSONAL_DETAIL_UPDATE_EXCEPTION = "Unable to update personal detail to the profile";
        public static string CONTACT_DETAIL_UPDATE_EXCEPTION = "Unable to update contact detail to the profile";
        public static string PERSONAL_IDENTIFICATION_UPDATE_EXCEPTION = "Unable to update personal identification to the profile";
        public static string BIRTH_INFORMATION_UPDATE_EXCEPTION = "Unable to update birth information to the profile";
        public static string VISA_DETAIL_UPDATE_EXCEPTION = "Unable to update visa detail to the profile";
        public static string LANGUAGE_INFO_UPDATE_EXCEPTION = "Unable to update language information to the profile";
        public static string DRIVING_LICENSE_EXIST_EXCEPTION = "The given driving license is used";
        public static string NATIONAL_ID_NUMEBR_EXIST_EXCEPTION = "The given national id number is used";
        public static string NPI_NUMEBR_EXIST_EXCEPTION = "The given NPI number is used";
        public static string CAQH_NUMEBR_EXIST_EXCEPTION = "The given CAQH number is used";
        public static string UPIN_NUMEBR_EXIST_EXCEPTION = "The given UPIN number is used";
        public static string USMLE_NUMEBR_EXIST_EXCEPTION = "The given USMLE number is used";

        public static string VISA_NUMBER_EXISTS_EXCEPTION = "Visa number exists in the profile";
        public static string GREEN_CARD_NUMBER_EXISTS_EXCEPTION = "Green card number exists in the profile";
        public static string NATIONAL_ID_NUMBER_EXISTS_EXCEPTION = "National ID number exists in the profile";
        public static string VISA_DETAIL_HISTORY_CREATE_EXCEPTION = "Unable to add visa detail history in the profile";

        #endregion

        #region Identification And License

        public static string STATE_LICENSE_CREATE_EXCEPTION = "Unable to add state license to the profile";
        public static string STATE_LICENSE_UPDATE_EXCEPTION = "Unable to update state license to the profile";
        public static string STATE_LICENSE_RENEW_EXCEPTION = "Unable to renew state license to the profile";
        public static string STATE_LICENSE_HISTORY_CREATE_EXCEPTION = "Unable to add state license history to the profile";
        public static string STATE_LICENSE_HISTORY_UPDATE_EXCEPTION = "Unable to update state license history to the profile";
        public static string STATE_LICENSE_EXISTS_EXCEPTION = "State License exists in the profile";
        public static string STATE_LICENSE_NUMBER_EXISTS_EXCEPTION = "State License number exists in the profile";

        public static string FEDERAL_DEA_LICENSE_CREATE_EXCEPTION = "Unable to add Federal DEA license to the profile";
        public static string FEDERAL_DEA_LICENSE_UPDATE_EXCEPTION = "Unable to update Federal DEA license in the profile";
        public static string FEDERAL_DEA_LICENSE_RENEW_EXCEPTION = "Unable to renew Federal DEA license in the profile";
        public static string FEDERAL_DEA_LICENSE_HISTORY_CREATE_EXCEPTION = "Unable to add Federal DEA license history in the profile";
        public static string FEDERAL_DEA_LICENSE_HISTORY_UPDATE_EXCEPTION = "Unable to update Federal DEA license history in the profile";
        public static string FEDERAL_DEA_LICENSE_EXISTS_EXCEPTION = "Federal DEA License exists in the profile";
        public static string FEDERAL_DEA_LICENSE_NUMBER_EXISTS_EXCEPTION = "Federal DEA number exists in the profile";

        public static string CDSC_LICENSE_CREATE_EXCEPTION = "Unable to add CDSC license to the profile";
        public static string CDSC_LICENSE_UPDATE_EXCEPTION = "Unable to update CDSC license to the profile";
        public static string CDSC_LICENSE_RENEW_EXCEPTION = "Unable to renew CDSC license to the profile";
        public static string CDSC_LICENSE_HISTORY_CREATE_EXCEPTION = "Unable to add CDSC license history to the profile";
        public static string CDSC_LICENSE_HISTORY_UPDATE_EXCEPTION = "Unable to update CDSC license history to the profile";
        public static string CDSC_LICENSE_NUMBER_EXISTS_EXCEPTION = "CDSC number exists to the profile";

        public static string MEDICARE_CREATE_EXCEPTION = "Unable to add Medicare to the profile";
        public static string MEDICARE_UPDATE_EXCEPTION = "Unable to update Medicare to the profile";
        public static string MEDICARE_NUMBER_EXISTS_EXCEPTION = "Medicare number exists to the profile";

        public static string MEDICAID_CREATE_EXCEPTION = "Unable to add Medicaid to the profile";
        public static string MEDICAID_UPDATE_EXCEPTION = "Unable to update Medicaid to the profile";
        public static string MEDICAID_NUMBER_EXISTS_EXCEPTION = "Medicaid number exists to the profile";

        public static string OTHER_IDENTIFICATION_NUMBER_UPDATE_EXCEPTION = "Unable to update other identification number to the profile";

        #endregion

        #region Specialty Board

        public static string SPECIALITY_BOARD_CREATE_EXCEPTION = "Unable to add specialty board to the profile";
        public static string SPECIALITY_BOARD_UPDATE_EXCEPTION = "Unable to update specialty board to the profile";
        public static string SPECIALITY_BOARD_RENEW_EXCEPTION = "Unable to renew specialty board to the profile";
        public static string SPECIALITY_BOARD_HISTORY_CREATE_EXCEPTION = "Unable to add specialty board history to the profile";
        public static string SPECIALITY_BOARD_HISTORY_UPDATE_EXCEPTION = "Unable to update specialty board history to the profile";

        public static string BOARD_SPECIALITY_PREFERENCE_SET_EXCEPTION = "Unable to set the board speciality preference to the profile";
        public static string PRACTICE_INTERSET_UPDATE_EXCEPTION = "Unable to update practice interest to the profile";

        #endregion

        #region Hospital Privilege

        public static string HOSPITAL_PRIVILEGE_DETAIL_CREATE_EXCEPTION = "Unable to add hospital privilege detail to the profile";
        public static string HOSPITAL_PRIVILEGE_DETAIL_UPDATE_EXCEPTION = "Unable to update hospital privilege detail to the profile";
        public static string HOSPITAL_PRIVILEGE_DETAIL_RENEW_EXCEPTION = "Unable to renew hospital privilege detail to the profile";
        public static string HOSPITAL_PRIVILEGE_DETAIL_HISTORY_CREATE_EXCEPTION = "Unable to add hospital privilege detail history to the profile";
        public static string HOSPITAL_PRIVILEGE_DETAIL_HISTORY_UPDATE_EXCEPTION = "Unable to update hospital privilege detail history to the profile";
        public static string HOSPITAL_PRIVILEGE_INFORMATION_UPDATE_EXCEPTION = "Unable to update hospital privilege information to the profile";

        public static string HOSPITAL_PRIVILEGE_INFORMATION_EXISTS_EXCEPTION = "Hospital privilege information exits for same start and end date to the profile";
        public static string HOSPITAL_PRIVILEGE_PREFERENCE_SET_EXCEPTION = "Unable to set the hospital privilege preference to the profile";        

        #endregion

        #region Professional Liability

        public static string PROFESSIONAL_LIABILITY_CREATE_EXCEPTION = "Unable to add professional liability to the profile";
        public static string PROFESSIONAL_LIABILITY_UPDATE_EXCEPTION = "Unable to update professional liability to the profile";
        public static string PROFESSIONAL_LIABILITY_RENEW_EXCEPTION = "Unable to renew professional liability to the profile";
        public static string PROFESSIONAL_LIABILITY_HISTORY_CREATE_EXCEPTION = "Unable to add professional liability history to the profile";
        public static string PROFESSIONAL_LIABILITY_HISTORY_UPDATE_EXCEPTION = "Unable to update professional liability history to the profile";

        #endregion

        #region Professional Affiliation

        public static string PROFESSIONAL_AFFILIATION_CREATE_EXCEPTION = "Unable to add Professional Affiliation to the profile";
        public static string PROFESSIONAL_AFFILIATION_UPDATE_EXCEPTION = "Unable to update Professional Affiliation to the profile";

        #endregion

        #region Professional Reference

        public static string PROFESSIONAL_REFERENCE_CREATE_EXCEPTION = "Unable to add Professional Reference to the profile";
        public static string PROFESSIONAL_REFERENCE_UPDATE_EXCEPTION = "Unable to update Professional Reference to the profile";
        public static string PROFESSIONAL_REFRENCE_COUNT_EXCEPTION = "Minimum three active professional reference should be present";

        #endregion

        #region Work History

        public static string PROFESSIONAL_WORK_EXPERIENCE_CREATE_EXCEPTION = "Sorry! Professional Work experience Information could not be saved";
        public static string PROFESSIONAL_WORK_EXPERIENCE_UPDATE_EXCEPTION = "Sorry! Professional Work experience Information could not be updated";

        public static string PROFESSIONAL_WORK_EXPERIENCE_EXISTS_EXCEPTION = "Professional work experience exists to the profile";

        public static string WORK_GAP_CREATE_EXCEPTION = "Sorry!Work Gap Information could not be saved";
        public static string WORK_GAP_UPDATE_EXCEPTION = "Sorry! Work Gap Information could not be updated";
        public static string WORK_GAP_EXISTS_EXCEPTION = "Work Gap period exists to the profile";

        public static string MILITARY_SERVICE_INFORMATION_CREATE_EXCEPTION = "Sorry! Military Service Information could not be saved";
        public static string MILITARY_SERVICE_INFORMATION_UPDATE_EXCEPTION = "Sorry! Military Service Information could not be updated";

        public static string PUBLIC_HEALTH_SERVICE_CREATE_EXCEPTION = "Sorry! Public Service Information could not be saved";
        public static string PUBLIC_HEALTH_SERVICE_UPDATE_EXCEPTION = "Sorry! Public Service Information could not be updated";
        public static string PUBLIC_HEALTH_SERVICE_EXISTS_EXCEPTION = "Public service exists to the profile";

        #endregion

        #region Education History

        public static string EDUCATION_DETAIL_CREATE_EXCEPTION = "Unable to add education detail to the profile";
        public static string EDUCATION_DETAIL_UPDATE_EXCEPTION = "Unable to update education detail to the profile";

        public static string TRAINING_DETAIL_CREATE_EXCEPTION = "Unable to add training detail to the profile";
        public static string TRAINING_DETAIL_UPDATE_EXCEPTION = "Unable to update training detail to the profile";

        public static string RESIDENCY_INTERNSHIP_DETAIL_CREATE_EXCEPTION = "Unable to add residency internship detail to the profile";
        public static string RESIDENCY_INTERNSHIP_DETAIL_UPDATE_EXCEPTION = "Unable to update residency internship detail to the profile";
        public static string RESIDENCY_INTERNSHIP_PREFERENCE_SET_EXCEPTION = "Unable to set the residency internship to the profile"; 
        

        public static string CME_CERTIFICATION_CREATE_EXCEPTION = "Unable to add CME certification to the profile";
        public static string CME_CERTIFICATION_UPDATE_EXCEPTION = "Unable to update CME certification to the profile";

        public static string ECFMG_CERTIFICATION_UPDATE_EXCEPTION = "Unable to update ECFMG certification to the profile";

        #endregion

        #endregion

        
    }
}
