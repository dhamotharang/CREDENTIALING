using AHC.CD.Data.Repository.Notification;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
using AHC.CD.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Notification
{
    public class RealTimeNotificationADORepository : IRealTimeNotificationRepository
    {
        private string ConnString = ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString;

        public int GetAllPendingRequestCount(NotificationDelegate.DependencyDelegate dependencyDelegate)
        {
            string credQuery = @"select [CredentialingRequestID] FROM [dbo].[CredentialingRequests] where [Status] = 'Active'";
            string updateQuery = @"select [ProfileUpdatesTrackerId] FROM [dbo].[ProfileUpdatesTrackers] where [ApprovalStatus] in ('Pending','OnHold')";

            var updateRequestData = CreateSqlDependency(updateQuery, dependencyDelegate, CommandType.Text);
            var credRequestData = CreateSqlDependency(credQuery, dependencyDelegate, CommandType.Text);

            return updateRequestData.Rows.Count + credRequestData.Rows.Count;
        }

        public dynamic GetAllPendingRequest(NotificationDelegate.DependencyDelegate dependencyDelegate)
        {
            dynamic pendingRequestData = new ExpandoObject();
            DataTable pendingRequest = new DataTable();
            
            string requestQuery = @"select [ProfileUpdatesTrackerId],[oldData],[NewData],[NewConvertedData],[Section],[SubSection]
                                      ,[Url],[RespectiveObjectId],[ApprovalStatus],[Modification],[RejectionReason],[ProfileId],[LastModifiedBy],[LastModifiedDate],[UniqueData]
	                                  FROM [dbo].[ProfileUpdatesTrackers]";

            string credQuery = @"select [CredentialingRequestID] FROM [dbo].[CredentialingRequests] where [Status] = 'Active'";

            try
            {

                pendingRequest = CreateSqlDependency(requestQuery, dependencyDelegate, CommandType.Text);
                var credRequestData = CreateSqlDependency(credQuery, dependencyDelegate, CommandType.Text);

                var requestData = ConstructPendingRequestObject(pendingRequest);
                pendingRequestData.UPDATECOUNT = requestData.Where(s => s.Modification == ModificationType.Update.ToString() && s.ApprovalStatus == ApprovalStatusType.OnHold.ToString() 
                    && s.ApprovalStatus == ApprovalStatusType.Pending.ToString()).Count();
                pendingRequestData.RENEWALCOUNT = requestData.Where(s => s.Modification == ModificationType.Renewal.ToString() && s.ApprovalStatus == ApprovalStatusType.OnHold.ToString()
                    && s.ApprovalStatus == ApprovalStatusType.Pending.ToString()).Count();
                pendingRequestData.PROFILEUPDATERENEWAL = requestData.Where(s => s.ApprovalStatus == ApprovalStatusType.OnHold.ToString()
                    && s.ApprovalStatus == ApprovalStatusType.Pending.ToString());
                pendingRequestData.REQUESTCOUNT = credRequestData.Rows.Count;
                pendingRequestData.HISTORYCOUNT = requestData.Where(s => s.ApprovalStatus == ApprovalStatusType.Approved.ToString()
                    && s.ApprovalStatus == ApprovalStatusType.Rejected.ToString() && s.ApprovalStatus == ApprovalStatusType.Dropped.ToString()).Count();
                
                return pendingRequestData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        #region Private Methods

        /// <summary>
        /// Generic menthod to enable the sqldependency for any table of the same database
        /// </summary>
        /// <param name="query">query/storedprocedure name</param>
        /// <param name="method">on chenge method name</param>
        /// <param name="type">text/stored procedure</param>
        /// <returns>DataTable</returns>
        private DataTable CreateSqlDependency(string query, NotificationDelegate.DependencyDelegate method, CommandType type)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {

                    SqlCommand command = new SqlCommand(query, connection);

                    command.CommandType = type;
                    connection.Open();
                    var sqlDependency = new SqlDependency(command);

                    sqlDependency.OnChange += new OnChangeEventHandler(method);
                    SqlDataReader reader = command.ExecuteReader();

                    dataTable.Load(reader);
                    reader.Close();
                    connection.Close();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Entities.MasterProfile.ProfileUpdateRenewal.ProfileUpdatesTracker> ConstructPendingRequestObject(DataTable dataTable)
        {
            List<ProfileUpdatesTracker> pendingRequestList = new List<ProfileUpdatesTracker>();
            try
            {
                foreach (DataRow data in dataTable.Rows)
                {
                    ProfileUpdatesTracker pendingRequest = new ProfileUpdatesTracker
                    {
                        oldData = data["oldData"].ToString(),
                        NewData = data["NewData"].ToString(),
                        NewConvertedData = data["NewConvertedData"].ToString(),
                        Section = data["Section"].ToString(),
                        SubSection = data["SubSection"].ToString(),
                        Url = data["Url"].ToString(),
                        RespectiveObjectId = (int)data["RespectiveObjectId"],
                        ApprovalStatus = data["ApprovalStatus"].ToString(),
                        Modification = data["Modification"].ToString(),
                        RejectionReason = data["RejectionReason"].ToString(),
                        ProfileId = (int)data["ProfileId"],
                        LastModifiedBy = (int)data["LastModifiedBy"],
                        LastModifiedDate = Convert.ToDateTime(data["LastModifiedDate"]),
                        UniqueData = data["LastModifiedDate"].ToString()

                    };

                    pendingRequestList.Add(pendingRequest);
                }

                return pendingRequestList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        
    }
}
