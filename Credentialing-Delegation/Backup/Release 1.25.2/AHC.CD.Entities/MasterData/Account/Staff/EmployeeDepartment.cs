using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account.Staff
{
    public class EmployeeDepartment
    {
        public EmployeeDepartment()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EmployeeDepartmentID { get; set; }

        public int DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }

        #region IsPrimary

        public string IsPrimary { get; private set; }

        [NotMapped]
        public YesNoOption? PrimaryYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsPrimary))
                    return null;

                if (this.IsPrimary.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsPrimary);
            }
            set
            {
                this.IsPrimary = value.ToString();
            }
        }

        #endregion

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
