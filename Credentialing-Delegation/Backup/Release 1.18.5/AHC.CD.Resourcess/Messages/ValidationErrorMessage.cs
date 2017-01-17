using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Resources.Messages
{
    /// <summary>
    /// Container class for all Model Validation Error Messages
    /// Store each error message in a static readonly string member variable
    /// </summary>
    public static class ValidationErrorMessage
    {
        public static readonly string PROVIDER_FIRSTNAME_REQUIRED = "Provider First Name Required";

        #region Required Messages

        public const string REQUIRED_ENTER = "Please enter {0}.";
        public const string REQUIRED_SELECT = "Please select the {0}.";
        public const string REQUIRED_SPECIFY = "Please specify whether {0}.";
        public const string REQUIRED_SPECIFY_LISTED = "Please specify whether you wish to be listed for {0}";

        #endregion

        #region String Length

        public const string NUMBER_LENGTH_MAX_MIN = "{0} must be between {2} and {1} digits in length.";
        public const string STRING_LENGTH_MAX_MIN = "{0} must be between {2} and {1} characters in length.";
        public const string STRING_LENGTH_MAX = "{0} must be less than {1} characters in length.";
        public const string STRING_LENGTH_FIXED = "{0} must be of {1} characters in length.";

        #endregion


        #region Unique

        public const string UNIQUE_DATA = "{0} used, {0} must be Unique.";

        #endregion
        public const string ALPHABETS_SPACE_NUMBER = "Please enter valid {0}. Only alphabets, spaces and numbers accepted.";

        public const string ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE = "Please enter valid {0}. Only alphabets, spaces, comma, hyphens, dot, quotes and apostrophe accepted.";
        public const string CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN = "Please enter valid {0}. Only alphabets, spaces, comma and hyphen accepted.";
        public const string CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT = "Please enter valid {0}. Only alphabets, spaces, comma, hyphen and dot accepted.";
        public const string CHARACTERS_ONLY_ALPHABETS_NUMBERS_HYPHEN = "Please enter valid {0}. Only alphabets, numbers and hyphen accepted.";
        public const string CHARACTERS_ONLY_ALPHABETS_NUMBERS_HYPHEN_FRWD_SLASH = "Please enter valid {0}. Only alphabets, numbers, hyphen and forward slash accepted.";
        public const string CHARACTERS_ONLY_NUMBERS = "Please enter valid {0}. Only Numeric Digits accepted.";
        public const string CHARACTERS_ONLY_ALPHABETS_NUMBERS_SPACE_HYPHEN = "Please enter valid {0}. Only alphabets, numbers, space and hyphen accepted.";
        public const string CHARACTERS_ONLY_NUMBERS_HYPHEN = "Please enter valid {0}. Only numbers and hyphen accepted.";
        public const string CHARACTERS_ONLY_ALPHABETS_SPACE = "Please enter valid {0}. Only numbers and space accepted.";
        public const string ALPHA_NUMERIC = "Please enter valid {0}. Only alphabets and numbers accepted.";
        public const string PERCENT_TWO_DECIMAL_PLACES = "Please enter valid {0}. Only numbers and decimals accepted.";
        public const string PHONE_FAX_NUMBER = "Please enter only 10 digits in {0}.";
        public const string CHARACTERS_ONLY_ALPHABETS = "Please enter valid {0}. Only alphabets accepted.";

        public const string CHARACTERS_ONLY_ALPHABETS_NUMBERS_HYPHEN_FRWD_SLASH_DOT = "Please enter valid {0}. Only alphabets, numbers, hyphen, forward slash and dot accepted.";
        public const string FOR_ALPHABETS_NUMBER_HYPHEN_DOT = "Please enter valid {0}. Only alphabets, numbers, hyphen and dot accepted.";
        public const string ALPHABETS_NUMBER_HYPHEN_DOT_SPACE = "Please enter valid {0}. Only alphabets, numbers, hyphen, dot and space accepted.";
        public const string ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT = "Please enter valid {0}. Only alphabets, numbers, hyphen, dot, comma and space accepted.";

        public const string REQUIRED_START_DATE = "Please enter Start Date.";
        public const string DATE_FORMAT_INVALID = "Please enter date in mm/dd/yyyy format.";
        public const string DATE_NOT_GREATER_THAN = "{0} should not be greater than {1}.";
        public const string START_DATE_RANGE = "Date should be greater than {2} and less than {1}";
        public const string STOP_DATE_RANGE = "Date should be greater than start date and less than {1}";
        public const string NOT_GREATER_THAN_CURRENT_DATE = "{0} should not be greater than current date.";
        public const string DATE_NOT_LESS_THAN = "{0} should not be less than or equal to {1}.";
        public const string DATE_GREATER_THAN_START_DATE = "Date should be greater than start date";

        public const string UPLOAD_FILE_EXTENSION_ELIGIBLE = "Please select the file of type {1}";
        public const string UPLOAD_FILE_SIZE_ELIGIBLE = "{0} should be less than {1} in size";

        public const string INVALID_EMAIL_FORMAT = "Please enter the valid email address.";

        public const string FOR_DATE_FORMAT = "Please enter the date in MM/DD/YYYY format.";



    }
}
