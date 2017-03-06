﻿using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account
{
    public class Group
    {
        public Group()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int GroupID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string TaxId { get; set; }

        public string NPINumber { get; set; }

        #region Status

        public string Status { get; set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
                    return null;
                
                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}