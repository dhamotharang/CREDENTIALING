using AHC.ActivityLogging;
using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Entities.DTO;
using AHC.CD.Resources.DatabaseQueries;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.AuditLog
{
    public class AuditLogADO : IAuditLogADO
    {
        private readonly DAPPERRepository dp = null;
        private readonly IDbConnection connection;
        public AuditLogADO()
        {
            this.dp = new DAPPERRepository();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString);
        }

        public List<dynamic> GetAllAuditLogADOAsync()
        {
            List<dynamic> Data = new List<dynamic>();
            try
            {
                string query = "SELECT * FROM [dbo].[AuditMessages]";
                Data = dp.ExecuteQueryForAuditLogger<dynamic>(query);
            }
            catch (Exception e)
            {
                throw e;
            }
            return Data;
        }

        public async Task<AduitDTO> GetAllAuditLog(AuditSearchDTO auditSearchDTO)
        {
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            AduitDTO auditMessage = new AduitDTO();
            string auditLogGenericQuery = QueryBuilder(auditSearchDTO);
            string AllQuery = AdoQueries.AUDITLOGTOTALCOUNT + AdoQueries.AUDITLOGINFORMATIONCOUNT + AdoQueries.AUDITLOGALERTCOUNT + auditLogGenericQuery;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery))
                {
                    auditMessage.TotalLogCount = MultipleResult.Read<int>().FirstOrDefault();
                    auditMessage.InformationLogCount = MultipleResult.Read<int>().FirstOrDefault();
                    auditMessage.AlertLogCount = MultipleResult.Read<int>().FirstOrDefault();
                    auditMessage.AuditMessages = MultipleResult.Read<AuditMessageDTO>().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return auditMessage;
        }




        #region Constructing generic Query

        private string QueryBuilder(AuditSearchDTO searchCriteria)
        {
            StringBuilder searchQuery = new StringBuilder(" ");
            System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder();
            int Flag = 0;
            #region Dynamic Conditions & Query Generation

            if (searchCriteria.FilterByFields != null && searchCriteria.FilterByFields.Count > 0)
            {
                searchQuery.Append(" where ");
                Flag = 1;
                for (int i = 0; i < searchCriteria.FilterByFields.Count; i++)
                {
                    searchQuery.Append(searchCriteria.FilterByFields[i].ApplyFunctions(searchCriteria.FilterByFields[i].FieldName, searchCriteria.FilterByFields[i].FieldValue) + ((i + 1) == searchCriteria.FilterByFields.Count ? "" : " and "));
                }
            }

            if (searchCriteria.AuditCategory == "Information" || searchCriteria.AuditCategory == "Alert")
            {
                if (Flag == 0)
                {
                    searchQuery.Append(" where ");
                    searchQuery.Append("Category like '%" + searchCriteria.AuditCategory + "%'");
                }
                else
                {
                    searchQuery.Append(" and "+ "Category like '%" + searchCriteria.AuditCategory + "%'");
                }
            }

            StringBuilder finalQuery = new StringBuilder(AdoQueries.AUDITLOGGENERICQUERY);

            #region orderBy For Getting Records like StartsWith, Contains and EndsWith

            var offsetQuery = searchCriteria.Pagination != null ? searchCriteria.Pagination.pageOffset.ToString().Sql_Offset(searchCriteria.Pagination.pageNumber.ToString()) : "0".Sql_Offset("10000");

            if (searchCriteria.FilterByFields != null && searchCriteria.FilterByFields.Count > 0)
            {
                var tempObj = searchCriteria.FilterByFields[0];

                searchQuery.Append(" order by case when " + tempObj.FieldName.Sql_StartsWith(tempObj.FieldValue) + " then 1");

                searchQuery.Append(" when " + tempObj.FieldName.Sql_EndsWith(tempObj.FieldValue) + " then 3 else 2 end, " + builder.QuoteIdentifier(tempObj.FieldName) + " asc " + offsetQuery);
                finalQuery.Replace("[REPLACESEARCHQUERY]", searchQuery.ToString());
                finalQuery.Replace("[REPLACESORTQUERY]", searchCriteria.Sorting != null ? searchCriteria.Sorting.columnName.Sql_OrderBy(searchCriteria.Sorting.sortingOrder) : "");

            }
            else
            {
                finalQuery.Replace("[REPLACESEARCHQUERY]", searchQuery.ToString());
                finalQuery.Replace("[REPLACESORTQUERY]",searchCriteria.Sorting != null ? searchCriteria.Sorting.columnName.Sql_OrderBy(searchCriteria.Sorting.sortingOrder) + offsetQuery : "");
            }

            return finalQuery.ToString();

            #endregion

            #endregion
        }

        #endregion
    }
}
