using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Tables
{
   public  class NotesTemplate
    {
       public NotesTemplate()
       {
           this.LastModifiedDate = DateTime.Now;
       }
        
       public int  NotesTemplateID{ get; set; }

       public string Code { get; set; }

       public string Description { get; set; }

       public DateTime CreatedDate { get; set; }

       public DateTime LastModifiedDate { get; set; }

       public string CreatedBy { get; set; }

       public string Status { get; set; }

       [NotMapped]
       public StatusType? StatusType
       {
           get
           {
               if (String.IsNullOrEmpty(this.Status))
                   return null;

               return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
           }
           set
           {
               this.Status = value.ToString();
           }
       }


    }
}
