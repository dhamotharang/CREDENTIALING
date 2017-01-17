using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.CredentialingRequest
{
    public class CredentialingRequest
    {
        public CredentialingRequest()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingRequestID { get; set; }

        public int ProfileID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NPINumber { get; set; }

        public string CAQHNumber { get; set; }

        public int PlanID { get; set; }

        #region DelegatedType

        public string DelegatedType { get; private set; }

        [NotMapped]
        public IsDelegated? IsDelegated
        {
            get
            {
                if (String.IsNullOrEmpty(this.DelegatedType))
                    return null;

                if (this.DelegatedType.Equals("Not Available"))
                    return null;

                return (IsDelegated)Enum.Parse(typeof(IsDelegated), this.DelegatedType);
            }
            set
            {
                this.DelegatedType = value.ToString();
            }
        }

        #endregion

        #region Status

        public string Status { get; private set; }

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
