using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.PDFGenerator
{
    public class PDFSupplementDataProf_IDsBusinessModel
    {
        # region Professional IDs

        #region DEA1
        public string SupplementFederalDEANumber1 { get; set; }
        public string SupplementFederalDEAIssueDate1 { get; set; }
        public string SupplementFederalDEAStateOfReg1 { get; set; }
        public string SupplementFederalDEAExpirationDate1 { get; set; }
        #endregion

        #region DEA2
        public string SupplementFederalDEANumber2 { get; set; }
        public string SupplementFederalDEAIssueDate2 { get; set; }
        public string SupplementFederalDEAStateOfReg2 { get; set; }
        public string SupplementFederalDEAExpirationDate2 { get; set; }
        #endregion

        #region CDS1
        public string SupplementCDSCertificateNumber1 { get; set; }
        public string SupplementCDSIssueDate1 { get; set; }
        public string SupplementCDSStateOfReg1 { get; set; }
        public string SupplementCDSExpirationDate1 { get; set; }
        #endregion

        #region CDS2

        public string SupplementCDSCertificateNumber2 { get; set; }
        public string SupplementCDSIssueDate2 { get; set; }
        public string SupplementCDSStateOfReg2 { get; set; }
        public string SupplementCDSExpirationDate2 { get; set; }

        #endregion

        #region State License1
        public string SupplementStateLicenseNumber1 { get; set; }
        public string SupplementStateLicenseIssuingState1 { get; set; }
        public string SupplementStateLicenseIssueDate1 { get; set; }
        public string SupplementStateLicenseAreYouPractisingInThisState1 { get; set; }
        public string SupplementStateLicenseExpirationDate1 { get; set; }
        public string SupplementStateLicenseStatusCode1 { get; set; }
        public string SupplementStateLicenseType1 { get; set; }
        #endregion

        #region State License2
        public string SupplementStateLicenseNumber2 { get; set; }
        public string SupplementStateLicenseIssuingState2 { get; set; }
        public string SupplementStateLicenseIssueDate2 { get; set; }
        public string SupplementStateLicenseAreYouPractisingInThisState2 { get; set; }
        public string SupplementStateLicenseExpirationDate2 { get; set; }
        public string SupplementStateLicenseStatusCode2 { get; set; }
        public string SupplementStateLicenseType2 { get; set; }
        #endregion

        #endregion
    }
}
