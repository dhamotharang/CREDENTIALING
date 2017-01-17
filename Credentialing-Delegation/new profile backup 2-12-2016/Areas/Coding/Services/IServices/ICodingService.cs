using PortalTemplate.Areas.Coding.DTO;
using PortalTemplate.Areas.Coding.Models.CodingList;
using PortalTemplate.Areas.Coding.Models.CPTCodes;
using PortalTemplate.Areas.Coding.Models.CreateCoding;
using PortalTemplate.Areas.Coding.Models.DashBoard;
using PortalTemplate.Areas.Coding.Models.ICDCodes;
using PortalTemplate.Areas.Coding.Models.ICDCPTMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Coding.Services.IServices
{
    public interface ICodingService
    {
        CodingDashBoardViewModel GetCodingDashBoardDetails();
        Task<List<CodingListViewModel>> GetCodingListByStatus(string status);
        CodingListCountsViewModel GetCodingListStatusCount();
        DeactivateEncounter GetDeactivatemodalData();
        Boolean DeactivateEncounter();
        CreateCodingViewModel ViewDetails();
        CreateCodingViewModel EditDetails();
        Task<bool> SaveCoding(SaveCreateCodingDTO SaveCoding);
        CreateCodingViewModel Create();
        ICDCodeHistoryDetailsViewModel GetICDCodeHistory();
        List<ICDCodeViewModel> GetIcdHistoryData();
        List<CPTCodeViewModel> GetCPTCodeHistory();
        List<ICDCPTCodemappingViewModel> GetCptHistoryData();
        List<CategoryViewModel> GetAllCategories();

        void GetActiveDiagnosisBySearch(object SearchObject);
        void GetActiveProceduresBySearch(object SearchObject);
        void GetEncountersReportAsPDF(object PDFOptions);
        void GetEncountersReportAsExcel(object ExcelOptions);
        void GetReadyToCodeEncountersOnSearch(object SerachObject);
        void DeActivateCodedEncounter(int CodedEncounterId);
        void UploadDocument(object FileDetails);
        void GetFeeSchedules();
        void GetHCCCodes();
        void GetICDCodesOnSearch();
        void GetCPTCodesOnSearch();
    }
}
