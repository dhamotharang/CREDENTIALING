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
        public static string PROVIDER_NOT_FOUND = "Provider not found with the given Provider ID";
        public static string HOSPITAL_EXISTS = "Provider not found with the given Provider ID";
        public static string SSN_EXISTS = "This SSN no already exists";
        public static string UNABLE_TO_CREATE_FOLDER = "Unable to Create Folder for Document";
        public static string CREATE_FILE_EXCEPTION = "Unable to Create a Document";
    }
}
