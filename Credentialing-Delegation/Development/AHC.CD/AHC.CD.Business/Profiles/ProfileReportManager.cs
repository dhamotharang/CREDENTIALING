using AHC.CD.Business.DocumentWriter;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.DTO;
using AHC.CD.Resources.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
     public class ProfileReportManager : IProfileReportManager
    {
        IProfileRepository profileRepository = null;
        IUnitOfWork uow = null;
        private readonly IDocumentsManager iDocumentsManager = null;
        public ProfileReportManager(IUnitOfWork uow, IDocumentsManager iDocumentsManager)
        {

            this.profileRepository = uow.GetProfileRepository();
            this.iDocumentsManager = iDocumentsManager;
        }
        public ICollection<ProfileReport> GetProfileReport()
        {
             ICollection<ProfileReport> profileReports =  profileRepository.LoadProfilesForReport();
             return profileReports;
        }

        public string SaveProfileReportPDFFile(byte[] pdfbytes)
        {
            string documentPath = "";
            try
            {
                documentPath = iDocumentsManager.SaveProfileReportPDF(pdfbytes, DocumentRootPath.PROFILE_REPORT_DOCUMENT_PATH);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return documentPath;
        }
    }
}
