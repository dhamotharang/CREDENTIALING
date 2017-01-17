using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document
        public ActionResult View(string path)
        {

            string contentType = "";
            var filePath = Server.MapPath(path);
            switch (System.IO.Path.GetExtension(filePath).ToLower())
            {
                case ".htm":
                case ".html":
                    contentType = "text/HTML";
                    break;

                case ".txt":
                    contentType = "text/plain";
                    break;

                case ".doc":
                case ".rtf":
                case ".docx":
                    contentType = "Application/msword";
                    break;

                case ".xls":
                case ".xlsx":
                    contentType = "Application/x-msexcel";
                    break;

                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;

                case ".gif":
                    contentType = "image/GIF";
                    break;

                case ".png":
                    contentType = "image/png";
                    break;

                case ".bmp":
                    contentType = "image/x-ms-bmp";
                    break;

                case ".pdf":
                    contentType = "application/pdf";
                    break;
            }


            return File(filePath, contentType);
        }
    }
}