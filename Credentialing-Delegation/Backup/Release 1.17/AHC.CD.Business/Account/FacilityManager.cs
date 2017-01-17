using AHC.CD.Data.Repository;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Account
{
   public class FacilityManager : IFacilityManager
    {
        private IGenericRepository<Facility> FacilityRepository { get; set; }

        public FacilityManager(IUnitOfWork uow)
        {
            this.FacilityRepository = uow.GetGenericRepository<Facility>();
        }
        
        public async Task<IEnumerable<Entities.MasterData.Account.Staff.Employee>> GetAllBusinessOfficeManagerAsync(int facilityId = 0, bool onlyActiveRecords = true)
        {
            try
            {
                var facility = await FacilityRepository.FindAsync(f => f.FacilityID == facilityId, "FacilityDetail.Employees.Employee.Designations.Designation");
                return facility.FacilityDetail.Employees.Select(e => e.Employee).Where(e => e.Designations.Any(d => d.DesignationID == EmployeeDesignationValue.OfficeManagerDesignationId));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Staff.Employee>> GetAllBiilingContactPersonAsync(int facilityId = 0, bool onlyActiveRecords = true)
        {
            try
            {
                var facility = await FacilityRepository.FindAsync(f => f.FacilityID == facilityId, "FacilityDetail.Employees.Employee.Designations.Designation");
                return facility.FacilityDetail.Employees.Select(e => e.Employee).Where(e => e.Designations.Any(d => d.DesignationID == EmployeeDesignationValue.BillingContactDesignationId));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Staff.Employee>> GetAllMidLevelPractionersAsync(int facilityId = 0, bool onlyActiveRecords = true)
        {
            try
            {
                var facility = await FacilityRepository.FindAsync(f => f.FacilityID == facilityId, "FacilityDetail.Employees.Employee.Designations.Designation");
                return facility.FacilityDetail.Employees.Select(e => e.Employee).Where(e => e.Designations.Any(d => d.DesignationID == EmployeeDesignationValue.MidLevelPractitionerDesignationId));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Staff.Employee>> GetAllPrimaryCredentialingContactPersonsAsync(int facilityId = 0, bool onlyActiveRecords = true)
        {
            try
            {
                var facility = await FacilityRepository.FindAsync(f => f.FacilityID == facilityId, "FacilityDetail.Employees.Employee.Designations.Designation");
                return facility.FacilityDetail.Employees.Select(e => e.Employee).Where(e => e.Designations.Any(d => d.DesignationID == EmployeeDesignationValue.PrimaryCredentialingContactDesignationId));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Staff.Employee>> GetAllPaymentAndRemittancePersonsAsync(int facilityId = 0, bool onlyActiveRecords = true)
        {
            try
            {
                var facility = await FacilityRepository.FindAsync(f => f.FacilityID == facilityId, "FacilityDetail.Employees.Employee.Designations.Designation");
                return facility.FacilityDetail.Employees.Select(e => e.Employee).Where(e => e.Designations.Any(d => d.DesignationID == EmployeeDesignationValue.PaymentAndRemittanceContactDesignationId));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Staff.Employee>> GetAllColleagueAsync(int facilityId = 0, bool onlyActiveRecords = true)
        {
            try
            {
                var facility = await FacilityRepository.FindAsync(f => f.FacilityID == facilityId, "FacilityDetail.Employees.Employee.Designations.Designation");
                return facility.FacilityDetail.Employees.Select(e => e.Employee).Where(e => e.Designations.Any(d => d.DesignationID == EmployeeDesignationValue.AssociateColleagueDesignationId || d.DesignationID == EmployeeDesignationValue.CoveringColleagueDesignationId || d.DesignationID == EmployeeDesignationValue.PartnerColleagueDesignationId));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
