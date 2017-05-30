using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Resources.DatabaseQueries
{
    public static class StringExtensions
    {

        private static SqlCommandBuilder builder = new SqlCommandBuilder();

        
        public static string Sql_Like(this string source, string value)
        {
            if (!String.IsNullOrEmpty(value) && !String.IsNullOrEmpty(source))
                return builder.QuoteIdentifier(source) + " like '%" + value + "%' ";
            else
                return " ";
        }


        public static string Sql_StartsWith(this string source, string value)
        {
            if (!String.IsNullOrEmpty(value) && !String.IsNullOrEmpty(source))
                return builder.QuoteIdentifier(source) + " like '" + value + "%' ";
            else
                return " ";
        }

        
        public static string Sql_EndsWith(this string source, string value)
        {
            if (!String.IsNullOrEmpty(value) && !String.IsNullOrEmpty(source))
                return builder.QuoteIdentifier(source) + " like '%" + value + "' ";
            else
                return " ";
        }

        
        public static string Sql_Date_Like(this string source, string value)
        {
            if (!String.IsNullOrEmpty(value) && !String.IsNullOrEmpty(source))
                return " convert(varchar,convert(date, " + builder.QuoteIdentifier(source) + ", 103) ,101) like '%" + value + "%' ";
            else
                return " ";
        }

        
        public static string Sql_Phone_Like(this string source, string value)
        {
            if (!String.IsNullOrEmpty(value) && !String.IsNullOrEmpty(source))
                return " replace(replace(replace(replace(" + builder.QuoteIdentifier(source) + ",'(',''),')',''),'-',''),' ','') like '%" + value + "%' ";
            else
                return " ";
        }

        
        public static string Sql_OrderBy(this string source, string sortOrder)
        {
            if (!String.IsNullOrEmpty(source))
                return " order by " + builder.QuoteIdentifier(source) + " " + (!String.IsNullOrEmpty(sortOrder) ? sortOrder : " asc ");
            else
                return " ";
        }

        
        public static string Sql_Offset(this string source, string pageLimit)
        {
            if (!String.IsNullOrEmpty(source))
                return " offset " + source + " rows fetch next " + pageLimit + " rows only ";
            else
                return " ";
        }

        
        private static bool HasSpecialChars(string value)
        {
            return value.Trim().Any(ch => !Char.IsLetterOrDigit(ch));
        }


    }
}
