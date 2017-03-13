using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Resources.DatabaseQueries;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Profile
{
    internal class RequestForApprovalRepo : IRequestForApprovalRepo
    {
        DAPPERRepository dp = null;

        public RequestForApprovalRepo()
        {
            this.dp = new DAPPERRepository();
        }

        public async Task<dynamic> GetAllUpdatesAndRenewalsRepo()
        {
            dynamic UpdatesAndRenewalsData = new ExpandoObject();
            IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString);
            connection.Open();
            string AllQuery = AdoQueries.UPDATECOUNT + AdoQueries.RENEWALCOUNT + AdoQueries.REQUESTCOUNT + AdoQueries.HISTORYCOUNT + AdoQueries.PROFILEUPDATERENEWAL;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery))
                {
                    UpdatesAndRenewalsData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdatesAndRenewalsData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdatesAndRenewalsData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdatesAndRenewalsData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdatesAndRenewalsData.PROFILEUPDATERENEWAL = MultipleResult.Read<dynamic>().ToList();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UpdatesAndRenewalsData;
        }

        public async Task<dynamic> GetAllCredentialRequestsRepo()
        {
            dynamic CredentialRequestsData = null;
            string Query = AdoQueries.CREDREQUEST;
            try
            {
                CredentialRequestsData = await dp.ExecuteQueryAsync<dynamic>(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredentialRequestsData;
        }

        public async Task<dynamic> GetAllHistoryRepo()
        {
            dynamic HistoryData = null;
            string Query = AdoQueries.UPDATERENEWALHISTORY;
            try
            {
                HistoryData = await dp.ExecuteQueryAsync<dynamic>(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return HistoryData;
        }

        public async Task<dynamic> GetCredRequestDataByIDRepo(int ID)
        {
            dynamic CredRequestDataByIDData = null;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
            string Query = AdoQueries.CREDREQUESTDATABYID;
            try
            {
                CredRequestDataByIDData = await dp.ExecuteAsyncQueryFirstOrDefault<dynamic>(parameters,Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredRequestDataByIDData;
        }

        public async Task<dynamic> GetCredRequestHistoryDataByIDRepo(int ID)
        {
            dynamic CredRequestHistoryDataByIDData = null;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
            string Query = AdoQueries.CREDREQUESTHISTORYDATABYID;
            try
            {
                CredRequestHistoryDataByIDData = await dp.ExecuteAsyncQueryFirstOrDefault<dynamic>(parameters,Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredRequestHistoryDataByIDData;
        }
    }
}
