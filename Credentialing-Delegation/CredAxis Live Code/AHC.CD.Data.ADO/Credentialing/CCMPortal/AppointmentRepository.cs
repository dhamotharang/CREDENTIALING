using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Entities.Credentialing.CCMPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Credentialing.CCMPortal
{
    internal class AppointmentRepository : IAppointmentRepository
    {
        private DAPPERRepository _genericDapperRepo = null;
        public AppointmentRepository()
        {
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

    }
}
