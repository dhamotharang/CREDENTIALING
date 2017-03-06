using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository.MasterDataRepo
{
   public  interface INotesTemplateRepository
    {
       List<NotesTemplate> GetNotesTemplateByCode(string Code);

       List<NotesTemplate> GetAllNotesTemplates();
    }
}
