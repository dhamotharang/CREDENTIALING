using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Resources.DatabaseQueries;

namespace AHC.CD.Entities.DTO
{
    public class AuditSearchDTO
    {

        public string AuditCategory { get; set; }

        public List<AuditFilterDTO> FilterByFields { get; set; }

        public AuditPaginationDTO Pagination { get; set; }

        public AuditSortingDTO Sorting { get; set; }

    }

    #region Helper Classes

    public class AuditFilterDTO
    {
        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        #region QueueFilter

        #region Dictionary for SqlFunctions

        private readonly Dictionary<string, StringBuilder> sqlFunctions = new Dictionary<string, StringBuilder>
        {
            {"datetime", new StringBuilder("FORMAT([DateTime],'M/d/yyyy h:mm:ss tt') like '%@datetime%'")},
        };

        #endregion

        public string ApplyFunctions(string property, string value)
        {
            System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder();

            if (sqlFunctions.Keys.Any(key => key == property.ToLower()) && !String.IsNullOrEmpty(value))
            {
                return sqlFunctions[property.ToLower()].Replace(("@" + property.ToLower()), value).ToString();
            }
            else if (!String.IsNullOrEmpty(value))
            {
                return property.Sql_Like(value);
            }
            else return "";
        }

        private bool HasSpecialChars(string value)
        {
            return value.Trim().Any(ch => !Char.IsLetterOrDigit(ch));
        }

        #endregion

    }

    public class AuditPaginationDTO
    {

        public int pageNumber { get; set; }

        public int pageOffset { get; set; }

    }

    public class AuditSortingDTO
    {
        public string columnName { get; set; }

        public string sortingOrder { get; set; }
    }

    public class AuditLogCategory
    {
        public string AuditCategory { get; set; }
    }

    #endregion

}
