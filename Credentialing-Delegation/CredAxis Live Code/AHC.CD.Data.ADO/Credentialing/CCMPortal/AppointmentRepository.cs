using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Entities.Credentialing.CCMPortal;
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

namespace AHC.CD.Data.ADO.Credentialing.CCMPortal
{
    internal class AppointmentRepository : IAppointmentRepository
    {
        private DAPPERRepository _genericDapperRepo = null;
        private readonly IDbConnection connection = null;
        public AppointmentRepository()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString);
            this._genericDapperRepo = new DAPPERRepository();
        }

        async Task<List<CCMAppiontment>> IAppointmentRepository.GetCCMAppointmentsInfo(string ApprovalStatus)
        {
            IEnumerable<CCMAppiontment> CCMAppiontmentList = new List<CCMAppiontment>();
            string MainQuery = "select * from CCMAppointmentsView";
            // To apply the filter based on status 
            MainQuery = ApprovalStatus == null ? MainQuery : MainQuery + "where [ApprovalStatus] =" + ApprovalStatus;
            try
            {
                CCMAppiontmentList = await _genericDapperRepo.ExecuteQueryAsync<CCMAppiontment>(MainQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return CCMAppiontmentList.OrderBy(x => x.AppointmentDate).ToList();

        }


        async Task<CCMActionDTO> IAppointmentRepository.GetCCMActionData(int CredInfoID)
        {
            IEnumerable<CCMActionDTO> result = new List<CCMActionDTO>();
            string MainQuery = "select * from [CCMActionView] where [CredentialingInfoID] =" + CredInfoID;
            // To apply the filter based on status 
            try
            {
                result = await _genericDapperRepo.ExecuteQueryAsync<CCMActionDTO>(MainQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result.FirstOrDefault();
        }

        async Task<dynamic> IAppointmentRepository.SaveCCMQuickActionResultsAsync(CCMQuickActionDTO CCMActionResult)
        {
            dynamic CCMRequestStatus = new ExpandoObject();
            try
            {
                
                DataTable CCMActionRequests = ConstructListOfDataTableCCMRequest(CCMActionResult);
                connection.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CCMQuickActionRequests", CCMActionRequests);
                using (var multipleResult = await connection.QueryMultipleAsync("CCMQuickAction", parameters, commandType: CommandType.StoredProcedure))
                {
                    CCMRequestStatus = multipleResult.Read<dynamic>().ToList();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return CCMRequestStatus;
        }

        #region Private Methods

        private DataTable ConstructListOfDataTableCCMRequest(CCMQuickActionDTO CCMActionResult)
        {
            DataTable CCMRequest = new DataTable();
            DataRow row = null;
            CCMRequest.Columns.Add(new DataColumn("CredentialingInfoID", typeof(int)));
            CCMRequest.Columns.Add(new DataColumn("CredentialingAppointmentDetailID", typeof(int)));
            CCMRequest.Columns.Add(new DataColumn("ProfileId", typeof(int)));
            CCMRequest.Columns.Add(new DataColumn("SignaturePath", typeof(string)));
            CCMRequest.Columns.Add(new DataColumn("SignedDate", typeof(DateTime)));
            CCMRequest.Columns.Add(new DataColumn("SignedByID", typeof(string)));
            CCMRequest.Columns.Add(new DataColumn("RemarksForAppointments", typeof(string)));
            CCMRequest.Columns.Add(new DataColumn("AppointmentsStatus", typeof(string)));
            CCMRequest.Columns.Add(new DataColumn("RecommendedLevel", typeof(string)));
            foreach (var request in CCMActionResult.QuickActionSet)
            {
                row = CCMRequest.NewRow();
                row["CredentialingInfoID"]=request.CredentialingInfoId;
                row["CredentialingAppointmentDetailID"]=request.CredentialingAppointmentDetailID;
                row["ProfileId"]=request.ProfileId;
                row["SignaturePath"]=CCMActionResult.SignaturePath;
                row["SignedDate"]=CCMActionResult.SignedDate;
                row["SignedByID"]=CCMActionResult.SignedByID;
                row["RemarksForAppointments"]=CCMActionResult.RemarksForAppointments;
                row["AppointmentsStatus"]=CCMActionResult.AppointmentsStatus;
                row["RecommendedLevel"] = request.RecommendedLevel;
                //CCMRequest.Rows.Add(new dat{ request.CredentialingInfoId, 
                //    request.CredentialingAppointmentDetailID, 
                //    request.ProfileId, CCMActionResult.SignaturePath,
                //    CCMActionResult.SignedDate, CCMActionResult.SignedByID, 
                //    CCMActionResult.RemarksForAppointments, CCMActionResult.AppointmentsStatus, CCMActionResult.Level});
                CCMRequest.Rows.Add(row);
            }
            //CCMRequest.AsTableValuedParameter("[dbo].[CCMRequestTable]");
            CCMRequest.SetTypeName("CCMRequestTable");

            return CCMRequest;
        }

        #endregion
    }
}
