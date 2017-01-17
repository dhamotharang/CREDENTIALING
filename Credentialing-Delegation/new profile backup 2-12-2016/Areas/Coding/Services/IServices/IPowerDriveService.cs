using PortalTemplate.Areas.Coding.Models.PowerDriveService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Services.IServices
{
    public interface IPowerDriveService
    {
        FileUploadResponse UploadFileService(FileService fileService);
        string DownLoadFile(string Path, UserInfo User);
        void SubscribeToGroupService(UserInfo userInfo, string GroupUsers, bool IsNewGroup);
    }
}