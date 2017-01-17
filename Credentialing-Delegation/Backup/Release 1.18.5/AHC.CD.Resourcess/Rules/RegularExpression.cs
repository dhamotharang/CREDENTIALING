using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Resources.Rules
{
    public static class RegularExpression
    {
        public const string FOR_ALPHABETS_SPACE_NUMBER = @"^[a-zA-Z ]*$";
        public const string FOR_ALPHABETS_SPACE_COMMA_HYPHEN = @"^[a-zA-Z ,-]*$";
        public const string FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT = @"^[a-zA-Z ,-.]*$";
        public const string FOR_ALPHABETS_NUMBER_HYPHEN_FRWD_SLASH = @"^[a-zA-Z0-9-/]*$";
        public const string DATE_FORMAT_MM_DD_YYYY = @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$";
        public const string FOR_NUMBERS = @"([0-9]+)";
        public const string FOR_ALPHABETS_SPACE_NUMBER_HYPHEN = @"^[a-zA-Z0-9 ,-]*$";
        public const string FOR_NUMBER_HYPHEN = @"^[0-9-]*$";
        public const string ALPHA_NUMERIC = @"^[a-zA-Z0-9]*$";
        public const string ALPHA_SPACE = @"^[a-zA-Z ]*$";
        public const string PERCENT_TWO_DECIMAL_PLACES = @"(^[0-9]\d{0,4}(\.\d{1,1})?%?)\d{0,1}$";
        public const string EMAIL_FORMAT = @"^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$";
        public const string PHONE_FAX_NUMBER = @"^[0-9]{10}$";
        public const string FOR_ALPHABETS_NUMBER_HYPHEN = @"^[a-zA-Z0-9-]*$";
        public const string FOR_ALPHABETS = @"([a-zA-Z]+)";
        public const string FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE = @"^[a-zA-Z ,-. â€‹'""]*$";
        public const string FOR_ALPHABETS_NUMBER_HYPHEN_FRWD_SLASH_DOT = @"^[a-zA-Z0-9-/.]*$";
        public const string FOR_ALPHABETS_NUMBER_HYPHEN_DOT = @"^[a-zA-Z0-9.]*$";
        public const string FOR_ALPHABETS_NUMBER_HYPHEN_DOT_SPACE = @"^[a-zA-Z0-9-. ]*$";
        public const string FOR_ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT = @"^[a-zA-Z0-9 ,.-]*$";
        public const string FOR_DATE_FORMAT = @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$";
    }
}
