using AHC.CD.Entities.MasterData.Account.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Account
{
    public interface IFacilityManager
    {
        Task<IEnumerable<Employee>> GetAllBusinessOfficeManagerAsync(int facilityId = 0, bool onlyActiveRecords = true);
        Task<IEnumerable<Employee>> GetAllBiilingContactPersonAsync(int facilityId = 0, bool onlyActiveRecords = true);
        Task<IEnumerable<Employee>> GetAllMidLevelPractionersAsync(int facilityId = 0, bool onlyActiveRecords = true);
        Task<IEnumerable<Employee>> GetAllPrimaryCredentialingContactPersonsAsync(int facilityId = 0, bool onlyActiveRecords = true);
        Task<IEnumerable<Employee>> GetAllPaymentAndRemittancePersonsAsync(int facilityId = 0, bool onlyActiveRecords = true);
        Task<IEnumerable<Employee>> GetAllColleagueAsync(int facilityId = 0, bool onlyActiveRecords = true);
    }
}
