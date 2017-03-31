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
            dynamic CredentialRequestsData = new ExpandoObject();
            IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString);
            connection.Open();
            string AllQuery = AdoQueries.UPDATECOUNT + AdoQueries.RENEWALCOUNT + AdoQueries.REQUESTCOUNT + AdoQueries.HISTORYCOUNT + AdoQueries.CREDREQUEST;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery))
                {
                    CredentialRequestsData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredentialRequestsData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredentialRequestsData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredentialRequestsData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredentialRequestsData.CREDENTIALINGREQUEST = MultipleResult.Read<dynamic>().ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredentialRequestsData;
        }

        public async Task<dynamic> GetAllHistoryRepo()
        {
            dynamic HistoryData = new ExpandoObject();
            IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString);
            connection.Open();
            string AllQuery = AdoQueries.UPDATECOUNT + AdoQueries.RENEWALCOUNT + AdoQueries.REQUESTCOUNT + AdoQueries.HISTORYCOUNT + AdoQueries.UPDATERENEWALHISTORY;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery))
                {
                    HistoryData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    HistoryData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    HistoryData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    HistoryData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    HistoryData.PROFILEUPDATEHISTORY = MultipleResult.Read<dynamic>().ToList();
                }

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

        public async Task<dynamic> GetAllUpdatesAndRenewalsForProviderRepo(int ID)
        {
            dynamic UpdatesAndRenewalsData = new ExpandoObject();
            IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString);
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
            string AllQuery = AdoQueries.PROVIDERUPDATECOUNT + AdoQueries.PROVIDERRENEWALCOUNT + AdoQueries.PROVIDERREQUESTCOUNT + AdoQueries.PROVIDERHISTORYCOUNT + AdoQueries.PROVIDERPROFILEUPDATERENEWAL;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery, parameters))
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

        public async Task<dynamic> GetAllCredentialRequestsForProviderRepo(int ID)
        {
            dynamic CredentialRequestsData = new ExpandoObject();
            IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString);
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
            string AllQuery = AdoQueries.PROVIDERUPDATECOUNT + AdoQueries.PROVIDERRENEWALCOUNT + AdoQueries.PROVIDERREQUESTCOUNT + AdoQueries.PROVIDERHISTORYCOUNT + AdoQueries.PROVIDERCREDREQUEST;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery, parameters))
                {
                    CredentialRequestsData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredentialRequestsData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredentialRequestsData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredentialRequestsData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredentialRequestsData.CREDENTIALINGREQUEST = MultipleResult.Read<dynamic>().ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredentialRequestsData;
        }

        public async Task<dynamic> GetAllHistoryForProviderRepo(int ID)
        {
            dynamic HistoryData = new ExpandoObject();
            IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString);
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
            string AllQuery = AdoQueries.PROVIDERUPDATECOUNT + AdoQueries.PROVIDERRENEWALCOUNT + AdoQueries.PROVIDERREQUESTCOUNT + AdoQueries.PROVIDERHISTORYCOUNT + AdoQueries.PROVIDERUPDATERENEWALHISTORY;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery, parameters))
                {
                    HistoryData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    HistoryData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    HistoryData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    HistoryData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    HistoryData.PROFILEUPDATEHISTORY = MultipleResult.Read<dynamic>().ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return HistoryData;
        }
    }
}
