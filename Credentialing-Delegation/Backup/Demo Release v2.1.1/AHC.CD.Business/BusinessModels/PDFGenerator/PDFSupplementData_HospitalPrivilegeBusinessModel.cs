using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.PDFGenerator
{
    public class PDFSupplementData_HospitalPrivilegeBusinessModel
    {
        #region Hospital Privileges
        public string SupplementHospitalName { get; set; }
        public string SupplementHospitalNumber { get; set; }
        public string SupplementHospitalStreet { get; set; }
        public string SupplementHospitalSuiteBuilding { get; set; }
        public string SupplementHospitalCity { get; set; }
        public string SupplementHospitalState { get; set; }
        public string SupplementHospitalZip { get; set; }
        public string SupplementHospitalTelephone { get; set; }
        public string SupplementHospitalFax { get; set; }
        public string SupplementHospitalDepartmentName { get; set; }
        public string SupplementHospitalDepartmentDirectorLastName { get; set; }
        public string SupplementHospitalDepartmentDirectorFirstName { get; set; }
        public string SupplementHospitalAffiliationStartDate { get; set; }
        public string SupplementHospitalAffiliationEndDate { get; set; }
        public string SupplementHospitalUnrestrictedPrivilege { get; set; }
        public string SupplementHospitalPrivilegesTemporary { get; set; }
        public string SupplementHospitalAdmittingPrivilegeStatus { get; set; }
        public double? SupplementHospitalPercentageAnnualAdmission { get; set; }
        public string SupplementHospitalExplain { get; set; }

        #endregion
    }
}
