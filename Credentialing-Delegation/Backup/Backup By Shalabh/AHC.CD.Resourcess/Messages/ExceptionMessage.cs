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
        public static string DISCLOSURE_QUESTIONS_CREATE_EXCEPTION = "Unable to save provider disclosure questions and answer";
        
        
        #region History

        public static readonly string EDUCATION_DETAIL_HISTORY_GET_EXCEPTION = "Sorry Unable to get Education detail history";
        public static readonly string PROGRAM_DETAIL_HISTORY_GET_EXCEPTION = "Sorry Unable to get Residency/Internship/Fellowship Details history";
        public static readonly string CME_CERTIFICATION_HISTORY_GET_EXCEPTION = "Sorry Unable to get Post Graduate Training/CME Details history";
        public static readonly string PROFESSIONAL_WORK_EXPERIENCE_HISTORY_GET_EXCEPTION = "Sorry Unable to get Professional work experience history";
        public static readonly string MILITARY_SERVICE_INFORMATION_HISTORY_GET_EXCEPTION = "Sorry Unable to get Military Service Information history";
        public static readonly string PUBLIC_HEALTH_SERVICE_HISTORY_GET_EXCEPTION = "Sorry Unable to get Public Health Service history";
        public static readonly string WORK_GAP_HISTORY_GET_EXCEPTION = "Sorry Unable to get Work Gap history";
        public static readonly string OTHER_LEGAL_NAME_HISTORY_GET_EXCEPTION = "Sorry Unable to get Other legal name history";
        public static readonly string HOME_ADDRESS_HISTORY_GET_EXCEPTION = "Sorry Unable to get Home address history";
        public static readonly string PROFESSIONAL_LIABILITY_HISTORY_GET_EXCEPTION = "Sorry Unable to get Professional liability history";
        public static readonly string PROFESSIONAL_AFFILIATION_HISTORY_GET_EXCEPTION = "Sorry Unable to get professional affiliation history";
        public static readonly string PROFESSIONAL_REFERENCE_HISTORY_GET_EXCEPTION = "Sorry Unable to get professional reference history";
        public static readonly string STATE_LICENSE_HISTORY_GET_EXCEPTION = "Sorry Unable to get State license history";
        public static readonly string FEDERAL_DEA_LICENSE_HISTORY_GET_EXCEPTION = "Sorry Unable to get Federal DEA license history";
        public static readonly string MEDICARE_HISTORY_GET_EXCEPTION = "Sorry Unable to get Medicare history";
        public static readonly string MEDICAID_HISTORY_GET_EXCEPTION = "Sorry Unable to get Medicaid history";
        public static readonly string CDSC_HISTORY_GET_EXCEPTION = "Sorry Unable to get CDS history";
        public static readonly string PRACTICE_LOCATION_HISTORY_GET_EXCEPTION = "Sorry Unable to get Practice location history";
        public static readonly string CONTRACT_INFORMATION_GROUP_HISTORY_GET_EXCEPTION = "Sorry Unable to get Group history";

        #endregion

        #region Profile

        public static string PROFILE_NOT_FOUND = "Profile not found with the given profileID";
        public static string PROFILE_BY_ID_GET_EXCEPTION = "Unable to get profile for the given profileID";
        public static string ACTIVE_PROFILES_GET_EXCEPTION = "Unable to get all active profile";
        public static string ALL_PROFILE_GET_EXCEPTION = "Unable to get all profile";

        public static string PROFILE_ADD_UPDATE_EXCEPTION= "Sorry for Inconvenience !!!! Please Try Again Later...";

        public static string USER_ADD_EXCEPTION = "Unable to add user";

        public static string USER_EXIST_EXCEPTION = "The email id is already exists";

        public static string USER_ASSIGNED_EXCEPTION = "The profile is already assigned";

        public static string TL_ASSIGN_EXCEPTION = "Unable to assign the team lead";

        #region PDF Generation

        public static string PROFILE_PDF_CREATION_EXCEPTION = "Unable to create pdf for the given profileID";
        public static string PROFILE_PRIMARY_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for primary profile data";
        public static string PROFILE_SUPLEMENT_PROFESSIONAL_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for suplemental profesional id's";
        public static string PROFILE_SUPLEMENT_TRAINING_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for suplemental education training data";
        public static string PROFILE_SUPLEMENT_SPECIALTY_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for suplemental specialty data";
        public static string PROFILE_SUPLEMENT_HOSPITAL_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for suplemental hospital data";
        public static string PROFILE_SUPLEMENT_LIABILITY_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for suplemental liability data";
        public static string PROFILE_SUPLEMENT_WORKHISTORY_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for suplemental work history data";
        public static string PROFILE_SUPLEMENT_WORKGAPS_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for suplemental work gaps data";
        public static string PROFILE_SUPLEMENT_COLLEAGUES_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for suplemental covering colleagues data";
        public static string PROFILE_SUPLEMENT_DISCLOSURE_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for suplemental disclosure questions data";
        public static string PROFILE_SUPLEMENT_PRACTICE_DATA_PDF_CREATION_EXCEPTION = "Unable to create pdf for suplemental practice location data";
        public static string PROFILE_DATA_DATE_TO_STRING_CONVERSION_EXCEPTION = "Unable to convert date to string";

        #endregion

        # region MasterData

        public static string PAYMENT_REMITTANCE_MASTER_DATA_ADD_EXCEPTION = "Sorry Unable To Add Payment and Remittance person";
        public static string PAYMENT_REMITTANCE_MASTER_DATA_UPDATE_EXCEPTION = "Sorry Unable To Update Payment and Remittance person";

        public static string CREDENTIALING_CONTACT_MASTER_DATA_ADD_EXCEPTION = "Sorry Unable To Add Credentialing Contact";
        public static string CREDENTIALING_CONTACT_MASTER_DATA_UPDATE_EXCEPTION = "Sorry Unable To Update Credentialing Contact";

        public static string BILLING_CONTACT_MASTER_DATA_ADD_EXCEPTION = "Sorry Unable To Add Billing Contact";
        public static string BILLING_CONTACT_MASTER_DATA_UPDATE_EXCEPTION = "Sorry Unable To Update Billing Contact";

        public static string OFFICE_MANAGER_MASTER_DATA_ADD_EXCEPTION = "Sorry Unable To Add Office Manager";
        public static string OFFICE_MANAGER_MASTER_DATA_UPDATE_EXCEPTION = "Sorry Unable To Update Office Manager";

        public static string SCHOOL_ADD_EXCEPTION = "Sorry Unable To Add School";
        public static string SCHOOL_UPDATE_EXCEPTION = "Sorry Unable To Update School";

        public static string PROVIDERTYPE_ADD_EXCEPTION = "Sorry Unable To Add Provider Type";
        public static string PROVIDERTYPE_UPDATE_EXCEPTION = "Sorry Unable To Update Provider Type ";


        public static string Admitting_Privileges_ADD_EXCEPTION = "Sorry Unable To Add Admitting Privilege";
        public static string Admitting_Privileges_UPDATE_EXCEPTION = "Sorry Unable To Update Admitting Privilege";

        public static string Certification_ADD_EXCEPTION = "Sorry Unable To Add Certification";
        public static string Certification_UPDATE_EXCEPTION = "Sorry Unable To Update Certification";

        public static string Practice_Accessibility_Questions_ADD_EXCEPTION = "Sorry Unable To Add Practice Accessibility Question";
        public static string Practice_Accessibility_Questions_UPDATE_EXCEPTION = "Sorry Unable To Update Practice Accessibility Question";

        public static string Practice_OpenStatus_Question_ADD_EXCEPTION = "Sorry Unable To Add Practice Open Status Question";
        public static string Practice_OpenStatus_Question_UPDATE_EXCEPTION = "Sorry Unable To Update Practice Open Status Question";

        public static string Practice_Service_Question_ADD_EXCEPTION = "Sorry Unable To Add Practice Service Question";
        public static string Practice_Service_Question_UPDATE_EXCEPTION = "Sorry Unable To Update Practice Service Question";


        public static string Qualification_Degree_ADD_EXCEPTION = "Sorry Unable To Add Qualification Degree";
        public static string Qualification_Degree_UPDATE_EXCEPTION = "Sorry Unable To Update Qualification Degree";

        public static string Speciality_ADD_EXCEPTION = "Sorry Unable To Add Speciality";
        public static string Speciality_UPDATE_EXCEPTION = "Sorry Unable To Update Speciality";

        public static string Speciality_Board_ADD_EXCEPTION = "Sorry Unable To Add Speciality Board";
        public static string Speciality_Board_UPDATE_EXCEPTION = "Sorry Unable To Update Speciality Board";


        public static string Staff_Category_ADD_EXCEPTION = "Sorry Unable To Add Staff Category";
        public static string Staff_Category_UPDATE_EXCEPTION = "Sorry Unable To Update Staff Category";

        public static string State_License_Status_ADD_EXCEPTION = "Sorry Unable To Add State License Status";
        public static string State_License_Status_UPDATE_EXCEPTION = "Sorry Unable To Update State License Status";

        public static string Visa_Status_ADD_EXCEPTION = "Sorry Unable To Add State Visa Status";
        public static string Visa_Status_UPDATE_EXCEPTION = "Sorry Unable To Update Visa Status";

        public static string Visa_Type_ADD_EXCEPTION = "Sorry Unable To Add State Visa Status";
        public static string Visa_Type_UPDATE_EXCEPTION = "Sorry Unable To Update Visa Status";

        public static string Provider_Level_ADD_EXCEPTION = "Sorry Unable To Add State Provider Level";
        public static string Provider_Level_UPDATE_EXCEPTION = "Sorry Unable To Update Provider Level";

        public static string Group_ADD_EXCEPTION = "Sorry Unable To Add State Group";
        public static string Group_UPDATE_EXCEPTION = "Sorry Unable To Update Group";

        public static string Military_Discharge_ADD_EXCEPTION = "Sorry Unable To Add Military Discharge";
        public static string Military_Discharge_UPDATE_EXCEPTION = "Sorry Unable To Update Military Discharge";

        public static string Military_Present_Duty_ADD_EXCEPTION = "Sorry Unable To Add Military Present Duty";
        public static string Military_Present_Duty_UPDATE_EXCEPTION = "Sorry Unable To Update Military Present Duty";

        public static string DEASchedule_ADD_EXCEPTION = "Sorry Unable To Add DEASchedule";
        public static string DEASchedule_UPDATE_EXCEPTION = "Sorry Unable To Update DEASchedule";

        public static string Hospital_ADD_EXCEPTION = "Sorry Unable To Add Hospital";
        public static string Hospital_UPDATE_EXCEPTION = "Sorry Unable To Update Hospital";

        public static string Hospital_Contact_ADD_EXCEPTION = "Sorry Unable To Add Hospital Contact";
        public static string Hospital_Contact_UPDATE_EXCEPTION = "Sorry Unable To Update Hospital Contact";

        public static string Hospital_Contact_Person_ADD_EXCEPTION = "Sorry Unable To Add Hospital Contact Person";
        public static string Hospital_Contact_Person_UPDATE_EXCEPTION = "Sorry Unable To Update Hospital Contact Person";

        public static string Insurance_Carrier_ADD_EXCEPTION = "Sorry Unable To Add Insurance Carrier";
        public static string   Insurance_Carrier_UPDATE_EXCEPTION = "Sorry Unable To Update Insurance Carrier";

        public static string Insurance_Carrier_Address_ADD_EXCEPTION = "Sorry Unable To Add Insurance Carrier Address";
        public static string Insurance_Carrier_Address_UPDATE_EXCEPTION = "Sorry Unable To Update Insurance Carrier Address";

        public static string  Military_Rank_ADD_EXCEPTION = "Sorry Unable To Add Military Rank";
        public static string  Military_Rank_UPDATE_EXCEPTION = "Sorry Unable To Update Military Rank";

        public static string Military_Branch_ADD_EXCEPTION = "Sorry Unable To Add Military Branch";
        public static string Military_Branch_UPDATE_EXCEPTION = "Sorry Unable To Update Military Branch";

        public static string Question_Category_ADD_EXCEPTION = "Sorry Unable To Add Question Category";
        public static string Question_Category_UPDATE_EXCEPTION = "Sorry Unable To Update Question Category";

        public static string Question_ADD_EXCEPTION = "Sorry Unable To Add Question";
        public static string Question_UPDATE_EXCEPTION = "Sorry Unable To Update Question";

        # endregion

        #region Profile Document

        public static string PROFILE_DOCUMENT_NOT_FOUND = "Profile document not found with the given path";
        public static string PROFILE_DOCUMENT_CREATE_EXCEPTION = "Unable to add document to the profile";
        public static string PROFILE_DOCUMENT_UPDATE_EXCEPTION = "Unable to update document to the profile";

        #endregion

        #region Demographics

        public static string PROFILE_IMAGE_UPDATE_EXCEPTION = "Unable to update image to the profile";
        public static string HOME_ADDRESS_CREATE_EXCEPTION = "Sorry! Home Address could not be saved.";
        public static string HOME_ADDRESS_UPDATE_EXCEPTION = "Sorry! Home Address could not be updated.";
        public static string HOME_ADDRESS_PREFERENCE_SET_EXCEPTION = "Unable to set the home address preference to the profile";
        public static string HOME_ADDRESS_HISTORY_CREATE_EXCEPTION = "Unable to add Home Address history to the profile";
        public static string HOME_ADDRESS_REMOVE_EXCEPTION = "Sorry! Home Address could not be removed.";
        public static string OTHER_LEGAL_NAME_CREATE_EXCEPTION = "Sorry! Other Legal Name could not be added";
        public static string OTHER_LEGAL_NAME_UPDATE_EXCEPTION = "Sorry! Other Legal Name could not be updated";
        public static string OTHER_LEGAL_NAME_REMOVE_EXCEPTION = "Sorry! Other Legal Name could not be removed.";
        public static string OTHER_LEGAL_NAME_HISTORY_CREATE_EXCEPTION = "Unable to add Other Legal Name history to the profile";
        public static string PERSONAL_DETAIL_UPDATE_EXCEPTION = "Sorry! Personal Information could not be updated.";
        public static string CONTACT_DETAIL_UPDATE_EXCEPTION = "Sorry! Contact Details could not be updated.";
        public static string PERSONAL_IDENTIFICATION_UPDATE_EXCEPTION = "Sorry! Personal Identification Details could not be saved.";
        public static string BIRTH_INFORMATION_UPDATE_EXCEPTION = "Sorry! Birth Information Details could not be updated.";
        public static string VISA_DETAIL_UPDATE_EXCEPTION = "Sorry! Visa Details could not be saved.";
        public static string LANGUAGE_INFO_UPDATE_EXCEPTION = "Unable to update language information to the profile";
        public static string DRIVING_LICENSE_EXIST_EXCEPTION = "The given driving license is used";
        public static string NATIONAL_ID_NUMEBR_EXIST_EXCEPTION = "The given national id number is used";
       
        public static string HOME_NUMBER_EXISTS_EXCEPTION = "The given home number {0} is already used";
        public static string FAX_NUMBER_EXISTS_EXCEPTION = "The given fax number {0} is already used";
        public static string MOBILE_NUMBER_EXISTS_EXCEPTION = "The given mobile number {0} is already used";
        public static string EMAIL_EXISTS_EXCEPTION = "The given email id {0} is already used";
        public static string PAGER_NUMBER_EXISTS_EXCEPTION = "The given pager number {0} is already used";
        public static string DUPLICATE_CONTACT_DETIAL_EXCEPTION = "Duplcate entries found : {0}";

        public static string VISA_NUMBER_EXISTS_EXCEPTION = "Visa number is already used";
        public static string GREEN_CARD_NUMBER_EXISTS_EXCEPTION = "Green card number exists in the profile";
        public static string NATIONAL_ID_NUMBER_EXISTS_EXCEPTION = "National ID number exists in the profile";
        public static string VISA_DETAIL_HISTORY_CREATE_EXCEPTION = "Unable to add visa detail history in the profile";

        #endregion

        #region Identification And License

        public static string STATE_LICENSE_CREATE_EXCEPTION = "Sorry! State License information could not be saved.";
        public static string STATE_LICENSE_UPDATE_EXCEPTION = "Sorry! State License information Details could not be updated.";
        public static string STATE_LICENSE_RENEW_EXCEPTION = "Unable to renew state license to the profile";
        public static string STATE_LICENSE_HISTORY_CREATE_EXCEPTION = "Unable to add state license history to the profile";
        public static string STATE_LICENSE_HISTORY_UPDATE_EXCEPTION = "Unable to update state license history to the profile";
        public static string STATE_LICENSE_EXISTS_EXCEPTION = "State License exists in the profile";
        public static string STATE_LICENSE_NUMBER_EXISTS_EXCEPTION = "State License number exists in the profile";
        public static string STATE_LICENSE_REMOVE_EXCEPTION = "Sorry! State License could not be removed.";

        public static string FEDERAL_DEA_LICENSE_CREATE_EXCEPTION = "Sorry! DEA information could not be saved.";
        public static string FEDERAL_DEA_LICENSE_UPDATE_EXCEPTION = "Sorry! DEA information Details could not be updated.";
        public static string FEDERAL_DEA_LICENSE_RENEW_EXCEPTION = "Unable to renew Federal DEA license in the profile";
        public static string FEDERAL_DEA_LICENSE_HISTORY_CREATE_EXCEPTION = "Unable to add Federal DEA license history in the profile";
        public static string FEDERAL_DEA_LICENSE_HISTORY_UPDATE_EXCEPTION = "Unable to update Federal DEA license history in the profile";
        public static string FEDERAL_DEA_LICENSE_EXISTS_EXCEPTION = "Federal DEA License exists in the profile";
        public static string FEDERAL_DEA_LICENSE_NUMBER_EXISTS_EXCEPTION = "Federal DEA number exists in the profile";

        public static string CDSC_LICENSE_CREATE_EXCEPTION = "Unable to add CDS license to the profile";
        public static string CDSC_LICENSE_UPDATE_EXCEPTION = "Unable to update CDS license to the profile";
        public static string CDSC_LICENSE_RENEW_EXCEPTION = "Unable to renew CDS license to the profile";
        public static string CDSC_LICENSE_HISTORY_CREATE_EXCEPTION = "Unable to add CDS license history to the profile";
        public static string CDSC_LICENSE_HISTORY_UPDATE_EXCEPTION = "Unable to update CDS license history to the profile";
        public static string CDSC_LICENSE_NUMBER_EXISTS_EXCEPTION = "CDS number exists to the profile";
        public static string CDSC_LICENSE_REMOVE_EXCEPTION = "Sorry! CDS License could not be removed.";

        public static string MEDICARE_CREATE_EXCEPTION = "Sorry! Medicare information could not be saved.";
        public static string MEDICARE_UPDATE_EXCEPTION = "Sorry! Medicare information Details could not be updated.";
        public static string MEDICARE_NUMBER_EXISTS_EXCEPTION = "Medicare number exists to the profile";
        public static string MEDICARE_HISTORY_ADD_EXCEPTION = "Sorry! Medicare information history could not be saved.";
        public static string MEDICARE_NUMBER_REMOVE_EXCEPTION = "Sorry unable to remove Medicare Exception";

        public static string MEDICAID_CREATE_EXCEPTION = "Sorry! Medicaid information could not be saved.";
        public static string MEDICAID_UPDATE_EXCEPTION = "Sorry! Medicaid information Details could not be updated.";
        public static string MEDICAID_NUMBER_EXISTS_EXCEPTION = "Medicaid number exists to the profile";
        public static string MEDICAID_HISTORY_ADD_EXCEPTION = "Sorry! Medicaid information history could not be saved.";
        public static string MEDICAID_NUMBER_REMOVE_EXCEPTION = "Sorry unable to remove Medicaid Number";

        public static string OTHER_IDENTIFICATION_NUMBER_UPDATE_EXCEPTION = "Sorry! Other Identification Information Details could not be updated.";
        public static string NPI_NUMEBR_EXIST_EXCEPTION = "The given NPI number is already used";
        public static string CAQH_NUMEBR_EXIST_EXCEPTION = "The given CAQH number is already used";
        public static string UPIN_NUMEBR_EXIST_EXCEPTION = "The given UPIN number is already used";
        public static string USMLE_NUMEBR_EXIST_EXCEPTION = "The given USMLE number is already used";
        public static string NPI_USERNAME_EXIST_EXCEPTION = "The given NPI username is already used";
        public static string CAQH_USERNAME_EXIST_EXCEPTION = "The given CAQH username is already used";
        
        #endregion

        #region Specialty Board

        public static string SPECIALITY_BOARD_CREATE_EXCEPTION = "Sorry! Specialty Details could not be saved.";
        public static string SPECIALITY_BOARD_UPDATE_EXCEPTION = "Sorry! Specialty Details could not be updated.";
        public static string SPECIALITY_BOARD_RENEW_EXCEPTION = "Unable to renew specialty board to the profile";
        public static string SPECIALITY_BOARD_HISTORY_CREATE_EXCEPTION = "Unable to add specialty board history to the profile";
        public static string SPECIALITY_BOARD_HISTORY_UPDATE_EXCEPTION = "Unable to update specialty board history to the profile";
        public static string SPECIALITY_BOARD_REMOVE_EXCEPTION = "Sorry! Specialty Details could not be removed.";

        public static string BOARD_SPECIALITY_PREFERENCE_SET_EXCEPTION = "Unable to set the board specialty preference to the profile";
        public static string PRACTICE_INTERSET_UPDATE_EXCEPTION = "Sorry! Practice Interest could not be updated.";

        #endregion

        #region Hospital Privilege

        public static string HOSPITAL_PRIVILEGE_DETAIL_CREATE_EXCEPTION = "Sorry! Hospital Privilege Details could not be saved.";
        public static string HOSPITAL_PRIVILEGE_DETAIL_UPDATE_EXCEPTION = "Sorry! Hospital Privilege Details Details could not be updated.";
        public static string HOSPITAL_PRIVILEGE_DETAIL_RENEW_EXCEPTION = "Unable to renew hospital privilege detail to the profile";
        public static string HOSPITAL_PRIVILEGE_DETAIL_HISTORY_CREATE_EXCEPTION = "Unable to add hospital privilege detail history to the profile";
        public static string HOSPITAL_PRIVILEGE_DETAIL_HISTORY_UPDATE_EXCEPTION = "Unable to update hospital privilege detail history to the profile";
        public static string HOSPITAL_PRIVILEGE_INFORMATION_UPDATE_EXCEPTION = "Sorry! Hospital Privilege information could not be updated.";
        public static string HOSPITAL_PRIVILEGE_DETAIL_REMOVE_EXCEPTION = "Sorry! Hospital Privilege Detail could not be removed.";

        public static string HOSPITAL_PRIVILEGE_INFORMATION_EXISTS_EXCEPTION = "Hospital privilege information exits for same start and end date to the profile";
        public static string HOSPITAL_PRIVILEGE_PREFERENCE_SET_EXCEPTION = "Unable to set the hospital privilege preference to the profile";        

        #endregion

        #region Professional Liability

        public static string PROFESSIONAL_LIABILITY_CREATE_EXCEPTION = "Sorry! Professional liability information could not be saved.";
        public static string PROFESSIONAL_LIABILITY_UPDATE_EXCEPTION = "Sorry! Professional liability information Details could not be updated.";
        public static string PROFESSIONAL_LIABILITY_RENEW_EXCEPTION = "Unable to renew professional liability to the profile";
        public static string PROFESSIONAL_LIABILITY_HISTORY_CREATE_EXCEPTION = "Unable to add professional liability history to the profile";
        public static string PROFESSIONAL_LIABILITY_HISTORY_UPDATE_EXCEPTION = "Unable to update professional liability history to the profile";

        #endregion

        #region Professional Affiliation

        public static string PROFESSIONAL_AFFILIATION_CREATE_EXCEPTION = "Sorry! Professional Affiliation Information could not be saved.";
        public static string PROFESSIONAL_AFFILIATION_UPDATE_EXCEPTION = "Sorry! Professional Affiliation Information could not be updated.";
        public static string PROFESSIONAL_AFFILIATION_HISTORY_ADD_EXCEPTION = "Sorry! Professional Affiliation History Information could not be saved.";
        public static string PROFESSIONAL_AFFILIATION_REMOVE_EXCEPTION = "Sorry! Professional Affiliation Information could not be removed.";

        #endregion

        #region Professional Reference

        public static string PROFESSIONAL_REFERENCE_CREATE_EXCEPTION = "Sorry! Professional Reference Information could not be saved.";
        public static string PROFESSIONAL_REFERENCE_UPDATE_EXCEPTION = "Sorry! Professional Reference Information could not be updated.";
        public static string PROFESSIONAL_REFRENCE_COUNT_EXCEPTION = "Minimum three active professional reference should be present";
        public static string PROFESSIONAL_REFERENCE_HISTORY_ADD_EXCEPTION = "Sorry! Professional Reference History Information could not be saved.";
        public static string PROFESSIONAL_REFRENCE_REMOVE_EXCEPTION = "Sorry! Professional Reference Information could not be removed.";

        #endregion

        #region Work History

        public static string PROFESSIONAL_WORK_EXPERIENCE_CREATE_EXCEPTION = "Sorry! Professional Work experience Information could not be saved";
        public static string PROFESSIONAL_WORK_EXPERIENCE_UPDATE_EXCEPTION = "Sorry! Professional Work experience Information could not be updated";
        public static string PROFESSIONAL_WORK_EXPERIENCE_HISTORY_ADD_EXCEPTION = "Sorry! Professional Work experience Information history could not be saved.";
        public static string PROFESSIONAL_WORK_EXPERIENCE_REMOVE_EXCEPTION = "Sorry! Professional Work experience Information could not be removed.";

        public static string PROFESSIONAL_WORK_EXPERIENCE_EXISTS_EXCEPTION = "Professional work experience exists to the profile";

        public static string WORK_GAP_CREATE_EXCEPTION = "Sorry!Work Gap Information could not be saved";
        public static string WORK_GAP_UPDATE_EXCEPTION = "Sorry! Work Gap Information could not be updated";
        public static string WORK_GAP_EXISTS_EXCEPTION = "Work Gap period exists to the profile";
        public static string WORK_GAP_REMOVE_EXCEPTION = "Sorry! Work Gap could not be removed.";
        public static string WORK_GAP_HISTORY_ADD_EXCEPTION = "Sorry! Work Gap history could not be saved.";

        public static string MILITARY_SERVICE_INFORMATION_CREATE_EXCEPTION = "Sorry! Military Service Information could not be saved";
        public static string MILITARY_SERVICE_INFORMATION_UPDATE_EXCEPTION = "Sorry! Military Service Information could not be updated";
        public static string MILITARY_SERVICE_INFORMATION_REMOVE_EXCEPTION = "Sorry! Military Service Information could not be removed.";
        public static string MILITARY_SERVICE_INFORMATION_HISTORY_ADD_EXCEPTION = "Sorry! Military Service Information history could not be saved.";

        public static string PUBLIC_HEALTH_SERVICE_CREATE_EXCEPTION = "Sorry! Public Service Information could not be saved";
        public static string PUBLIC_HEALTH_SERVICE_UPDATE_EXCEPTION = "Sorry! Public Service Information could not be updated";
        public static string PUBLIC_HEALTH_SERVICE_EXISTS_EXCEPTION = "Public service exists to the profile";
        public static string PUBLIC_HEALTH_SERVICE_REMOVE_EXCEPTION = "Sorry! Public Service Information could not be removed.";
        public static string PUBLIC_HEALTH_SERVICE_HISTORY_ADD_EXCEPTION = "Sorry! Public Service Information history could not be saved.";

        #endregion

        #region Education History

        public static string EDUCATION_DETAIL_CREATE_EXCEPTION = "Sorry! Education information could not be saved.";
        public static string EDUCATION_DETAIL_UPDATE_EXCEPTION = "Sorry! Education information Details could not be updated.";
        public static string EDUCATION_DETAIL_REMOVE_EXCEPTION = "Sorry! Education information Detail could not be removed.";
        public static string EDUCATION_DETAIL_HISTORY_ADD_EXCEPTION = "Sorry! Education information Detail history could not be saved.";

        public static string TRAINING_DETAIL_CREATE_EXCEPTION = "Sorry! Residency/Internship information could not be saved.";
        public static string TRAINING_DETAIL_UPDATE_EXCEPTION = "Sorry! Residency/Internship information Details could not be updated.";

        public static string RESIDENCY_INTERNSHIP_DETAIL_CREATE_EXCEPTION = "Sorry! Residency/Internship information could not be saved.";
        public static string RESIDENCY_INTERNSHIP_DETAIL_UPDATE_EXCEPTION = "Sorry! Residency/Internship information Details could not be updated.";
        public static string RESIDENCY_INTERNSHIP_PREFERENCE_SET_EXCEPTION = "Unable to set the residency internship to the profile";

        public static string CME_CERTIFICATION_CREATE_EXCEPTION = "Sorry! Post Graduation Training/CME information could not be saved.";
        public static string CME_CERTIFICATION_UPDATE_EXCEPTION = "Sorry! Post Graduation Training/CME information Details could not be updated.";
        public static string CME_CERTIFICATION_REMOVE_EXCEPTION = "Sorry! Post Graduation Training/CME information Detail could not be removed.";
        public static string CME_CERTIFICATION_HISTORY_ADD_EXCEPTION = "Sorry! Post Graduation Training/CME information Detail history could not be saved.";

        public static string ECFMG_CERTIFICATION_UPDATE_EXCEPTION = "Sorry! ECFMG information Details could not be updated.";

        public static string PROGRAM_DETAIL_CREATE_EXCEPTION = "Sorry! Residency/Internship/Fellowship information could not be saved.";
        public static string PROGRAM_DETAIL_UPDATE_EXCEPTION = "Sorry! Residency/Internship/Fellowship information Details could not be updated.";
        public static string PROGRAM_DETAIL_PREFERENCE_SET_EXCEPTION = "Unable to set the primary residency/internship to the profile";
        public static string PROGRAM_DETAIL_HISTORY_ADD_EXCEPTION = "Sorry! Residency/Internship/Fellowship information history could not be saved.";
        public static string PROGRAM_DETAIL_REMOVE_EXCEPTION = "Sorry! Residency/Internship/Fellowship information Detail could not be removed.";

        #endregion

        #region Practice Location

        public static string PRACTICE_LOCATION_CREATE_EXCEPTION = "Sorry! practice location information could not be saved";
        public static string PRACTICE_LOCATION_UPDATE_EXCEPTION = "Sorry! practice location information could not be updated";
        public static string PRACTICE_LOCATION_REMOVE_EXCEPTION = "Sorry! Practice Location Information could not be removed.";
        public static string OFFICE_MANAGER_UPDATE_EXCEPTION = "Sorry! Office manager/Business Office Staff Contact information Details could not be updated.";
        public static string OPEN_PRACTICE_UPDATE_EXCEPTION = "Sorry! Open Practice Status could not be updated.";
        public static string ACCESSIBILITIES_UPDATE_EXCEPTION = "Sorry! Accessibilities could not be updated.";
        public static string WORKER_COMPENSATION_UPDATE_EXCEPTION = "Sorry! Worker Compensation could not be updated.";
        public static string BILLING_CONTACT_UPDATE_EXCEPTION = "Sorry! Billing Contact could not be updated.";
        public static string PAYMENT_REMMITTANCE_UPDATE_EXCEPTION = "Sorry! Payment And Remittance could not be updated.";
        public static string MID_LEVEL_CREATE_EXCEPTION = "Sorry! Mid-Level practitioner information could not be saved";
        public static string Partner_CREATE_EXCEPTION = "Sorry! Covering Colleague information could not be saved";
        public static string PracticeProvider_CREATE_EXCEPTION = "Sorry! information could not be saved";
        public static string PracticeProvider_UPDATE_EXCEPTION = "Sorry! information could not be updated";
        public static string PracticeProvider_REMOVED_EXCEPTION = "Sorry! information could not be removed";
        public static string SUPERVISING_PROVIDER_SAVE_EXCEPTION = "Sorry! Supervising provider information could not be saved";
        public static string DUPLICATE_PRACTICE_LOCATION_FACILITY = "Practice location with the selected facility already exists";
        public static string PRACTICE_LOCATION_DUPLICATE_MIDLEVEL = "Mid-level practitioner already exists";
        public static string PRACTICE_LOCATION_DUPLICATE_Partner = "Covering Colleague already exists";
        public static string PRACTICE_LOCATION_DUPLICATE_PracticeProvider = "Practice Provider already exists";
        public static string PRACTICE_LOCATION_DUPLICATE_SUPERVISING = "Supervising Provider already exists";
        public static string CREDENTIALING_CONTACT_ADD_EXCEPTION = "Sorry Credentialing Contact could not added";
        public static string CREDENTIALING_CONTACT_UPDATE_EXCEPTION = "Sorry Credentialing Contact could not updated";

        #endregion

        #region Contract Information

        public static string CONTRACT_INFORMATION_UPDATE_EXCEPTION = "Sorry! Contract Information could not be updated.";
        public static string CONTRACT_INFORMATION_ADD_EXCEPTION = "Sorry! Contract Information could not be added.";
        public static string CONTRACT_GROUP_INFORMATION_ADD_EXCEPTION = "Sorry! Contract Group Information could not be added.";
        public static string CONTRACT_GROUP_INFORMATION_UPDATE_EXCEPTION = "Sorry! Contract Group Information could not be updated.";
        public static string DUPLICATE_GROUP_INFORMATION_EXCEPTION = "Sorry! Contract Group with this name already added";
        public static string INACTIVE_CONTRACT_INFORMATION_EXCEPTION = "Sorry! Contract Has Been Inactivated";
        public static string CONTRACT_GROUP_INFORMATION_HISTORY_ADD_EXCEPTION = "Sorry! Contract Group Information history could not be saved.";
        public static string CONTRACT_GROUP_INFORMATION_REMOVE_EXCEPTION = "Sorry! Contract Group Information could not be removed.";

        #endregion

        #region Document Repository

        public static string OTHER_DOCUMENT_CREATE_EXCEPTION = "Sorry! Other Document could not be added";
        public static string OTHER_DOCUMENT_UPDATE_EXCEPTION = "Sorry! Other Document could not be updated";
        public static string OTHER_DOUCUMENT_REMOVE_EXCEPTION = "Sorry! Other Document could not be removed.";
        public static string OTHER_DOCUMENT_HISTORY_CREATE_EXCEPTION = "Unable to add Other Document history to the profile";

        #endregion
      
        #endregion

        #region Get Profile as section wise

        public static string PROFILE_DEMOGRAPHICS_BY_ID_GET_EXCEPTION = "Unable to get profile demographics for the given profileID";
        public static string PROFILE_IDENTIFICATION_AND_LICENSES_BY_ID_GET_EXCEPTION = "Unable to get profile identification and licenses for the given profileID";
        public static string PROFILE_EDUCATION_HISTORY_BY_ID_GET_EXCEPTION = "Unable to get profile education history for the given profileID";
        public static string PROFILE_SPECIALTY_BOARD_BY_ID_GET_EXCEPTION = "Unable to get profile specialty board for the given profileID";
        public static string PROFILE_PRACTICE_LOCATION_BY_ID_GET_EXCEPTION = "Unable to get profile practice location for the given profileID";
        public static string PROFILE_HOSPITAL_PRIVILEGE_BY_ID_GET_EXCEPTION = "Unable to get profile hospital privilege for the given profileID";
        public static string PROFILE_PROFESSIONAL_LIABILITY_BY_ID_GET_EXCEPTION = "Unable to get profile professional liability for the given profileID";
        public static string PROFILE_WORK_HISTORY_BY_ID_GET_EXCEPTION = "Unable to get profile work history for the given profileID";
        public static string PROFILE_PROFESSIONAL_REFERENCE_BY_ID_GET_EXCEPTION = "Unable to get profile professional reference for the given profileID";
        public static string PROFILE_PROFESSIONAL_AFFILIATION_BY_ID_GET_EXCEPTION = "Unable to get profile professional affiliation for the given profileID";
        public static string PROFILE_DISCLOSURE_QUESTIONS_BY_ID_GET_EXCEPTION = "Unable to get profile disclosure questions for the given profileID";
        public static string PROFILE_CONTRACT_INFORAMTION_BY_ID_GET_EXCEPTION = "Unable to get profile contract information for the given profileID";

        #endregion

        public static string INITIATE_PROVIDER_CREATE_EXCEPTION = "Unable to create provider";

        public static string UNABLE_TO_DEACTIVATE_PROFILE = "Unable to deactivate profile";

        public static string UNABLE_TO_REACTIVATE_PROFILE = "Unable to reactivate profile";

        public static string UNABLE_TO_ADD_ROLE_PROFILE = "Unable to add role to the profile.";

        public static string DUPLICATE_ROLE_ADD_PROFILE = "The role is already added to the profile.";

        public static string City_ADD_EXCEPTION = "Unable to Add City";

        public static string EMAIL_TEMPLATE_ADD_EXCEPTION = "Unable to Add Email Template";

        public static string EMAIL_SENT_EXCEPTION = "Unable to Send Email";

        public static string EMAIL_FOLLOW_UP_STOP_EXCEPTION = "Unable to Stop Follow Up Email";

        public static string EMAIL_FOLLOW_UP_FOR_RECEIVERS_STOP_EXCEPTION = "Unable to Stop Follow Up Email For Selected Receivers";

        public static string CREDENTIALING_REQUEST_EXCEPTION = "Unable to Send Credentialing Request";

        public static string CREDENTIALING_REQUEST_TRACKER_EXCEPTION = "Unable to Process Request";

        public static string DISPLAY_PROFILE_SECTION = "Unable to Display Profile Sections";

        public static string SECTION_NOTAPPLICABLE = "Unable to Make Not Applicable";

        #region PLAN

        public static string PLAN_UPDATE_EXCEPTION = "Unable to update Plan";

        public static string PLAN_ADD_EXCEPTION = "Unable to add Plan";

        public static string PLAN_REMOVE_EXCEPTION = "Unable to remove Plan";

        #endregion

        #region LOB

        public static string LOB_UPDATE_EXCEPTION = "Unable to update Line Of Business";

        public static string LOB_ADD_EXCEPTION = "Unable to add Line Of Business";

        #endregion

        #region Credentialing

        public static string NO_PLAN_EXCEPTION = "Please provide a Plan Name.";
        public static string PLAN_PROVIDER_CREDENTIALING_EXISTS = "This provider is already credentialed for this plan.";
        public static string INITIATE_CREDENTIALING_EXCEPTION = "Unable to credential this provider for this plan.";
        public static string PSV_INCOMPLETE_EXCEPTION = "PSV is yet to be completed.";
        public static string NOT_DELEGATED_EXCEPTION = "This plan is not delegated.";
        public static string INITIATE_RECREDENTIALING_EXCEPTION = "Unable to Re-credential this provider for this plan.";
        public static string INITIATE_DECREDENTIALING_EXCEPTION = "Unable to De-credential this provider for this plan.";
        public static string INITIATE_DECREDENTIALING_EXIST_EXCEPTION = "De-credential already done for this provider and plan.";

        #endregion

        #region Profile Verification

        public static string ALL_PSV_GET_EXCEPTION = "Unable to get Primary Source Verification list";
        public static string PROFILE_VERIFICATIONDATA_GET_EXCEPTION = "Unable to get Profile Verification data";
        public static string SAVE_PROFILE_VERIFIED_DATA_EXCEPTION = "Unable to save Profile Verified data";
        public static string INITIATE_PSV_EXCEPTION = "Unable to initiate Primary Source Verification";
        public static string PSV_INPROGRESS_EXCEPTION = "Primary Source Verification is in progress";
        public static string SET_ALLVERIFIED_BEFORE_COMPLETE_EXCEPTION = "Please complete the Primary Source Verification before doing All Verified";
        public static string SET_ALLVERIFIED_EXCEPTION = "Unable complete Primary Source Verification";
        public static string GET_PENDING_PSV_EXCEPTION = "Unable get pending Primary Source Verification";
        public static string GET_VERIFIED_EXCEPTION = "Unable get verified data";
        public static string UPDATE_PROFILE_VERIFIED_DATA_EXCEPTION = "Unable to update Profile Verified data";

        #endregion

        #region CustomField

        public static string NO_CUSTOM_FIELD_EXCEPTION = "No Custom Field Found.";

        #endregion
    }
}
