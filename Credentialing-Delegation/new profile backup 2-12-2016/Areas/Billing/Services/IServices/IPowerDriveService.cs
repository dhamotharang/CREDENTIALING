
using PortalTemplate.Areas.Billing.Models.PowerDriveService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Billing.Services.IServices
{
    public interface IPowerDriveService
    {
        FileUploadResponse UploadFileService(FileService fileService);
        Task<Stream> DownLoadFile(string Path, UserInfo User);
        void SubscribeToGroupService(UserInfo userInfo, string GroupUsers, bool IsNewGroup);
    }
}