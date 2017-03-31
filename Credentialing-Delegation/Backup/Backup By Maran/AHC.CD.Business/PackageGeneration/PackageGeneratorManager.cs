using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.Credentialing.Loading;
using iTextSharp.text.pdf;
using System.IO;
using System.Web;
using AHC.CD.Entities.PackageGenerate;
using AHC.CD.Entities.MasterProfile;
using System.Web.Mvc;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.LoadingInformation;

namespace AHC.CD.Business.PackageGeneration
{
    internal class PackageGeneratorManager : IPackageGeneratorManager
    {
        private IUnitOfWork uow = null;
        public PackageGeneratorManager(IUnitOfWork uow) 
        {
            this.uow = uow;
        }



        public async Task<Profile> GetAllDocuments(int profileID)
        {
            try
            {
                var profile = uow.GetGenericRepository<Profile>();
               
                var selectedProfile=await profile.FindAsync(s=>s.ProfileID==profileID);

                return selectedProfile;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public PackageGenerator CombinePdfs(int profileId, int LastCount, List<string> pdflist, string UserAuthId, int planId)
        {
            string includeProperties = "PersonalDetail,PackageGenerator";
            var profileRepo = uow.GetGenericRepository<Profile>();
            Profile profile = profileRepo.Find(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.ProfileID == profileId, includeProperties);
            var c = LastCount + 1;
            string PackageFileName = profile.PersonalDetail.FirstName + profile.PersonalDetail.MaidenName + profile.PersonalDetail.LastName + "_" + c + ".pdf";

            string generatedPdfPath = HttpContext.Current.Request.MapPath("~/ApplicationDocument/GeneratedPackagePdf/" + PackageFileName);
            //string tempFilePath = HttpContext.Current.Server.MapPath("~/ApplicationDocument/TempGeneratedPdf");
            // step 1: creation of a document-object
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, new FileStream(generatedPdfPath, FileMode.Create));
            if (writer == null)
            {
                return null;
            }
            // step 3: we open the document
            document.Open();


            //check for a NonPDF image file and convert to pdf first before use
            for (int i = 0; i < pdflist.Count; i++)
            {
                PdfReader reader = null;
                try
                {
                    if (pdflist[i] != "" && (pdflist[i].Contains(".pdf") || pdflist[i].Contains(".PDF")))
                    {
                        reader = new PdfReader(HttpContext.Current.Request.MapPath(pdflist[i]));

                    }
                    else
                    {
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Request.MapPath(pdflist[i]));
                        FileStream fs = new FileStream(Path.GetFullPath(HttpContext.Current.Request.MapPath(pdflist[i])) + Path.GetFileNameWithoutExtension(HttpContext.Current.Request.MapPath(pdflist[i])) + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None);

                        iTextSharp.text.Document doc = new iTextSharp.text.Document(image);
                        PdfWriter writerPdf = PdfWriter.GetInstance(doc, fs);

                        doc.Open();
                        doc.NewPage();
                        image.SetAbsolutePosition(0, 0);
                        writerPdf.DirectContent.AddImage(image);
                        writerPdf.ResetPageCount();
                        doc.Close();
                        pdflist[i] = Path.GetFullPath(HttpContext.Current.Server.MapPath(pdflist[i])) + Path.GetFileNameWithoutExtension(HttpContext.Current.Server.MapPath(pdflist[i])) + ".pdf";
                        //pdflist[i] = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.MapPath(pdflist[i])) + ".pdf";

                        //string newFile = HttpContext.Current.Server.MapPath("~/Documents/OtherDocument/" + pdflist[i]);
                        reader = new PdfReader(pdflist[i]);

                    }

                    // we create a reader for a certain document
                    reader.ConsolidateNamedDestinations();
                    // step 4: we add content
                    for (int j = 1; j <= reader.NumberOfPages; j++)
                    {
                        PdfImportedPage page = writer.GetImportedPage(reader, j);
                        writer.AddPage(page);
                    }
                    PRAcroForm form = reader.AcroForm;
                    if (form != null)
                    {
                        writer.CopyAcroForm(reader);
                    }
                    reader.Close();
                }
                catch
                {

                    throw;

                }


            }
            // step 5: we close the document and writer
            writer.Close();
            document.Close();
            PackageGenerator packageGenerator = new PackageGenerator();
            packageGenerator = AddDocument(profileId, PackageFileName, LastCount, UserAuthId, planId);
            return packageGenerator;
        }


        public PackageGenerator AddDocument(int profileId, string PackageFileName, int LastCount, string UserAuthId, int planId)
        {
            PackageGenerator packageGenerator = new PackageGenerator();
            var c=LastCount+1;
            try
            {
                string includeProperties = "PackageGenerator";
                var profileRepo = uow.GetGenericRepository<Profile>();
                Profile profile = profileRepo.Find(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.ProfileID == profileId, includeProperties);

                packageGenerator.InitiatedByID = GetUserId(UserAuthId);
                packageGenerator.PackageFilePath = "\\ApplicationDocument\\GeneratedPackagePdf\\" + PackageFileName;
                packageGenerator.PackageName = PackageFileName;
                packageGenerator.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                packageGenerator.PlanID = planId;
                profile.PackageGenerator.Add(packageGenerator);
                profileRepo.Update(profile);
                profileRepo.Save();
                return packageGenerator;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int GetUserId(string authUserId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == authUserId);
                return user.CDUserID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<Entities.PackageGenerate.PackageGenerator> SavePackage()
        {
            throw new NotImplementedException();
        }


        public async Task<PackageGeneratorReport> AddPackageGeneratorReport(int ContractRequestID, string PackageGeneratorReportCode)
        {
            try
            {
                var ContractRequest = uow.GetGenericRepository<CredentialingContractRequest>();

                CredentialingContractRequest credentialingContractRequest = (from p in await ContractRequest.GetAllAsync("PackageGeneratorReport")
                                                                             where p.CredentialingContractRequestID == ContractRequestID
                                                                             select p).First();

                if (credentialingContractRequest.PackageGeneratorReport == null) {
                    credentialingContractRequest.PackageGeneratorReport = new PackageGeneratorReport();
                }

                credentialingContractRequest.PackageGeneratorReport.PackageGeneratorReportCode = PackageGeneratorReportCode;
                ContractRequest.Update(credentialingContractRequest);
                await ContractRequest.SaveAsync();
                return credentialingContractRequest.PackageGeneratorReport;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public List<AuditingPackageGenerationTracker> GetAllPackageGenerationTracker()
        {
            try
            {
                var trackerRepo = uow.GetGenericRepository<AuditingPackageGenerationTracker>();

                var trackers = trackerRepo.GetAll("GeneratedFor.PersonalDetail").ToList();
               

                return trackers;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
