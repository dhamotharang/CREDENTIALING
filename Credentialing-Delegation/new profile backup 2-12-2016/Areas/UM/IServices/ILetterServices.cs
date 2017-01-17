using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalTemplate.Areas.UM.Models.ViewModels.Letter;

namespace PortalTemplate.Areas.UM.IServices
{
    interface ILetterServices
    {
        List<LetterViewModel> GetAllLetters(int AuthorizationID);
        List<LetterViewModel> GetAllLettersByBatchNo(string BatchNumber);
        string EditServiceRequested(int LetterID);
        void DeleteLetter(LetterViewModel model);
        ApprovalLetterViewModel PreviewLetter(int LetterID, string MemberID, string LetterTemplateName);
        PortalTemplate.Areas.UM.Models.ViewModels.Letter.LetterViewModel SaveLetter(LetterViewModel model);
    }
}
