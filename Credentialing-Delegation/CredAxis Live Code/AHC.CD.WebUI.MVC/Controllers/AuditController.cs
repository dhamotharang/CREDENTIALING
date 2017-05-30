using AHC.ActivityLogging;
using AHC.CD.Business.AuditLogManager;
using AHC.CD.Entities.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class AuditController : Controller
    {
        //
        // GET: /Audit/

        private readonly IAuditManager iAuditManager;
        private readonly IAuditManagerADO iAuditManagerADO;
        public AuditController(IAuditManager iAuditManager, IAuditManagerADO iAuditManagerADO)
        {
            this.iAuditManager = iAuditManager;
            this.iAuditManagerADO = iAuditManagerADO;
        }
        public ActionResult Index()
        {
            ViewBag.AuditLog = iAuditManager.GetAllAuditLogAsync();
            return View();
        }

        public async Task<ActionResult> Home()
        {
            AduitDTO auditMessages = new AduitDTO();
            try
            {
                auditMessages = await iAuditManagerADO.GetAllAuditLog(ConstructSearchObjectForProviderQueue());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(auditMessages);
        }

        [HttpPost]
        public async Task<ActionResult> GetAllAuditLOG(int index, int CountOfList, string sortingType, string sortBy, AuditLogCategory AuditDataWithCatagory, AuditMessageDTO auditMessage)
        {
            AduitDTO auditMessages = new AduitDTO();
            try
            {
                auditMessages = await iAuditManagerADO.GetAllAuditLog(ConstructSearchObjectForProviderQueue(AuditDataWithCatagory, auditMessage, index, CountOfList, sortingType, sortBy));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("~/Views/Audit/_AuditTableBody.cshtml", auditMessages.AuditMessages);
        }

        [HttpPost]
        public async Task<ActionResult> GetAllAuditBasedOnCategory(string AuditCategory)
        {
            AduitDTO auditMessages = new AduitDTO();
            try
            {
                AuditLogCategory auditLogCategory = new AuditLogCategory();
                auditLogCategory.AuditCategory = AuditCategory;
                auditMessages = await iAuditManagerADO.GetAllAuditLog(ConstructSearchObjectForProviderQueue(auditLogCategory));
                ViewBag.TotalLogCount = auditMessages.TotalLogCount;
                ViewBag.InformationCount = auditMessages.InformationLogCount;
                ViewBag.AlertCount = auditMessages.AlertLogCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("~/Views/Audit/_AuditTableBody.cshtml", auditMessages.AuditMessages);
        }

        #region Private Methods



        private AuditSearchDTO ConstructSearchObjectForProviderQueue(AuditLogCategory auditCategory = null, AuditMessageDTO auditMessage = null, int index = 0, int CountOfList = 50, string sortingType = "desc", string sortBy = "DateTime")
        {
            AuditSearchDTO auditSearchDTOTemp = new AuditSearchDTO();
            if (auditCategory != null)
            {
                auditSearchDTOTemp.AuditCategory = auditCategory.AuditCategory;
            }
            else
            {
                auditSearchDTOTemp.AuditCategory = "Total";
            }
            auditSearchDTOTemp.Pagination = new AuditPaginationDTO();
            auditSearchDTOTemp.Pagination.pageNumber = CountOfList;
            auditSearchDTOTemp.Pagination.pageOffset = index;
            auditSearchDTOTemp.Sorting = new AuditSortingDTO();
            auditSearchDTOTemp.Sorting.columnName = sortBy ?? "asc";
            auditSearchDTOTemp.Sorting.sortingOrder = sortingType ?? "DateTime";
            auditSearchDTOTemp.FilterByFields = ConstructFilterObject(auditMessage);
            return auditSearchDTOTemp;
        }

        private List<AuditFilterDTO> ConstructFilterObject(AuditMessageDTO auditMessage)
        {
            List<AuditFilterDTO> AuditFilterDTO = new List<AuditFilterDTO>();
            if (auditMessage != null)
            {
                foreach (var prop in auditMessage.GetType().GetProperties())
                {
                    if (prop.CanRead && prop.GetValue(auditMessage) != null)
                    {
                        AuditFilterDTO temp = new AuditFilterDTO();
                        temp.FieldName = prop.Name;
                        temp.FieldValue = prop.GetValue(auditMessage).ToString();
                        AuditFilterDTO.Add(temp);
                    }
                }
            }
            return AuditFilterDTO;
        }

        #endregion
    }
}