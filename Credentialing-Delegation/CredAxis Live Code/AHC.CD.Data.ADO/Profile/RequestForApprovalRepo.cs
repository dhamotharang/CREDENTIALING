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
        private readonly DAPPERRepository dp = null;
        private readonly IDbConnection connection = null;
        public RequestForApprovalRepo()
        {
            dp = new DAPPERRepository();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString);
        }

        public async Task<dynamic> GetAllUpdatesAndRenewalsRepo()
        {
            dynamic UpdatesAndRenewalsData = new ExpandoObject();
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

        public async Task<dynamic> GetAllCredRequestHistory()
        {
            dynamic CredHistoryData = new ExpandoObject();
            connection.Open();
            string AllQuery = AdoQueries.UPDATECOUNT + AdoQueries.RENEWALCOUNT + AdoQueries.REQUESTCOUNT + AdoQueries.HISTORYCOUNT
                + AdoQueries.UPDATE_HISTORY_COUNT + AdoQueries.RENEWAL_HISTORY_COUNT + AdoQueries.REQUEST_HISTORY_COUNT + AdoQueries.CRED_REQUEST_HISTORY_DATA;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery))
                {
                    CredHistoryData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.HistoryIndividualCount = new ExpandoObject() as dynamic;
                    CredHistoryData.HistoryIndividualCount.UPDATEHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.HistoryIndividualCount.RENEWALHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.HistoryIndividualCount.CredREQUESTHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();                    
                    CredHistoryData.HISTORYDATA = MultipleResult.Read<dynamic>().ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredHistoryData;
        }

        public async Task<dynamic> GetAllCredRequestHistoryByID(int ID)
        {
            dynamic CredHistoryData = new ExpandoObject();
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
            string AllQuery = AdoQueries.PROVIDERUPDATECOUNT + AdoQueries.PROVIDERRENEWALCOUNT + AdoQueries.PROVIDERREQUESTCOUNT + AdoQueries.PROVIDERHISTORYCOUNT
                + AdoQueries.PROVIDER_UPDATE_HISTORY_COUNT + AdoQueries.PROVIDER_RENEWAL_HISTORY_COUNT 
                + AdoQueries.PROVIDER_REQUEST_HISTORY_COUNT + AdoQueries.PROVIDER_CRED_REQUEST_HISTORY_DATA;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery, parameters))
                {
                    CredHistoryData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.HistoryIndividualCount = new ExpandoObject() as dynamic;
                    CredHistoryData.HistoryIndividualCount.UPDATEHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.HistoryIndividualCount.RENEWALHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.HistoryIndividualCount.CredREQUESTHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    CredHistoryData.HISTORYDATA = MultipleResult.Read<dynamic>().ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredHistoryData;
        }

        public async Task<dynamic> GetAllUpdateRequestHistory()
        {
            dynamic UpdateHistoryData = new ExpandoObject();
            connection.Open();
            string AllQuery = AdoQueries.UPDATECOUNT + AdoQueries.RENEWALCOUNT + AdoQueries.REQUESTCOUNT + AdoQueries.HISTORYCOUNT
                + AdoQueries.UPDATE_HISTORY_COUNT + AdoQueries.RENEWAL_HISTORY_COUNT + AdoQueries.REQUEST_HISTORY_COUNT + AdoQueries.UPDATE_HISTORY;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery))
                {
                    UpdateHistoryData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.HistoryIndividualCount = new ExpandoObject() as dynamic;
                    UpdateHistoryData.HistoryIndividualCount.UPDATEHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.HistoryIndividualCount.RENEWALHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.HistoryIndividualCount.CredREQUESTHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.HISTORYDATA = MultipleResult.Read<dynamic>().ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UpdateHistoryData;
        }

        public async Task<dynamic> GetAllUpdateRequestHistoryByID(int ID)
        {
            dynamic UpdateHistoryData = new ExpandoObject();
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
            string AllQuery = AdoQueries.PROVIDERUPDATECOUNT + AdoQueries.PROVIDERRENEWALCOUNT + AdoQueries.PROVIDERREQUESTCOUNT + AdoQueries.PROVIDERHISTORYCOUNT + 
                              AdoQueries.PROVIDER_UPDATE_HISTORY_COUNT + AdoQueries.PROVIDER_RENEWAL_HISTORY_COUNT + 
                              AdoQueries.PROVIDER_REQUEST_HISTORY_COUNT + AdoQueries.PROVIDER_UPDATE_HISTORY;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery, parameters))
                {
                    UpdateHistoryData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.HistoryIndividualCount = new ExpandoObject() as dynamic;
                    UpdateHistoryData.HistoryIndividualCount.UPDATEHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.HistoryIndividualCount.RENEWALHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.HistoryIndividualCount.CredREQUESTHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    UpdateHistoryData.HISTORYDATA = MultipleResult.Read<dynamic>().ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UpdateHistoryData;
        }

        public async Task<dynamic> GetAllRenewalRequestHistory()
        {
            dynamic RenewalHistoryData = new ExpandoObject();
            connection.Open();
            string AllQuery = AdoQueries.UPDATECOUNT + AdoQueries.RENEWALCOUNT + AdoQueries.REQUESTCOUNT + AdoQueries.HISTORYCOUNT + 
                              AdoQueries.UPDATE_HISTORY_COUNT + AdoQueries.RENEWAL_HISTORY_COUNT + AdoQueries.REQUEST_HISTORY_COUNT + AdoQueries.RENEWAL_HISTORY;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery))
                {
                    RenewalHistoryData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.HistoryIndividualCount = new ExpandoObject() as dynamic;
                    RenewalHistoryData.HistoryIndividualCount.UPDATEHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.HistoryIndividualCount.RENEWALHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.HistoryIndividualCount.CredREQUESTHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.HISTORYDATA = MultipleResult.Read<dynamic>().ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RenewalHistoryData;
        }

        public async Task<dynamic> GetAllRenewalRequestHistoryByID(int ID)
        {
            dynamic RenewalHistoryData = new ExpandoObject();
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
            string AllQuery = AdoQueries.PROVIDERUPDATECOUNT + AdoQueries.PROVIDERRENEWALCOUNT + AdoQueries.PROVIDERREQUESTCOUNT + AdoQueries.PROVIDERHISTORYCOUNT
                + AdoQueries.PROVIDER_UPDATE_HISTORY_COUNT + AdoQueries.PROVIDER_RENEWAL_HISTORY_COUNT
                + AdoQueries.PROVIDER_REQUEST_HISTORY_COUNT + AdoQueries.PROVIDER_RENEWAL_HISTORY;
            try
            {
                using (var MultipleResult = await connection.QueryMultipleAsync(AllQuery, parameters))
                {
                    RenewalHistoryData.UPDATECOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.RENEWALCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.REQUESTCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.HISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.HistoryIndividualCount = new ExpandoObject() as dynamic;
                    RenewalHistoryData.HistoryIndividualCount.UPDATEHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.HistoryIndividualCount.RENEWALHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.HistoryIndividualCount.CredREQUESTHISTORYCOUNT = MultipleResult.Read<int>().FirstOrDefault();
                    RenewalHistoryData.HISTORYDATA = MultipleResult.Read<dynamic>().ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RenewalHistoryData;
        }

        public async Task<dynamic> ApproveAllCredentialingRequestBYIDS(string CredentialingRequestIDs, int UserID)
        {
            dynamic UpdatesAndRenewalsData = new ExpandoObject();
            try
            {
                connection.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CredRequestIDS", CredentialingRequestIDs);
                parameters.Add("@UserID", UserID);
                using (var MultipleResult = await connection.QueryMultipleAsync("ApproveAllCredRequest", parameters, commandType: CommandType.StoredProcedure))
                {
                    UpdatesAndRenewalsData = MultipleResult.Read<dynamic>().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UpdatesAndRenewalsData;
        }
    }
}
