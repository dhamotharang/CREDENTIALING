using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.UtilityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    internal class ProfileDataDuplicateManager : IProfileDataDuplicateManager
    {
        private IProfileRepository ProfileRepository { get; set; }
        private IGenericRepository<PhoneDetail> PhoneDetailRepository { get; set; }
        private IGenericRepository<EmailDetail> EmailDetailRepository { get; set; }

        public ProfileDataDuplicateManager(IUnitOfWork uow)
        {
            this.ProfileRepository = uow.GetProfileRepository();
            this.PhoneDetailRepository = uow.GetGenericRepository<PhoneDetail>();
            this.EmailDetailRepository = uow.GetGenericRepository<EmailDetail>();
        }

        public bool IsEmailAddressDoesNotExists(string emailAddress, int emailDetailID = 0)
        {
            switch (emailDetailID)
            {
                case 0:
                    if (EmailDetailRepository.Any(p => p.EmailAddress.Equals(emailAddress)))
                        return false;

                    break;
                default:
                    if (emailAddress != null && EmailDetailRepository.Any(p => p.EmailDetailID != emailDetailID && p.EmailAddress.Equals(emailAddress)))
                        return false;

                    break;
            }

            return true;
        }

        public bool IsContactNumberDoesNotExists(string phoneNumber, int phoneDetailID)
        {
            switch (phoneDetailID)
            {
                case 0:
                    if (PhoneDetailRepository.Any(p => p.PhoneNumber.Equals(phoneNumber)))
                        return false;

                    break;
                default:
                    if (phoneNumber != null && PhoneDetailRepository.Any(p => p.PhoneDetailID != phoneDetailID && p.PhoneNumber.Equals(phoneNumber)))
                        return false;

                    break;
            }

            return true;
        }

        public bool IsNPINumberDoesNotExists(string number, int profileId = 0)
        {
            switch (profileId)
            {
                case 0:
                    if (number != null && ProfileRepository.Any(p => p.OtherIdentificationNumber.NPINumber.Equals(number)))
                        return false;

                    break;
                default:
                    if (number != null && ProfileRepository.Any(p => p.ProfileID != profileId && p.OtherIdentificationNumber.NPINumber.Equals(number)))
                        return false;

                    break;
            }

            return true;
        }

        public bool IsNPIUsernameDoesNotExists(string number, int profileId = 0)
        {
            switch (profileId)
            {
                case 0:
                    if (number != null && ProfileRepository.Any(p => p.OtherIdentificationNumber.NPIUserName.Equals(number)))
                        return false;

                    break;
                default:
                    if (number != null && ProfileRepository.Any(p => p.ProfileID != profileId && p.OtherIdentificationNumber.NPIUserName.Equals(number)))
                        return false;

                    break;
            }

            return true;
        }

        public bool IsCAQHNumberDoesNotExists(string number, int profileId = 0)
        {
            switch (profileId)
            {
                case 0:
                    if (number != null && ProfileRepository.Any(p => p.OtherIdentificationNumber.CAQHNumber.Equals(number)))
                        return false;

                    break;
                default:
                    if (number != null && ProfileRepository.Any(p => p.ProfileID != profileId && p.OtherIdentificationNumber.CAQHNumber.Equals(number)))
                        return false;

                    break;
            }

            return true;
        }

        public bool IsCAQHUsernameDoesNotExists(string number, int profileId = 0)
        {
            switch (profileId)
            {
                case 0:
                    if (number != null && ProfileRepository.Any(p => p.OtherIdentificationNumber.CAQHUserName.Equals(number)))
                        return false;

                    break;
                default:
                    if (number != null && ProfileRepository.Any(p => p.ProfileID != profileId && p.OtherIdentificationNumber.CAQHUserName.Equals(number)))
                        return false;

                    break;
            }

            return true;
        }

        public bool IsIndividualTaxIDDoesNotExists(string IndividualTaxId, int contractInfoID = 0)
        {
            switch (contractInfoID)
            {
                case 0:
                    if (!String.IsNullOrWhiteSpace(IndividualTaxId) && ProfileRepository.Any(p => p.ContractInfoes.Any(c => c.IndividualTaxId.Equals(IndividualTaxId))))
                        return false;

                    break;
                default:
                    if (!String.IsNullOrWhiteSpace(IndividualTaxId) && ProfileRepository.Any(p => p.ContractInfoes.Any(c => c.ContractInfoID != contractInfoID && c.IndividualTaxId.Equals(IndividualTaxId))))
                        return false;

                    break;
            }

            return true;
        }
        
        
        public bool IsDLNumberDoesNotExists(string licenseNumber, int profileId = 0)
        {
            switch (profileId)
            {
                case 0:
                    if (licenseNumber != null && ProfileRepository.Any(p => p.PersonalIdentification.DL.Equals(licenseNumber)))
                        return false;

                    break;
                default:
                    if (licenseNumber != null && ProfileRepository.Any(p => p.ProfileID != profileId && p.PersonalIdentification.DL.Equals(licenseNumber)))
                        return false;

                    break;
            }

            return true;
        }

        public bool IsSSNumberDoesNotExists(string ssNumber, int profileId = 0)
        {
            if (ssNumber != null)
            {
                ssNumber = EncryptorDecryptor.Encrypt(ssNumber);

                switch (profileId)
                {
                    case 0:
                        if (ProfileRepository.Any(p => p.PersonalIdentification.SocialSecurityNumber.Equals(ssNumber)))
                            return false;

                        break;
                    default:
                        if (ProfileRepository.Any(p => p.ProfileID != profileId && p.PersonalIdentification.SocialSecurityNumber.Equals(ssNumber)))
                            return false;

                        break;
                }
            }

            return true;
        }

        public bool IsVisaNumberDoesNotExists(string number, int profileId = 0)
        {
            if (number != null)
            {
                number = EncryptorDecryptor.Encrypt(number);

                switch (profileId)
                {
                    case 0:
                        if (ProfileRepository.Any(p => p.VisaDetail.VisaInfo.VisaNumberStored.Equals(number)))
                            return false;

                        break;
                    default:
                        if (ProfileRepository.Any(p => p.ProfileID != profileId && p.VisaDetail.VisaInfo.VisaNumberStored.Equals(number)))
                            return false;

                        break;
                }
            }

            return true;
        }

        public bool IsGreenCardNumberDoesNotExists(string number, int profileId = 0)
        {
            if (number != null)
            {
                number = EncryptorDecryptor.Encrypt(number);

                switch (profileId)
                {
                    case 0:
                        if (ProfileRepository.Any(p => p.VisaDetail.VisaInfo.GreenCardNumberStored.Equals(number)))
                            return false;

                        break;
                    default:
                        if (ProfileRepository.Any(p => p.ProfileID != profileId && p.VisaDetail.VisaInfo.GreenCardNumberStored.Equals(number)))
                            return false;

                        break;
                }
            }

            return true;
        }

        public bool IsNationalIDNumberDoesNotExists(string number, int profileId = 0)
        {
            if (number != null)
            {
                number = EncryptorDecryptor.Encrypt(number);

                switch (profileId)
                {
                    case 0:
                        if (ProfileRepository.Any(p => p.VisaDetail.VisaInfo.NationalIDNumberNumberStored.Equals(number)))
                            return false;

                        break;
                    default:
                        if (ProfileRepository.Any(p => p.ProfileID != profileId && p.VisaDetail.VisaInfo.NationalIDNumberNumberStored.Equals(number)))
                            return false;

                        break;
                }
            }

            return true;
        }

        public bool IsStateLicenseNumberDoesNotExists(string number, int stateLicenseInformationId = 0)
        {
            if (number != null)
            {
                switch (stateLicenseInformationId)
                {
                    case 0:
                        if (ProfileRepository.Any(p => p.StateLicenses.Any(s => s.LicenseNumber.Equals(number))))
                            return false;

                        break;
                    default:
                        if (ProfileRepository.Any(p => p.StateLicenses.Any(s => s.StateLicenseInformationID != stateLicenseInformationId && s.LicenseNumber.Equals(number))))
                            return false;

                        break;
                }
            }

            return true;
        }

        public bool IsFederalDEANumberDoesNotExists(string number, int federalDEAInformationId = 0)
        {
            if (number != null)
            {
                switch (federalDEAInformationId)
                {
                    case 0:
                        if (ProfileRepository.Any(p => p.FederalDEAInformations.Any(s => s.DEANumber.Equals(number))))
                            return false;

                        break;
                    default:
                        if (ProfileRepository.Any(p => p.FederalDEAInformations.Any(s => s.FederalDEAInformationID != federalDEAInformationId && s.DEANumber.Equals(number))))
                            return false;

                        break;
                }
            }

            return true;
        }

        public bool IsCDSCLicenseNumberDoesNotExists(string number, int cdscInformationId = 0)
        {
            if (number != null)
            {
                switch (cdscInformationId)
                {
                    case 0:
                        if (ProfileRepository.Any(p => p.CDSCInformations.Any(s => s.CertNumber.Equals(number))))
                            return false;

                        break;
                    default:
                        if (ProfileRepository.Any(p => p.CDSCInformations.Any(s => s.CDSCInformationID != cdscInformationId && s.CertNumber.Equals(number))))
                            return false;

                        break;
                }
            }

            return true;
        }

        public bool IsMedicareLicenseNumberDoesNotExists(string number, int medicareInformationId = 0)
        {
            if (number != null)
            {
                switch (medicareInformationId)
                {
                    case 0:
                        if (ProfileRepository.Any(p => p.MedicareInformations.Any(s => s.LicenseNumber.Equals(number))))
                            return false;

                        break;
                    default:
                        if (ProfileRepository.Any(p => p.MedicareInformations.Any(s => s.MedicareInformationID != medicareInformationId && s.LicenseNumber.Equals(number))))
                            return false;

                        break;
                }
            }

            return true;
        }

        public bool IsMedicaidLicenseNumberDoesNotExists(string number, int medicaidInformationId = 0)
        {
            if (number != null)
            {
                switch (medicaidInformationId)
                {
                    case 0:
                        if (ProfileRepository.Any(p => p.MedicaidInformations.Any(s => s.LicenseNumber.Equals(number))))
                            return false;

                        break;
                    default:
                        if (ProfileRepository.Any(p => p.MedicaidInformations.Any(s => s.MedicaidInformationID != medicaidInformationId && s.LicenseNumber.Equals(number))))
                            return false;

                        break;
                }
            }

            return true;
        }

        public bool IsUPINNumberDoesNotExists(string number, int profileId = 0)
        {
            switch (profileId)
            {
                case 0:
                    if (number != null && ProfileRepository.Any(p => p.OtherIdentificationNumber.UPINNumber.Equals(number)))
                        return false;

                    break;
                default:
                    if (number != null && ProfileRepository.Any(p => p.ProfileID != profileId && p.OtherIdentificationNumber.UPINNumber.Equals(number)))
                        return false;

                    break;
            }

            return true;
        }

        public bool IsUSMLENumberDoesNotExists(string number, int profileId = 0)
        {
            switch (profileId)
            {
                case 0:
                    if (number != null && ProfileRepository.Any(p => p.OtherIdentificationNumber.USMLENumber.Equals(number)))
                        return false;

                    break;
                default:
                    if (number != null && ProfileRepository.Any(p => p.ProfileID != profileId && p.OtherIdentificationNumber.USMLENumber.Equals(number)))
                        return false;

                    break;
            }

            return true;
        }


        
    }
}
