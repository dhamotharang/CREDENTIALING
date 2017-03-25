using AHC.CD.Data.Repository.MasterDataRepo;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository.MasterDataEFRepo
{
   internal class NotesTemplateEFRepository:EFGenericRepository<NotesTemplate>,INotesTemplateRepository
    {

       public List<NotesTemplate> GetNotesTemplateByCode(string Code)
       {
           return (from temp in DbSet where temp.Code.Contains(Code) select temp).ToList();
       }
       public List<NotesTemplate> GetAllNotesTemplates()
       {
           return DbSet.ToList();
       }

    }
}
