using PortalTemplate.Areas.Billing.Models.CreateClaim;
using PortalTemplate.Areas.Billing.Models.File_Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Billing.Services.IServices
{
    public interface IFileManagementService
    {
        List<File837ViewModel> Get837TableList();

        List<File837ViewModel> Get837TableByIndex(int index, bool sortingType, string sortBy, File837ViewModel SearchObject);

        List<File835> Get835TableList();

        List<File835> Get835TableByIndex(int index, bool sortingType, string sortBy, File835 SearchObject);

        List<File277ViewModel> Get277FileList();

        List<File277ViewModel> Get277TableByIndex(int index, bool sortingType, string sortBy, File277ViewModel SearchObject);

        List<File999ViewModel> Get999FileList();

        List<File999ViewModel> Get999TableByIndex(int index, bool sortingType, string sortBy, File999ViewModel SearchObject);

        List<ClaimList> GetClaimList(string IncomeFileLoggerID);

        List<ClaimList> Get837ClaimListTableListByIndex(int index, bool sortingType, string sortBy, ClaimList SearchObject, string IncomingFileId);

        List<File835ProviderInfo> Get835ProviderList(int InterKey, string CheckNumber);

        List<File835EOBList> Get835EobList(int InterKey, string HeaderKey, string NPI);

        Cms1500ViewModels GetCms1500Form(int ClaimId);

        Task<Stream> DownloadFile(string Path, Models.PowerDriveService.UserInfo User);
    }
}
