using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.DocumentRepository;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.PackageGenerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.PackageGeneration
{
    public interface IPackageGeneratorManager
    {
        Task<Profile> GetAllDocuments(int profileID);
        Task<PackageGenerator> SavePackage();
        PackageGenerator CombinePdfs(int profileId, int LastCount, List<string> pdflist, string UserAuthId, int planId);

        Task<PackageGeneratorReport> AddPackageGeneratorReport(int ContractRequestID, string PackageGeneratorReportCode);

        List<AuditingPackageGenerationTracker> GetAllPackageGenerationTracker();
    }
}
