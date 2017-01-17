using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository
{
    public class MasterDataDBInitializer
    {
        public void Seed()
        {
            try
            {
                EFEntityContext context = new EFEntityContext();

                #region Admitting Privilege

                //Admitting Privileges Master Data
                context.AdmittingPrivileges.AddRange(new List<AdmittingPrivilege>()
                {
                    new AdmittingPrivilege() { Title = "None", StatusType = StatusType.Active },
                    new AdmittingPrivilege() { Title = "Provisional", StatusType = StatusType.Active },
                    new AdmittingPrivilege() { Title = "Full Unrestricted", StatusType = StatusType.Active },
                    new AdmittingPrivilege() { Title = "Temporary", StatusType = StatusType.Active }
                });

                #endregion

                #region Certification

                // Certification Master Data Info
                context.Certifications.AddRange(new List<Certification>()
                {
                    new Certification() { Name = "Certified HIPAA Privacy Associate", Code = "CHPA", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified HIPAA Privacy Expert", Code = "CHPE", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified HIPAA Privacy Security Expert", Code = "CHPSE", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified HIPAA Professional", Code = "CHP", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified HIPAA Security Expert", Code = "CHSE", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified HIPAA Security Specialist", Code = "CHSS", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Veterinary Assistant", Code = "CVA", StatusType = StatusType.Active },
                    new Certification() { Name = "Doctor of Veterinary Medicine", Code = "DVM", StatusType = StatusType.Active },
                    new Certification() { Name = "Doctor of Chiropractic", Code = "DC", StatusType = StatusType.Active },
                    new Certification() { Name = "Doctor of Dental Surgery", Code = "DDS", StatusType = StatusType.Active },
                    new Certification() { Name = "Doctor of Medicine", Code = "MD", StatusType = StatusType.Active },
                    new Certification() { Name = "Doctor of Nursing Practice", Code = "DNP", StatusType = StatusType.Active },
                    new Certification() { Name = "Doctor of Osteopathic Medicine", Code = "DO", StatusType = StatusType.Active },
                    new Certification() { Name = "Emergency Medical Dispatcher", Code = "EMD", StatusType = StatusType.Active },
                    new Certification() { Name = "Nationally Registered Emergency Medical Responder", Code = "NREMR", StatusType = StatusType.Active },
                    new Certification() { Name = "Emergency Medical Technician - Basic", Code = "EMT-B", StatusType = StatusType.Active },
                    new Certification() { Name = "Nationally Registered Emergency Medical Technician", Code = "NREMT", StatusType = StatusType.Active },
                    new Certification() { Name = "Emergency Medical Technician - Intermediate/85", Code = "EMT-I/85", StatusType = StatusType.Active },
                    new Certification() { Name = "Emergency Medical Technician - Intermediate/99", Code = "EMT-I/99", StatusType = StatusType.Active },
                    new Certification() { Name = "Nationally Registered Advanced Emergency Medical Technician", Code = "NRAEMT", StatusType = StatusType.Active },
                    new Certification() { Name = "Emergency Medical Technician - Paramedic", Code = "EMT-P", StatusType = StatusType.Active },
                    new Certification() { Name = "Nationally Registered Paramedic", Code = "NRP", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Paramedic", Code = "LP", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Practical Nurse", Code = "LPN", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Vocational Nurse", Code = "LVN", StatusType = StatusType.Active },
                    new Certification() { Name = "Registered Nurse", Code = "RN", StatusType = StatusType.Active },
                    new Certification() { Name = "Advanced Practice Nurse", Code = "APN", StatusType = StatusType.Active },
                    new Certification() { Name = "Advanced Practice Registered Nurse", Code = "APRN", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Massage Therapist", Code = "CMT", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Massage Therapist", Code = "LMT", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Clinical Massage Therapist", Code = "LCMT", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Midwife", Code = "LM", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Midwife", Code = "CM", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Professional Midwife", Code = "CPM", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Nurse-Midwife", Code = "CNM", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Registered Nurse Anaesthetist", Code = "CRNA", StatusType = StatusType.Active },
                    new Certification() { Name = "Nurse Practitioner", Code = "NP", StatusType = StatusType.Active },
                    new Certification() { Name = "Physician Assistant", Code = "PA", StatusType = StatusType.Active },
                    new Certification() { Name = "Registered Veterinary Technician", Code = "RVT", StatusType = StatusType.Active },
                    new Certification() { Name = "Physical Therapist", Code = "PT", StatusType = StatusType.Active },
                    new Certification() { Name = "Physical Therapy Assistant", Code = "PTA", StatusType = StatusType.Active },
                    new Certification() { Name = "Medical Laboratory Scientist", Code = "MLS", StatusType = StatusType.Active },
                    new Certification() { Name = "Medical Technologist", Code = "MT", StatusType = StatusType.Active },
                    new Certification() { Name = "Medical Laboratory Technician", Code = "MLT", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Professional Counsellor", Code = "LPC", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Associate Counsellor", Code = "LAC", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Master Social Worker", Code = "LMSW", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Clinical Social Worker", Code = "LCSW", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Veterinary Technician", Code = "LVT", StatusType = StatusType.Active },
                    new Certification() { Name = "Qualified Clinical Social Worker", Code = "QCSW", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Tissue Bank Specialist", Code = "CTBS", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Hypnotherapist", Code = "CHT", StatusType = StatusType.Active },
                    new Certification() { Name = "National Board Certified Clinical Hypnotherapist", Code = "NBCCH", StatusType = StatusType.Active },
                    new Certification() { Name = "National Board Certified Clinical Hypnotherapist in Public Service", Code = "NBCCH-PS", StatusType = StatusType.Active },
                    new Certification() { Name = "National Board Certified Diplomate in Clinical Hypnotherapy", Code = "NBCDCH", StatusType = StatusType.Active },
                    new Certification() { Name = "National Board Certified Diplomate in Clinical Hypnotherapy in Public Service", Code = "NBCDCH-PS", StatusType = StatusType.Active },
                    new Certification() { Name = "National Board Certified Fellow in Clinical Hypnotherapy", Code = "NBCFCH", StatusType = StatusType.Active },
                    new Certification() { Name = "National Board Certified Fellow in Clinical Hypnotherapy in Public Service", Code = "NBCFCH-PS", StatusType = StatusType.Active },
                    new Certification() { Name = "Licensed Acupuncturist", Code = "L.Ac", StatusType = StatusType.Active },
                    new Certification() { Name = "Diplomate in Acupuncture", Code = "Dipl.Ac", StatusType = StatusType.Active },
                    new Certification() { Name = "Diplomate in Oriental Medicine", Code = "Dipl.O.M", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American Society for Parenteral and Enteral Nutrition", Code = "FASPEN", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American Academy of Nursing", Code = "FAAN", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American Society of Health-System Pharmacists", Code = "FASHP", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American Academy of Emergency Medicine", Code = "FAAEM", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American Academy of Pediatrics", Code = "FAAP", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American College of Cardiology", Code = "FACC", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American College of Dentists", Code = "FACD", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American College of Endocrinology", Code = "FACE", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American College of Physicians", Code = "FACP", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American College of Surgeons", Code = "FACS", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American College of Osteopathic Family Physician", Code = "FACOFP", StatusType = StatusType.Active },
                    new Certification() { Name = "Fellow of the American Congress of Obstetricians and Gynaecologists", Code = "FACOG", StatusType = StatusType.Active },
                    new Certification() { Name = "Doctor of Pharmacy", Code = "PharmD", StatusType = StatusType.Active },
                    new Certification() { Name = "Registered Pharmacist", Code = "RPh", StatusType = StatusType.Active },
                    new Certification() { Name = "Registered Respiratory Therapist", Code = "RRT", StatusType = StatusType.Active },
                    new Certification() { Name = "Registered Respiratory Therapist, Neonatal & Pediatric Specialist", Code = "RRT-NPS", StatusType = StatusType.Active },
                    new Certification() { Name = "Registered Respiratory Therapist, Sleep Disorder Specialist", Code = "RRT-SDS", StatusType = StatusType.Active },
                    new Certification() { Name = "Registered Respiratory Therapist, Adult Critical Care Specialist", Code = "RRT-ACCS", StatusType = StatusType.Active },
                    new Certification() { Name = "Respiratory practitioner", Code = "RP", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Respiratory Therapy Technician", Code = "CRTT", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Respiratory Therapist", Code = "CRT", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Phlebotomy Technician", Code = "CPT", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Athletic trainer", Code = "ATC", StatusType = StatusType.Active },
                    new Certification() { Name = "Certified Medical Assistant", Code = "CMA", StatusType = StatusType.Active }
                });

                #endregion

                #region DEA Schedule

                context.DEASchedules.AddRange(new List<DEASchedule>()
                {
                    new DEASchedule() { ScheduleTitle = "Schedule I", ScheduleTypeTitle = "Non-Narcotic", StatusType = StatusType.Active },
                    new DEASchedule() { ScheduleTitle = "Schedule II", ScheduleTypeTitle = "Narcotic", StatusType = StatusType.Active },
                    new DEASchedule() { ScheduleTitle = "Schedule II", ScheduleTypeTitle = "Non-Narcotic", StatusType = StatusType.Active },
                    new DEASchedule() { ScheduleTitle = "Schedule III", ScheduleTypeTitle = "Narcotic", StatusType = StatusType.Active },
                    new DEASchedule() { ScheduleTitle = "Schedule III", ScheduleTypeTitle = "Non-Narcotic", StatusType = StatusType.Active },
                    new DEASchedule() { ScheduleTitle = "Schedule IV", ScheduleTypeTitle = "Non-Narcotic", StatusType = StatusType.Active },
                    new DEASchedule() { ScheduleTitle = "Schedule V", ScheduleTypeTitle = "Non-Narcotic", StatusType = StatusType.Active }
                });

                #endregion

                #region Hospital

                context.Hospitals.AddRange(new List<Hospital>()
                {
                    new Hospital() { HospitalName = "A.G. Holley State Hospital", StatusType = StatusType.Active, HospitalContactInfoes = new List<HospitalContactInfo>()
                                        {
                                            new HospitalContactInfo() { Street = "Lantana Rd", LocationName = "Lantana Rd", City = "Lantana", State = "Florida", County = "Lantana", 
                                                                        Country = "US", Phone = "+1-5615825666", StatusType = StatusType.Active, Email = "", UnitNumber = "123", ZipCode = "99999",
                                                                        HospitalContactPersons = new List<HospitalContactPerson>()
                                                {
                                                    new HospitalContactPerson() { ContactPersonName = "Henry Williams", ContactPersonPhone = "+1-850-263-4431", ContactPersonFax = "+1-408-999 8888", StatusType = StatusType.Active }
                                                }
                                            }
                                        }
                                    }
                });

                #endregion

                #region Insurance Carrier

                context.InsuranceCarriers.AddRange(new List<InsuranceCarrier>()
                {
                    new InsuranceCarrier() { Name = "AXA INSURANCE COMPANY", StatusType = StatusType.Active, InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "5th Floor", Street = "125 Broad Street", LocationName = "New York", City = "New York", State = "New York", County = "", Country = "US", StatusType = StatusType.Active, ZipCode = "10004" } }},
                    new InsuranceCarrier() { Name = "BALBOA INSURANCE COMPANY", StatusType = StatusType.Active, InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "# 300", Street = "3349 Michelson Drive ", LocationName = "Irvine - California", City = "Irvine", State = "California", County = "Orange", Country = "US", StatusType = StatusType.Active, ZipCode = "92612" } } },
                    new InsuranceCarrier() { Name = "BAPTIST LIFE ASSOCIATION", StatusType = StatusType.Active, InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "Not Available", Street = "8555 Main St", LocationName = "St.Williamsville - New York", City = "Williamsville", State = "New York", County = "Erie", Country = "US", StatusType = StatusType.Active, ZipCode = "14221" } } },
                    new InsuranceCarrier() { Name = "BCS INSURANCE COMPANY", StatusType = StatusType.Active,  InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "Not Available", Street = "2 Mid America Plaza", LocationName = "Oakbrook - Illinois", City = "Oakbrook", State = "Illinois", County = "DuPage", Country = "US", StatusType = StatusType.Active, ZipCode = "60181" } } },
                    new InsuranceCarrier() { Name = "BOSTON MUTUAL LIFE INSURANCE COMPANY", StatusType = StatusType.Active, InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "Not Available", Street = "120 Royall St", LocationName = "Royall St.Canton - Massachusetts", City = "Not Available", State = "Massachusetts", County = "Berkshire", Country = "US", StatusType = StatusType.Active, ZipCode = "02021" } } },
                    new InsuranceCarrier() { Name = "CALIFORNIA INSURANCE COMPANY", StatusType = StatusType.Active,  InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "14th Floor", Street = "950 Tower Lane", LocationName = "Foster city - California", City = "Foster city", State = "California", County = "San Mateo", Country = "US", StatusType = StatusType.Active, ZipCode = "94404" } } },
                    new InsuranceCarrier() { Name = "CANADA LIFE ASSURANCE COMPANY", StatusType = StatusType.Active,  InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "Not Available", Street = "8515 E. Orchard Road", LocationName = "Greenwood - Colorado", City = "Greenwood", State = "Colorado", County = "Arapahoe", Country = "US", StatusType = StatusType.Active, ZipCode = "80111" } } },
                    new InsuranceCarrier() { Name = "CAPITOL INDEMNITY CORPORATION", StatusType = StatusType.Active,  InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "# 400", Street = "1600 Aspen Cmns # 400, ", LocationName = "Middleton - Wisconsin", City = "Middleton", State = "Wisconsin", County = "Dane", Country = "US", StatusType = StatusType.Active, ZipCode = "53562" } } },
                    new InsuranceCarrier() { Name = "CITIZENS UNITED RECIPROCAL EXCHANGE", StatusType = StatusType.Active,  InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "Suite 101", Street = "214 Carnegie Center", LocationName = "Princeton-New Jersey", City = "Princeton", State = "New Jersey", County = "Mercer", Country = "US", StatusType = StatusType.Active, ZipCode = "08540" } } },
                    new InsuranceCarrier() { Name = "FACTORY MUTUAL INSURANCE COMPANY", StatusType = StatusType.Active,  InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "Not Available", Street = "270 Central Avenue", LocationName = "Johnston - Rhode Island", City = "Johnston", State = "Rhode Island", County = "", Country = "US", StatusType = StatusType.Active, ZipCode = "02919" } } },
                });

                context.InsuranceCarriers.Add(new InsuranceCarrier()
                {
                    Name = "SAMPLE INSURANCE COMPANY",
                    StatusType = StatusType.Active,
                    InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>() { new InsuranceCarrierAddress() { Building = "122357", Street = "Lantana Rd", LocationName = "Lantana Rd", City = "Lantana", State = "Florida", County = "Lantana", Country = "US", StatusType = StatusType.Active, ZipCode = "99999" } }
                });

                #endregion

                #region Military Branch

                context.MilitaryBranches.AddRange(new List<MilitaryBranch>()
                {
                    new MilitaryBranch() { Title = "Army", StatusType = StatusType.Active },
                    new MilitaryBranch() { Title = "Marine", StatusType = StatusType.Active },
                    new MilitaryBranch() { Title = "Air Force", StatusType = StatusType.Active },
                    new MilitaryBranch() { Title = "Navy", StatusType = StatusType.Active },
                    new MilitaryBranch() { Title = "Coast Guard", StatusType = StatusType.Active }
                });

                #endregion

                #region Military Discharge

                context.MilitaryDischarges.AddRange(new List<MilitaryDischarge>()
                {
                    new MilitaryDischarge() { Title = "Retired", StatusType = StatusType.Active },
                    new MilitaryDischarge() { Title = "General", StatusType = StatusType.Active },
                    new MilitaryDischarge() { Title = "Honourable", StatusType = StatusType.Active },
                    new MilitaryDischarge() { Title = "Bad Conduct", StatusType = StatusType.Active },
                    new MilitaryDischarge() { Title = "Officer", StatusType = StatusType.Active },
                    new MilitaryDischarge() { Title = "Entry Level Separation", StatusType = StatusType.Active },
                    new MilitaryDischarge() { Title = "Other than honourable", StatusType = StatusType.Active },
                });

                #endregion

                #region Military Present Duty

                context.MilitaryPresentDuties.AddRange(new List<MilitaryPresentDuty>()
                {
                    new MilitaryPresentDuty() { Title = "Active", StatusType = StatusType.Active },
                    new MilitaryPresentDuty() { Title = "Reserve", StatusType = StatusType.Active },
                    new MilitaryPresentDuty() { Title = "National Guard", StatusType = StatusType.Active }
                });

                #endregion

                #region Practice Location

                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion1 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion1.Title = "Re-use information from another practice location";
                //PracticeAccessibilityQuestion1.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion1);


                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion2 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion2.Title = "Does this ADA accessibility requirements?";
                //PracticeAccessibilityQuestion2.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion2);


                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion3 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion3.Title = "Does this meet site offer handicapped access for (Building, Parking, Restroom)?";
                //PracticeAccessibilityQuestion3.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion3);


                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion4 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion4.Title = "Does this site offer other services for disabled?";
                //PracticeAccessibilityQuestion4.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion4);


                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion5 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion5.Title = "Text telephone(TTY)";
                //PracticeAccessibilityQuestion5.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion5);


                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion6 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion6.Title = "America sign language";
                //PracticeAccessibilityQuestion6.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion6);

                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion7 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion7.Title = "Mental/Physical impairment services";
                //PracticeAccessibilityQuestion7.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion7);

                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion8 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion8.Title = "Accessible by public transportation(Bus, Subway, Regional train)";
                //PracticeAccessibilityQuestion8.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion8);

                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion9 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion9.Title = "Other handicapped access";
                //PracticeAccessibilityQuestion9.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion9);


                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion10 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion10.Title = "Other disability services";
                //PracticeAccessibilityQuestion10.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion10);


                //PracticeAccessibilityQuestion PracticeAccessibilityQuestion11 = new PracticeAccessibilityQuestion();
                //PracticeAccessibilityQuestion11.Title = "Other transportation access";
                //PracticeAccessibilityQuestion11.StatusType = StatusType.Active;
                //context.PracticeAccessibilityQuestions.Add(PracticeAccessibilityQuestion11);



                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion1 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion1.Title = "Re-use information from another practice location?";
                //PracticeOpenStatusQuestion1.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion1);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion2 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion2.Title = "Accept new patients into this practice?";
                //PracticeOpenStatusQuestion2.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion2);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion3 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion3.Title = "Accept all new patients?";
                //PracticeOpenStatusQuestion3.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion3);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion4 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion4.Title = "Accept existing patients with change of payer?";
                //PracticeOpenStatusQuestion4.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion4);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion5 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion5.Title = "Accept new Medicare patients?";
                //PracticeOpenStatusQuestion5.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion5);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion6 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion6.Title = "Accept new patients with physician referral?";
                //PracticeOpenStatusQuestion6.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion6);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion7 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion7.Title = "Accept new Medicaid patients?";
                //PracticeOpenStatusQuestion7.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion7);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion8 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion8.Title = "If any of the above information varies by plan, Explain";
                //PracticeOpenStatusQuestion8.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion8);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion9 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion9.Title = "Are there any practice limitations?";
                //PracticeOpenStatusQuestion9.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion9);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion10 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion10.Title = "Gender limitations";
                //PracticeOpenStatusQuestion10.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion10);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion11 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion11.Title = "Age limitations (Min age, Max age)";
                //PracticeOpenStatusQuestion11.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion11);

                //PracticeOpenStatusQuestion PracticeOpenStatusQuestion12 = new PracticeOpenStatusQuestion();
                //PracticeOpenStatusQuestion12.Title = "List other limitations";
                //PracticeOpenStatusQuestion12.StatusType = StatusType.Active;
                //context.PracticeOpenStatusQuestions.Add(PracticeOpenStatusQuestion12);


                //PracticeServiceQuestion PracticeServiceQuestion1 = new PracticeServiceQuestion();
                //PracticeServiceQuestion1.Title = "Re-use information from another practice location";
                //PracticeServiceQuestion1.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion1);


                //PracticeServiceQuestion PracticeServiceQuestion2 = new PracticeServiceQuestion();
                //PracticeServiceQuestion2.Title = "Laboratory Services?";
                //PracticeServiceQuestion2.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion2);

                //PracticeServiceQuestion PracticeServiceQuestion3 = new PracticeServiceQuestion();
                //PracticeServiceQuestion3.Title = "Provide Accrediting/Certifying program";
                //PracticeServiceQuestion3.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion3);

                //PracticeServiceQuestion PracticeServiceQuestion4 = new PracticeServiceQuestion();
                //PracticeServiceQuestion4.Title = "Radiology services?";
                //PracticeServiceQuestion4.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion4);

                //PracticeServiceQuestion PracticeServiceQuestion5 = new PracticeServiceQuestion();
                //PracticeServiceQuestion5.Title = "Provide X-Ray certificate type";
                //PracticeServiceQuestion5.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion5);

                //PracticeServiceQuestion PracticeServiceQuestion6 = new PracticeServiceQuestion();
                //PracticeServiceQuestion6.Title = "EKGS?";
                //PracticeServiceQuestion6.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion6);

                //PracticeServiceQuestion PracticeServiceQuestion7 = new PracticeServiceQuestion();
                //PracticeServiceQuestion7.Title = "Allergy Injection?";
                //PracticeServiceQuestion7.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion7);

                //PracticeServiceQuestion PracticeServiceQuestion8 = new PracticeServiceQuestion();
                //PracticeServiceQuestion8.Title = "Allergy skin testing?";
                //PracticeServiceQuestion8.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion8);

                //PracticeServiceQuestion PracticeServiceQuestion9 = new PracticeServiceQuestion();
                //PracticeServiceQuestion9.Title = "Routing office gynaecology(Pelvi/Pap)?";
                //PracticeServiceQuestion9.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion9);

                //PracticeServiceQuestion PracticeServiceQuestion10 = new PracticeServiceQuestion();
                //PracticeServiceQuestion10.Title = "Drawing blood?";
                //PracticeServiceQuestion10.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion10);

                //PracticeServiceQuestion PracticeServiceQuestion11 = new PracticeServiceQuestion();
                //PracticeServiceQuestion11.Title = "Age appropriate immunization?";
                //PracticeServiceQuestion11.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion11);

                //PracticeServiceQuestion PracticeServiceQuestion12 = new PracticeServiceQuestion();
                //PracticeServiceQuestion12.Title = "Flexible Sigmoidoscopy?";
                //PracticeServiceQuestion12.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion12);

                //PracticeServiceQuestion PracticeServiceQuestion13 = new PracticeServiceQuestion();
                //PracticeServiceQuestion13.Title = "Tympanometry/Audiometry screening?";
                //PracticeServiceQuestion13.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion13);

                //PracticeServiceQuestion PracticeServiceQuestion14 = new PracticeServiceQuestion();
                //PracticeServiceQuestion14.Title = "Asthma treatment?";
                //PracticeServiceQuestion14.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion14);


                //PracticeServiceQuestion PracticeServiceQuestion15 = new PracticeServiceQuestion();
                //PracticeServiceQuestion15.Title = "Osteopathic manipulation?";
                //PracticeServiceQuestion15.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion15);

                //PracticeServiceQuestion PracticeServiceQuestion16 = new PracticeServiceQuestion();
                //PracticeServiceQuestion16.Title = "IV Hydration? Treatment?";
                //PracticeServiceQuestion16.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion16);

                //PracticeServiceQuestion PracticeServiceQuestion17 = new PracticeServiceQuestion();
                //PracticeServiceQuestion17.Title = "Cardiac Stress test?";
                //PracticeServiceQuestion17.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion17);

                //PracticeServiceQuestion PracticeServiceQuestion18 = new PracticeServiceQuestion();
                //PracticeServiceQuestion18.Title = "Pulmonary Function Testing?";
                //PracticeServiceQuestion18.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion18);

                //PracticeServiceQuestion PracticeServiceQuestion19 = new PracticeServiceQuestion();
                //PracticeServiceQuestion19.Title = "Physical Therapy?";
                //PracticeServiceQuestion19.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion19);

                //PracticeServiceQuestion PracticeServiceQuestion20 = new PracticeServiceQuestion();
                //PracticeServiceQuestion20.Title = "Care of minor lacerations?";
                //PracticeServiceQuestion20.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion20);

                //PracticeServiceQuestion PracticeServiceQuestion21 = new PracticeServiceQuestion();
                //PracticeServiceQuestion21.Title = "Is anaesthesia administered in your office ";
                //PracticeServiceQuestion21.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion21);

                //PracticeServiceQuestion PracticeServiceQuestion22 = new PracticeServiceQuestion();
                //PracticeServiceQuestion22.Title = "What class/category do you use";
                //PracticeServiceQuestion22.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion22);

                //PracticeServiceQuestion PracticeServiceQuestion23 = new PracticeServiceQuestion();
                //PracticeServiceQuestion23.Title = "Who administers it";
                //PracticeServiceQuestion23.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion23);

                //PracticeServiceQuestion PracticeServiceQuestion24 = new PracticeServiceQuestion();
                //PracticeServiceQuestion24.Title = "First Name";
                //PracticeServiceQuestion24.StatusType = StatusType.Active;

                //PracticeServiceQuestion PracticeServiceQuestion25 = new PracticeServiceQuestion();
                //PracticeServiceQuestion25.Title = "Last Name";
                //PracticeServiceQuestion25.StatusType = StatusType.Active;

                //PracticeServiceQuestion PracticeServiceQuestion26 = new PracticeServiceQuestion();
                //PracticeServiceQuestion26.Title = "Type of practice";
                //PracticeServiceQuestion26.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion26);

                //PracticeServiceQuestion PracticeServiceQuestion27 = new PracticeServiceQuestion();
                //PracticeServiceQuestion27.Title = "Additional office procedures provide (Including surgical procedures)";
                //PracticeServiceQuestion27.StatusType = StatusType.Active;
                //context.PracticeServiceQuestions.Add(PracticeServiceQuestion27);

                //PracticeType PracticeType1 = new PracticeType();
                //PracticeType1.Title = "Solo Practice";
                //PracticeType1.StatusType = StatusType.Active;
                //context.PracticeTypes.Add(PracticeType1);

                //PracticeType PracticeType2 = new PracticeType();
                //PracticeType2.Title = "Single Specialty Group";
                //PracticeType2.StatusType = StatusType.Active;
                //context.PracticeTypes.Add(PracticeType2);


                //PracticeType PracticeType3 = new PracticeType();
                //PracticeType3.Title = "Multi-Specialty Group";
                //PracticeType3.StatusType = StatusType.Active;
                //context.PracticeTypes.Add(PracticeType3);

                #endregion

                #region Provider Type

                context.ProviderTypes.AddRange(new List<ProviderType>
                {
                    new ProviderType{Title = "Medical Doctor", Code="MD", StatusType=StatusType.Active},
                    new ProviderType{Title = "Doctor of Dental Surgery", Code="DDS",StatusType=StatusType.Active},
                    new ProviderType{Title = "Doctor of Dental Medicine", Code="DMD",StatusType=StatusType.Active},
                    new ProviderType{Title = "Doctor of Podiatric Medicine", Code="DPM",StatusType=StatusType.Active},
                    new ProviderType{Title = "Doctor of Chiropractic", Code="DC",StatusType=StatusType.Active},
                    new ProviderType{Title = "Osteopathic Doctor", Code="DO",StatusType=StatusType.Active},
                    new ProviderType{Title = "Acupuncturist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Audiologist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Biofeedback Technician", StatusType=StatusType.Active},
                    new ProviderType{Title = "Certified Registered Nurse Anaesthetist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Christian Science Practitioner", StatusType=StatusType.Active},
                    new ProviderType{Title = "Clinical Nurse Specialist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Clinical Psychologist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Clinical Social Worker", StatusType=StatusType.Active},
                    new ProviderType{Title = "Dietician", StatusType=StatusType.Active},
                    new ProviderType{Title = "Licensed Practical Nurse", StatusType=StatusType.Active},
                    new ProviderType{Title = "Marriage/Family Therapist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Massage Therapist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Naturopath", StatusType=StatusType.Active},
                    new ProviderType{Title = "Neuropsychologist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Midwife", StatusType=StatusType.Active},
                    new ProviderType{Title = "Nurse Midwife", StatusType=StatusType.Active},
                    new ProviderType{Title = "Nurse Practitioner", StatusType=StatusType.Active},
                    new ProviderType{Title = "Nutritionist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Occupational Therapist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Optician", StatusType=StatusType.Active},
                    new ProviderType{Title = "Optometrist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Pharmacist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Physical Therapist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Physician Assistant", StatusType=StatusType.Active},
                    new ProviderType{Title = "Professional Counsellor", StatusType=StatusType.Active},
                    new ProviderType{Title = "Registered Nurse", StatusType=StatusType.Active},
                    new ProviderType{Title = "Registered Nurse First Assistant", StatusType=StatusType.Active},
                    new ProviderType{Title = "Respiratory Therapist", StatusType=StatusType.Active},
                    new ProviderType{Title = "Speech Pathologist", StatusType=StatusType.Active}
                });

                #endregion

                #region Qualification Degree

                context.QualificationDegrees.AddRange(new List<QualificationDegree>
                {
                    new QualificationDegree{Title = "Associate degree" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Bachelor's degree" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Certificate of Advanced Study" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Doctorate" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Double majors in the United States" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Educational specialist" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Engineer's degree" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "First professional degree" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Master of Advanced Studies" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Master of Advanced Study" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Master's degree" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Specialist degree" , StatusType=StatusType.Active},
                    new QualificationDegree{Title = "Terminal degree" , StatusType=StatusType.Active}
                });

                #endregion

                #region School

                context.Schools.AddRange(new List<School>
                {
                    new School{Name = "University of Alabama School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Alabama School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of South Alabama College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Arkansas College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Arizona College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Arizona College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "California College of Podiatric Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Cleveland Chiropractic College of Los Angele" , StatusType=StatusType.Active},
                    new School{Name = "Keck School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Life Chiropractic College West" , StatusType=StatusType.Active},
                    new School{Name = "Loma Linda University School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Loma Linda University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Los Angeles College of Chiropractic" , StatusType=StatusType.Active},
                    new School{Name = "Palmer College of Chiropractic West" , StatusType=StatusType.Active},
                    new School{Name = "Quantum University/SCCC" , StatusType=StatusType.Active},
                    new School{Name = "Stanford University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Touro University College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "UCLA School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of California" , StatusType=StatusType.Active},
                    new School{Name = "University of California, Irvine, College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of California, Los Angeles School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of California, San Diego, School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of California, San Francisco, School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of California, San Francisco, School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Southern California School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of the Pacific School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Western University of Health Sciences, College of Osteopathic Medicine of the Pacific" , StatusType=StatusType.Active},
                    new School{Name = "University of Colorado School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Colorado School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Bridgeport College of Chiropractic" , StatusType=StatusType.Active},
                    new School{Name = "University of Connecticut School of Dental Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Connecticut School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Yale University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "George Washington University" , StatusType=StatusType.Active},
                    new School{Name = "Georgetown University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Howard University College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Howard University College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Barry University School of Graduate Medical Sciences" , StatusType=StatusType.Active},
                    new School{Name = "Nova Southeastern University College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Nova Southeastern University College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Florida College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Florida College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Miami School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of South Florida College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Emory University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Life Chiropractic College" , StatusType=StatusType.Active},
                    new School{Name = "Medical College of Georgia School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Medical College of Georgia School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Mercer University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Morehouse School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "John A. Burns School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "College of Podiatric Medicine and Surgery Des Moines University" , StatusType=StatusType.Active},
                    new School{Name = "Des Moines University, Osteopathic Medical Center, College of Osteopathic Medicine and Surgery" , StatusType=StatusType.Active},
                    new School{Name = "Palmer College of Chiropractic" , StatusType=StatusType.Active},
                    new School{Name = "University of Iowa College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Iowa College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Chicago Medical School, Finch University of Health Sciences" , StatusType=StatusType.Active},
                    new School{Name = "Loyola University Chicago, Stritch School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Midwestern University, Chicago College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "National College of Chiropractic" , StatusType=StatusType.Active},
                    new School{Name = "Northwestern University Dental School" , StatusType=StatusType.Active},
                    new School{Name = "Northwestern University Medical School" , StatusType=StatusType.Active},
                    new School{Name = "Rush Medical College of Rush University" , StatusType=StatusType.Active},
                    new School{Name = "Scholl College of Podiatric Medicine at Finch University" , StatusType=StatusType.Active},
                    new School{Name = "Southern Illinois University School of Dental Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Southern Illinois University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Chicago, The Pritzker School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Illinois at Chicago College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Illinois College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Indiana University School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Indiana University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Kansas School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Pikeville College, School of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Kentucky College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Kentucky College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Louisville School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Louisville School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Louisiana State University School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Louisiana State University School of Medicine in New Orleans" , StatusType=StatusType.Active},
                    new School{Name = "Louisiana State University School of Medicine in Shreveport 041 Tulane University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Boston University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Boston University, Goldman School of Dental Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Harvard Medical School" , StatusType=StatusType.Active},
                    new School{Name = "Harvard School of Dental Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Tufts University School of Dental Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Tufts University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Massachusetts Medical School" , StatusType=StatusType.Active},
                    new School{Name = "Johns Hopkins University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Uniformed Services University of the Health Sciences" , StatusType=StatusType.Active},
                    new School{Name = "University of Maryland School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Maryland, Baltimore, College of Dental Surgery" , StatusType=StatusType.Active},
                    new School{Name = "University of New England, College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Michigan State University College of Human Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Michigan State University, College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Detroit Mercy School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Michigan Medical School" , StatusType=StatusType.Active},
                    new School{Name = "University of Michigan School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Wayne State University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Mayo Medical School" , StatusType=StatusType.Active},
                    new School{Name = "Northwestern College of Chiropractic" , StatusType=StatusType.Active},
                    new School{Name = "University of Minnesota, Duluth School of Medicine 054 University of Minnesota Medical School, Twin Cities" , StatusType=StatusType.Active},
                    new School{Name = "University of Minnesota School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Cleveland Chiropractic College of Kansas City" , StatusType=StatusType.Active},
                    new School{Name = "Kirksville College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Logan Chiropractic College" , StatusType=StatusType.Active},
                    new School{Name = "Saint Louis University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Health Sciences, College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Missouri, Columbia School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Missouri Kansas City School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Missouri Kansas City School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Washington University in St. Louis School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Mississippi School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Mississippi School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Duke University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "The Brody School of Medicine at East Carolina University" , StatusType=StatusType.Active},
                    new School{Name = "University of North Carolina at Chapel Hill School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of North Carolina at Chapel Hill School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Wake Forest University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of North Dakota School of Medicine and Health Sciences" , StatusType=StatusType.Active},
                    new School{Name = "Creighton University School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Creighton University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Nebraska College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Nebraska Medical Center, College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Dartmouth Medical School" , StatusType=StatusType.Active},
                    new School{Name = "Robert Wood Johnson Medical School" , StatusType=StatusType.Active},
                    new School{Name = "University of Medicine and Dentistry of New Jersey (UMDNJ)" , StatusType=StatusType.Active},
                    new School{Name = "UMDNJ, New Jersey Dental School" , StatusType=StatusType.Active},
                    new School{Name = "UMDNJ, School of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of New Mexico School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Nevada School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Albany Medical College" , StatusType=StatusType.Active},
                    new School{Name = "Albert Einstein College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Columbia University College of Physicians and Surgeons" , StatusType=StatusType.Active},
                    new School{Name = "Columbia University School of Dental and Oral Surgery" , StatusType=StatusType.Active},
                    new School{Name = "Joan & Sanford I. Weill Medical College of Cornell University" , StatusType=StatusType.Active},
                    new School{Name = "Mount Sinai School of Medicine of New York University" , StatusType=StatusType.Active},
                    new School{Name = "New York Chiropractic College" , StatusType=StatusType.Active},
                    new School{Name = "NY College of Osteopathic Medicine of the NY Institute of Technology" , StatusType=StatusType.Active},
                    new School{Name = "New York Medical College" , StatusType=StatusType.Active},
                    new School{Name = "New York University Kriser Dental Center" , StatusType=StatusType.Active},
                    new School{Name = "New York University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "State University of New York at Buffalo School of Dental Medicine" , StatusType=StatusType.Active},
                    new School{Name = "State University of New York at Buffalo School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "State University of New York at Stony Brook School of Dental Medicine" , StatusType=StatusType.Active},
                    new School{Name = "081 State University of New York at Stony Brook School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "State University of New York College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "State University of New York Upstate Medical University" , StatusType=StatusType.Active},
                    new School{Name = "University of Rochester School of Medicine and Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Case Western Reserve University School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Case Western Reserve University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Medical College of Ohio" , StatusType=StatusType.Active},
                    new School{Name = "Northeastern Ohio Universities College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Ohio College of Podiatric Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Ohio State University College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Ohio State University College of Medicine and Public Health" , StatusType=StatusType.Active},
                    new School{Name = "Ohio University College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Cincinnati College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Wright State University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Oklahoma State University, College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Oklahoma College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Oklahoma College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Oregon Health & Science University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Oregon Health Sciences University School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Western States Chiropractic College" , StatusType=StatusType.Active},
                    new School{Name = "Jefferson Medical College of Thomas Jefferson University" , StatusType=StatusType.Active},
                    new School{Name = "Lake Erie College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "MCP Hahnemann University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Pennsylvania State University College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Philadelphia College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Temple University School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Temple University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Temple University School of Podiatric Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Pennsylvania School of Dental Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Pennsylvania School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Pittsburgh School of Dental Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Pittsburgh School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Ponce School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Universidad Central del Caribe School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Puerto Rico School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Puerto Rico School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Brown Medical School" , StatusType=StatusType.Active},
                    new School{Name = "Medical University of South Carolina College of Dental Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Medical University of South Carolina College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Sherman College of Chiropractic" , StatusType=StatusType.Active},
                    new School{Name = "University of South Carolina School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of South Dakota School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "East Tennessee State University" , StatusType=StatusType.Active},
                    new School{Name = "Meharry Medical College School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Meharry Medical College School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Tennessee College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Tennessee College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Vanderbilt University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Baylor College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Baylor College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Parker College of Chiropractic" , StatusType=StatusType.Active},
                    new School{Name = "Texas Chiropractic College" , StatusType=StatusType.Active},
                    new School{Name = "Texas Tech University Health Sciences Center School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "The Texas A & M University System College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "UNT Health Sciences Center, Texas College of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Texas Health Science Center at Houston Dental School" , StatusType=StatusType.Active},
                    new School{Name = "University of Texas Health Science Center at San Antonio Dental School" , StatusType=StatusType.Active},
                    new School{Name = "University of Texas Medical Branch at Galveston" , StatusType=StatusType.Active},
                    new School{Name = "University of Texas Medical School at Houston" , StatusType=StatusType.Active},
                    new School{Name = "University of Texas Medical School at San Antonio" , StatusType=StatusType.Active},
                    new School{Name = "UT Southwestern Medical Center at Dallas Southwestern Medical School" , StatusType=StatusType.Active},
                    new School{Name = "University of Utah School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Eastern VA Medical School of the Medical College of Hampton Roads" , StatusType=StatusType.Active},
                    new School{Name = "University of Virginia School of Medicine Health System" , StatusType=StatusType.Active},
                    new School{Name = "Virginia Commonwealth University School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Virginia Commonwealth University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Vermont College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Washington School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Washington School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Marquette University School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Medical College of Wisconsin" , StatusType=StatusType.Active},
                    new School{Name = "University of Wisconsin Medical School" , StatusType=StatusType.Active},
                    new School{Name = "Joan C. Edwards School of Medicine at Marshall University" , StatusType=StatusType.Active},
                    new School{Name = "West Virginia School of Osteopathic Medicine" , StatusType=StatusType.Active},
                    new School{Name = "West Virginia University School of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "West Virginia University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Dalhousie University Faculty of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Dalhousie University Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Laval University Faculty of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Laval University Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "McGill University Faculty of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "McGill University Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "McMaster University School of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Memorial University of Newfoundland Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Queens University Faculty of Health Sciences" , StatusType=StatusType.Active},
                    new School{Name = "The University of Western Ontario Faculty of Medicine & Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "Universite de Montreal Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "Universite de Sherbrooke Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Alberta Faculty of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Alberta Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of British Columbia Faculty of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of British Columbia Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Calgary Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Manitoba Faculty of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Manitoba Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Montreal Faculty of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Ottawa Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Saskatchewan College of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Saskatchewan College of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Toronto Faculty of Dentistry" , StatusType=StatusType.Active},
                    new School{Name = "University of Toronto Faculty of Medicine" , StatusType=StatusType.Active},
                    new School{Name = "University of Western Ontario Faculty of Dentistry" , StatusType=StatusType.Active}
                });

                #endregion

                #region Staff Categories

                context.StaffCategories.AddRange(new List<StaffCategory>(){
                    new StaffCategory() { Title = "Active", StatusType = StatusType.Active},
                    new StaffCategory() { Title = "Inactive", StatusType = StatusType.Active},
                    new StaffCategory() { Title = "Courtesy", StatusType = StatusType.Active},
                    new StaffCategory() { Title = "Expelled", StatusType = StatusType.Active},
                    new StaffCategory() { Title = "Suspended", StatusType = StatusType.Active}
                });

                #endregion

                #region State License Status

                context.StateLicenseStatuses.AddRange(new List<StateLicenseStatus>(){
                    new StateLicenseStatus{Title = " Active" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Cancelled" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Denied" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Expired" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Inactive" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Lapsed" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Limited" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Pending" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Probation" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Provisional" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Restricted" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Revoked" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Suspended" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Surrendered" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Temporary" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Terminated" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Time Limited" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Unrestricted" , StatusType=StatusType.Active},
                    new StateLicenseStatus{Title = "Other" , StatusType=StatusType.Active}
                });

                #endregion

                #region Visa Status

                context.VisaStatuses.AddRange(new List<VisaStatus>(){
                    new VisaStatus{Title="SEVIS",StatusType=StatusType.Active},
                    new VisaStatus{Title="DOL",StatusType=StatusType.Active},
                    new VisaStatus{Title="USCIS",StatusType=StatusType.Active},
                    new VisaStatus{Title="DOL then USCIS",StatusType=StatusType.Active}
                });

                #endregion

                #region Visa Type

                context.VisaTypes.AddRange(new List<VisaType>(){
                    new VisaType{Title="B1/B2",StatusType=StatusType.Active},
                    new VisaType{Title="H-1B",StatusType=StatusType.Active},
                    new VisaType{Title="L",StatusType=StatusType.Active},
                    new VisaType{Title="F",StatusType=StatusType.Active},
                    new VisaType{Title="M",StatusType=StatusType.Active},
                    new VisaType{Title="J",StatusType=StatusType.Active},
                    new VisaType{Title="C1/D",StatusType=StatusType.Active},
                    new VisaType{Title="H3",StatusType=StatusType.Active},
                    new VisaType{Title="I",StatusType=StatusType.Active},
                    new VisaType{Title="P1",StatusType=StatusType.Active},
                    new VisaType{Title="P2",StatusType=StatusType.Active},
                    new VisaType{Title="P3",StatusType=StatusType.Active},
                    new VisaType{Title="A",StatusType=StatusType.Active},
                    new VisaType{Title="G",StatusType=StatusType.Active},
                    new VisaType{Title="C",StatusType=StatusType.Active},
                    new VisaType{Title="R-1, R-2",StatusType=StatusType.Active}
                });

                #endregion

                context.SaveChanges();

                #region Military Rank

                var navyOrCoastGuardBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Navy")).ToList();
                var marOrAFBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Marine") || m.Title.Equals("Air Force")).ToList();
                var marAFNavCGBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Marine") || m.Title.Equals("Air Force") || m.Title.Equals("Navy") || m.Title.Equals("Coast Guard")).ToList();
                var marNavCGBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Marine") || m.Title.Equals("Navy") || m.Title.Equals("Coast Guard")).ToList();
                var marNavBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Marine") || m.Title.Equals("Navy")).ToList();
                var marArmyBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Marine") || m.Title.Equals("Army")).ToList();
                var marAFArmyBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Marine") || m.Title.Equals("Army") || m.Title.Equals("Air Force")).ToList();

                var marineBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Marine")).ToList();
                var airforceBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Air Force")).ToList();
                var navyBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Navy")).ToList();
                var coastGuardBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Coast Guard")).ToList();
                var armyBranch = context.MilitaryBranches.Where(m => m.Title.Equals("Army")).ToList();


                context.MilitaryRanks.AddRange(new List<MilitaryRank>()
                {
                    new MilitaryRank() { Title = "Admiral Chief of Naval ops / Commandant of the CG", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Airman", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Airman Apprentice", MilitaryBranches = coastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Airman Basic", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Airman First Class", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Brigadier General", MilitaryBranches = marOrAFBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Captain", MilitaryBranches = marAFNavCGBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Chief Master Sergeant", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Chief Master Sergeant (Diamond)", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Chief Master Sergeant of the Air Force", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Chief Petty officer", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Chief Warrant Officer 2", MilitaryBranches = marNavCGBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Chief Warrant Officer 3", MilitaryBranches = marNavCGBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Chief Warrant Officer 4", MilitaryBranches = marNavCGBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Chief Warrant Officer 5", MilitaryBranches = marNavBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Colonel", MilitaryBranches = marOrAFBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Command Chief Master Sergeant", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Command Master Chief Petty Officer", MilitaryBranches = coastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Command Sergeant Major", MilitaryBranches = armyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Commander", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Corporal", MilitaryBranches = marArmyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Ensign", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Fireman", MilitaryBranches = coastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Fireman Apprentice", MilitaryBranches = coastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "First Lieutenant", MilitaryBranches = marOrAFBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "First Sergeant", MilitaryBranches = marArmyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Fleet Admiral", MilitaryBranches = navyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Fleet/ Commander Master Chief Petty Officer", MilitaryBranches = navyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "General", MilitaryBranches = marineBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "General Air Force Chief of Staff", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "General of the Air Force", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Gennery Sergeant", MilitaryBranches = marineBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Lance Corporal", MilitaryBranches = marineBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Lieutenant", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Lieutenant Colonel", MilitaryBranches = marOrAFBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Lieutenant Commander", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Lieutenant General", MilitaryBranches = marOrAFBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Lieutenant Junior Grade", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Major", MilitaryBranches = marOrAFBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Major General", MilitaryBranches = marOrAFBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Master Chief Navy Officer", MilitaryBranches = navyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Master Chief Petty officer", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Master Chief Petty officer of the Coast Guard", MilitaryBranches = coastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Master Gunnery Sergeant", MilitaryBranches = marineBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Master Sergeant", MilitaryBranches = marAFArmyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Master Sergeant (Diamond)", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Petty Officer 1st Class", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Petty Officer 2nd Class", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Petty Officer 3rd Class", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Private", MilitaryBranches = marArmyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Private 2", MilitaryBranches = armyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Private First Class", MilitaryBranches = marArmyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Rear Admiral", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Seaman", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Seaman Apprentice", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Seaman Recruit", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Second Lieutenant", MilitaryBranches = marOrAFBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Senior Airman", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Senior Chief Petty Officer", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Senior Master Sergeant", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Senior Master Sergeant (Diamond)", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Seregeant Major", MilitaryBranches = marineBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Sergeant", MilitaryBranches = marArmyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Sergeant First Class", MilitaryBranches = armyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Sergeant Major", MilitaryBranches = armyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Sergeant Major of the Army", MilitaryBranches = armyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Sergeant Major of the Marine Corps", MilitaryBranches = marineBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Specialist", MilitaryBranches = armyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Staff Sergeant", MilitaryBranches = marAFArmyBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Technical Sergeant", MilitaryBranches = airforceBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Vice Admiral", MilitaryBranches = navyOrCoastGuardBranch, StatusType = StatusType.Active },
                    new MilitaryRank() { Title = "Warrant Officer", MilitaryBranches = marineBranch, StatusType = StatusType.Active },
                });



                #endregion

                #region Specialty

                var providerTypeMOorDO = context.ProviderTypes.Where(p => p.Code.Equals("MD") || p.Code.Equals("DO")).ToList();
                var providerTypeDDSorDMD = context.ProviderTypes.Where(p => p.Code.Equals("DDS") || p.Code.Equals("DMD")).ToList();
                var providerTypeDPM = context.ProviderTypes.Where(p => p.Code.Equals("DPM")).ToList();
                var providerTypeDC = context.ProviderTypes.Where(p => p.Code.Equals("DC")).ToList();

                context.Specialities.AddRange(new List<Specialty>()
                {
                    new Specialty{Name = "Allergy & Immunology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Allergy & Immunology, Allergy", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Allergy & Immunology, Clinical & Laboratory Immunology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Anesthesiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Anesthesiology, Addiction Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Anesthesiology, Critical Care Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Anesthesiology, Pain Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Clinical Pharmacology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Colon & Rectal Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Dermatology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Dermatology, Clinical & Laboratory Dermatological Immunology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Dermatology, Dermatological Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Dermatology, Dermatopathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Dermatology, MOHS-Micrographic Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Dermatology, Pediatric Dermatology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Emergency Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Emergency Medicine, Emergency Medical Services", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Emergency Medicine, Medical Toxicology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Emergency Medicine, Pediatric Emergency Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Emergency Medicine, Sports Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Emergency Medicine, Undersea and Hyperbaric Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Facial Plastic Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Family Practice", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Family Practice, Addiction Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Family Practice, Adolescent Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Family Practice, Adult Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Family Practice, Geriatric Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Family Practice, Sports Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "General Practice", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Hospitalist", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Addiction Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Adolescent Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Allergy & Immunology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Cardiovascular Disease", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Clinical & Laboratory Immunology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Clinical Cardiac Electrophysiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Critical Care Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Endocrinology, Diabetes & Metabolism", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Gastroenterology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Geriatric Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Hematology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Hematology & Oncology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Hepatology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Infectious Disease", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Interventional Cardiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Magnetic Resonance Imaging (MRI)", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Medical Oncology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Nephrology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Pulmonary Disease", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Rheumatology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Internal Medicine, Sports Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Laboratories, Clinical Medical Laboratory", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Legal Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Medical Genetics, Clinical Biochemical Genetics", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Medical Genetics, Clinical Cytogenetic", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Medical Genetics, Clinical Genetics (M.D.)", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Medical Genetics, Clinical Molecular Genetics", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Medical Genetics, Molecular Genetic Pathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Medical Genetics, Ph.D. Medical Genetics", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Neonatal-Perinatal Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Neopathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Neurological Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Neuromusculoskeletal Medicine & OMM", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Neuromusculoskeletal Medicine, Sports Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Nuclear Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Nuclear Medicine, In Vivo & In Vitro Nuclear Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Nuclear Medicine, Nuclear Cardiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Nuclear Medicine, Nuclear Imaging & Therapy", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Obstetrics & Gynecology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Obstetrics & Gynecology, Critical Care Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Obstetrics & Gynecology, Gynecologic Oncology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Obstetrics & Gynecology, Gynecology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Obstetrics & Gynecology, Maternal & Fetal Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Obstetrics & Gynecology, Obstetrics", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Obstetrics & Gynecology, Reproductive Endocrinology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Ophthalmology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Oral & Maxillofacial Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Orthopaedic Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Orthopaedic Surgery, Adult Reconstructive Orthopaedic Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Orthopaedic Surgery, Foot and Ankle Orthopaedics", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Orthopaedic Surgery, Hand Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Orthopaedic Surgery, Orthopaedic Surgery of the Spine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Orthopaedic Surgery, Orthopaedic Trauma", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Orthopaedic Surgery, Sports Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Orthopedic", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Otolaryngology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Otolaryngology, Otolaryngic Allergy", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Otolaryngology, Otolaryngology/ Facial Plastic Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Otolaryngology, Otology & Neurotology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Otolaryngology, Pediatric Otolaryngology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Otolaryngology, Plastic Surgery within the Head & Neck", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pain Medicine, Interventional Pain Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pain Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Anatomic Pathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Anatomic Pathology & Clinical Pathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Blood Banking & Transfusion Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Chemical Pathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Clinical Pathology/Laboratory Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Cytopathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = " Pathology, Dermatopathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Forensic Pathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Hematology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Immunopathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Medical Microbiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Molecular Genetic Pathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Neuropathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pathology, Pediatric Pathology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Adolescent Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Clinical & Laboratory Immunology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Developmental – Behavioral Pediatrics", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Medical Toxicology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Neurodevelopmental Disabilities", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Pediatric Allergy & Immunology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Pediatric Cardiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Pediatric Critical Care Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Pediatric Emergency Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Pediatric Endocrinology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = " Pediatrics, Pediatric Gastroenterology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Pediatric HematologyOncology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Pediatric Infectious Diseases", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Pediatric Nephrology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Pediatric Pulmonology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Pediatric Rheumatology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Pediatrics, Sports Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Physical Medicine & Rehabilitation", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Physical Medicine & Rehabilitation, Pain Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Physical Medicine & Rehabilitation, Pediatric Rehabilitation Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Physical Medicine & Rehabilitation, Spinal Cord Injury Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Physical Medicine & Rehabilitation, Sports Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Plastic Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Plastic Surgery, Plastic Surgery With in the Head and Neck", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Plastic Surgery, Surgery of the Hand", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Preventive Medicine, Aerospace Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Preventive Medicine, Medical Toxicology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Preventive Medicine, Occupational Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Preventive Medicine, Sports Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Preventive Medicine, Undersea and Hyperbaric Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Preventive Medicine/Occupational Environmental Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Addiction Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Addiction Psychiatry", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Child & Adolescent Psychiatry", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Clinical Neurophysiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Forensic Psychiatry", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = " Psychiatry & Neurology, Geriatric Psychiatry", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Neurodevelopmental Disabilities", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Neurology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Neurology with Special Qualifications in Child Neurology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Pain Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Psychiatry", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Sports Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Psychiatry & Neurology, Vascula Neurology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Public Health & General Preventive Medicine", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Radiology, Body Imaging", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Radiology, Diagnostic Radiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Radiology, Diagnostic Ultrasound", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Radiology, Neuroradiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Radiology, Nuclear Radiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Radiology, Pediatric Radiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Radiology, Radiation Oncology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Radiology, Radiological Physics", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Radiology, Therapeutic Radiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Radiology, Vascular & Interventional Radiology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Supplier", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Surgery, Pediatric Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Surgery, Plastic and Reconstructive Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Surgery, Surgery of the Hand", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Surgery, Surgical Critical Care", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Surgery, Surgical Oncology", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Surgery, Trauma Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Surgery, Vascular Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Thoracic Surgery (Cardiothoracic Vascular Surgery)", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Transplant Surgery", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Uroligy", ProviderTypes = providerTypeMOorDO, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist" , ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist, Dental Public Health", ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist, Endodontics", ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist, General Practice", ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist, Oral and Maxillofacial Pathology", ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist, Oral and Maxillofacial Radiology", ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist, Oral and Maxillofacial Surgery", ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist, Orthodontics and Dentofacial Orthopedics", ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist, Pediatric Dentistry", ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist, Periodontics", ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Dentist, Prosthodontics", ProviderTypes = providerTypeDDSorDMD, StatusType=StatusType.Active},
                    new Specialty{Name = "Podiatrist" , ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new Specialty{Name = "Podiatrist, Foot & Ankle Surgery", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new Specialty{Name = "Podiatrist, Foot Surgery", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new Specialty{Name = "Podiatrist, General Practice", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new Specialty{Name = "Podiatrist, Primary Podiatric Medicine", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new Specialty{Name = "Podiatrist, Public Medicine", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new Specialty{Name = "Podiatrist, Radiology", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new Specialty{Name = "Podiatrist, Sports Medicine", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new Specialty{Name = "Chiropractor", ProviderTypes = providerTypeDC, StatusType=StatusType.Active},
                    new Specialty{Name = "Chiropractor, Internist", ProviderTypes = providerTypeDC, StatusType=StatusType.Active},
                    new Specialty{Name = "Chiropractor, Neurology", ProviderTypes = providerTypeDC, StatusType=StatusType.Active},
                    new Specialty{Name = "Chiropractor, Nutrition", ProviderTypes = providerTypeDC, StatusType=StatusType.Active},
                    new Specialty{Name = "Chiropractor, Occupational Medicine", ProviderTypes = providerTypeDC, StatusType=StatusType.Active},
                    new Specialty{Name = "Chiropractor, Orthopedic", ProviderTypes = providerTypeDC, StatusType=StatusType.Active},
                    new Specialty{Name = "Chiropractor, Radiology", ProviderTypes = providerTypeDC, StatusType=StatusType.Active},
                    new Specialty{Name = "Chiropractor, Sports Physician", ProviderTypes = providerTypeDC, StatusType=StatusType.Active},
                    new Specialty{Name = "Chiropractor, Thermography", ProviderTypes = providerTypeDC, StatusType=StatusType.Active}
                });

                #endregion

                #region Speciality Board

                var providerTypeMD = context.ProviderTypes.Where(p => p.Code.Equals("MD")).ToList();
                var providerTypeDO = context.ProviderTypes.Where(p => p.Code.Equals("DO")).ToList();
                providerTypeDPM = context.ProviderTypes.Where(p => p.Code.Equals("DPM")).ToList();
                var providerTypeDental = context.ProviderTypes.Where(p => p.Code.Equals("DDS") || p.Code.Equals("DMD")).ToList();
                var providerTypeOther = context.ProviderTypes.Where(p => p.Code.Equals("MD") || p.Code.Equals("DDS") || p.Code.Equals("DMD")).ToList();

                context.SpecialtyBoards.AddRange(new List<SpecialtyBoard>(){
                    new SpecialtyBoard{Name = "American Board of Allergy & Immunology", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Anesthesiology", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Colon & Rectal Surgery", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Dermatology", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Emergency Medicine", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Family Medicine", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Internal Medicine", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Medical Genetics", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Neurological Surgery", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Nuclear Medicine", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Obstetrics & Gynecology", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Ophthalmology", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Oral & Maxillofacial Surgeons", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Orthopedic Surgery", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Otolaryngology", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Pathology", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Pediatrics", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Physical Medicine & Rehabilitation", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Plastic Surgery", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Preventive Medicine", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Psychiatry & Neurology", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Radiology", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Surgery", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Thoracic Surgery", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Urology", ProviderTypes = providerTypeMD, StatusType=StatusType.Active},                    
                    new SpecialtyBoard{Name = "American Board of Endodontics", ProviderTypes = providerTypeDental, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Oral & Maxillofacial Pathology", ProviderTypes = providerTypeDental, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Oral & Maxillofacial Radiology", ProviderTypes = providerTypeDental, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Orthodontics", ProviderTypes = providerTypeDental, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Pediatric Dentistry", ProviderTypes = providerTypeDental, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Periodontology", ProviderTypes = providerTypeDental, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Prosthodontics", ProviderTypes = providerTypeDental, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Public Health Dentistry", ProviderTypes = providerTypeDental, StatusType=StatusType.Active},                    
                    new SpecialtyBoard{Name = "Boards other than ABMS/AOA", ProviderTypes = providerTypeOther, StatusType=StatusType.Active},                    
                    new SpecialtyBoard{Name = "American Osteopathic Board of Anesthesiology", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Dermatology", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Emergency Medicine", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Family Practice", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Internal Medicine", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Neurology and Psychiatry", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Neuromuskuloskeletal Medicine", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Nuclear Medicine", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Obstetrics and Gynecology", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Ophthalmology and Otolaryngology", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Orthopedic Surgery", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Pathology", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Pediatrics", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Preventive Medicine", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Proctology", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Radiology", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Rehabilitation Medicine", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Osteopathic Board of Surgery", ProviderTypes = providerTypeDO, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Medical Specialists in Podiatry", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Podiatric Orthopedics and Primary Podiatric Medicine", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Board of Podiatric Surgery", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active},
                    new SpecialtyBoard{Name = "American Council of Certified Podiatric Surgeons and Physicians", ProviderTypes = providerTypeDPM, StatusType=StatusType.Active}
                });

                #endregion

                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Console.WriteLine(ex.Message);
                throw new DatabaseValidationException(String.Join(", ", ex.EntityValidationErrors.SelectMany(m => m.ValidationErrors).Select(e => e.ErrorMessage)),
                                                        ExceptionMessage.DATABASE_VALIDATION_EXCEPTION,
                                                        ex);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
