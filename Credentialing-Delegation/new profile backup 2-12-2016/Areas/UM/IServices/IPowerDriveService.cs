using PortalTemplate.Areas.UM.Models.PowerDriveService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IPowerDriveService
    {
        FileUploadResponse UploadFileService(FileService fileService);
        string DownLoadFile(string Path, UserInfo User);
        void SubscribeToGroupService(UserInfo userInfo, string GroupUsers, bool IsNewGroup);
        Task<Stream> PreviewFile(string Path, UserInfo User);
    }
}