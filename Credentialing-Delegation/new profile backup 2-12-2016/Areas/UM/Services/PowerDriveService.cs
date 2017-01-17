using Newtonsoft.Json;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.PowerDriveService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.UM.Services
{
    public class PowerDriveService : IPowerDriveService
    {
        public FileUploadResponse UploadFileService(FileService fileService)
        {
            HttpResponseMessage result;
            using (HttpClient client = new HttpClient())
            {
                result = new HttpResponseMessage();
                using (MultipartFormDataContent mpfdc = new MultipartFormDataContent())
                {
                    foreach (var DocumentAndStream in fileService.DocumentAndStreams)
                    {
                        if (TransferType.Stream == DocumentAndStream.Document.TransferType)
                            mpfdc.Add(new StreamContent(DocumentAndStream.InputStream), "File", DocumentAndStream.Document.FileName);
                        mpfdc.Add(new StringContent(JsonConvert.SerializeObject(DocumentAndStream.Document)), "Documents");

                    }

                    mpfdc.Add(new StringContent(JsonConvert.SerializeObject(fileService.UserInfo)), "UserInfo");
                    var requestUri = ConfigurationManager.AppSettings["WebAPIURL"] + "api/UploadFiles";//?UserID=1";
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    result = client.PostAsync(requestUri, mpfdc).GetAwaiter().GetResult();
                    string res = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    // HTTP GET
                    //HttpResponseMessage response = await client.GetAsync(requestUri);
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    result = await response.Content.ReadAsAsync<object>();
                    //}
                    //return (result.StatusCode == System.Net.HttpStatusCode.OK) ? true : false;

                    FileUploadResponse response = JsonConvert.DeserializeObject<FileUploadResponse>(res);
                    return response;
                }
            }
        }

        public string DownLoadFile(string Path,UserInfo User)
        {
            //string FileName = "";
            string FilePath = "";
            string PathOfFile = "";
            HttpResponseMessage result;
            HttpResponseMessage hpm = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                result = new HttpResponseMessage();

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(JsonConvert.SerializeObject(Path)), "Path");
                    formData.Add(new StringContent(JsonConvert.SerializeObject(User)), "User");

                    var requestUri = ConfigurationManager.AppSettings["WebAPIURL"] + "api/DownLoadFile";

                    result = client.PostAsync(requestUri, formData).Result;
                    var stream = result.Content.ReadAsStreamAsync();
                    var FileNamess = result.Content.Headers.ContentDisposition.FileName.Replace("\"", "");
                    // FilePath = "E:\\277\\Sample\\EDIFiles" + FileNamess;
                    string sAppPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["DownloadedFiles"].ToString();
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sAppPath);
                    if (!dir.Exists)
                        dir.Create();
                    PathOfFile = sAppPath;
                    FilePath = sAppPath + FileNamess;

                    using (var fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                    {
                        stream.GetAwaiter().GetResult().CopyTo(fileStream);
                    }
                }
            }
            return PathOfFile;
        }

        public void SubscribeToGroupService(UserInfo userInfo, string GroupUsers, bool IsNewGroup)
        {
            //SbscribeUserToGroup
            HttpResponseMessage result;
            using (HttpClient client = new HttpClient())
            {
                result = new HttpResponseMessage();

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(JsonConvert.SerializeObject(userInfo)), "UserInfo");

                    formData.Add(new StringContent(JsonConvert.SerializeObject(GroupUsers)), "GroupUsers");
                    formData.Add(new StringContent(JsonConvert.SerializeObject(IsNewGroup)), "IsNewGroup");

                    var requestUri = ConfigurationManager.AppSettings["PowerDriveWebAPIURL"] + "api/CreateGroupAndInsertUsers";
                    result = client.PostAsync(requestUri, formData).GetAwaiter().GetResult();
                }
            }
        }
        public async Task<Stream> PreviewFile(string Path, UserInfo User)
        {

            HttpResponseMessage result;
            HttpResponseMessage hpm = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                result = new HttpResponseMessage();

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(JsonConvert.SerializeObject(Path)), "Path");
                    formData.Add(new StringContent(JsonConvert.SerializeObject(User)), "User");

                    var requestUri = ConfigurationManager.AppSettings["WebAPIURL"] + "api/DownLoadFile";

                    result = client.PostAsync(requestUri, formData).Result;
                    var stream = await result.Content.ReadAsStreamAsync();
                    return stream;

                }
            }
        }




     
    }

    
}