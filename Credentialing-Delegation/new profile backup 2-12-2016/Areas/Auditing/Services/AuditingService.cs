using PortalTemplate.Areas.Auditing.Models.AuditingList;
using PortalTemplate.Areas.Auditing.Models.CreateAuditing;
using PortalTemplate.Areas.Auditing.Services.IServices;
using PortalTemplate.Areas.SharedView.Models.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Services
{
    public class AuditingService : IAuditingService
    {
        List<CoderQueueViewModel> CoderList;
        List<CommitteeQueueViewModel> CommitteeList;
        List<ProviderQueueViewModel> ProviderList;
        List<QCQueueViewModel> QCList;
        List<ReadytoBillViewModel> RBAuditingList;
        List<ReadytoBillViewModel> DraftAuditingList;
        List<InactiveQueueViewModel> InactiveList;
        public AuditingService()
        {
            CoderList = new List<CoderQueueViewModel>();
            CommitteeList = new List<CommitteeQueueViewModel>();
            ProviderList = new List<ProviderQueueViewModel>();
            QCList = new List<QCQueueViewModel>();
            RBAuditingList = new List<ReadytoBillViewModel>();
            DraftAuditingList = new List<ReadytoBillViewModel>();
            InactiveList = new List<InactiveQueueViewModel>();

            CoderList.Add(new CoderQueueViewModel { EncounterId = 116244954, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 2), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "MEDICARE", Status = "On Hold" });
            CoderList.Add(new CoderQueueViewModel { EncounterId = 116244955, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 2), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            CoderList.Add(new CoderQueueViewModel { EncounterId = 116244956, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 2), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "MEDICARE", Status = "On Hold" });
            CoderList.Add(new CoderQueueViewModel { EncounterId = 116244957, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 2), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            CoderList.Add(new CoderQueueViewModel { EncounterId = 116244958, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 2), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            CoderList.Add(new CoderQueueViewModel { EncounterId = 116244959, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 2), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "MEDICARE", Status = "On Hold" });
            CoderList.Add(new CoderQueueViewModel { EncounterId = 116244960, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 2), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });

            CommitteeList.Add(new CommitteeQueueViewModel { EncounterId = 116244952, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 10), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            CommitteeList.Add(new CommitteeQueueViewModel { EncounterId = 116244953, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 10), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            CommitteeList.Add(new CommitteeQueueViewModel { EncounterId = 116244954, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 10), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "MEDICARE", Status = "On Hold" });
            CommitteeList.Add(new CommitteeQueueViewModel { EncounterId = 116244955, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 10), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            CommitteeList.Add(new CommitteeQueueViewModel { EncounterId = 116244956, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 10), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            CommitteeList.Add(new CommitteeQueueViewModel { EncounterId = 116244957, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 10), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            CommitteeList.Add(new CommitteeQueueViewModel { EncounterId = 116244958, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 8, 10), DOC = new DateTime(2016, 8, 10), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });

            ProviderList.Add(new ProviderQueueViewModel { EncounterId = 116244952, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            ProviderList.Add(new ProviderQueueViewModel { EncounterId = 116244953, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Rejected by Committee" });
            ProviderList.Add(new ProviderQueueViewModel { EncounterId = 116244954, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "MEDICARE", Status = "On Hold" });
            ProviderList.Add(new ProviderQueueViewModel { EncounterId = 116244955, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            ProviderList.Add(new ProviderQueueViewModel { EncounterId = 116244956, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            ProviderList.Add(new ProviderQueueViewModel { EncounterId = 116244957, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            ProviderList.Add(new ProviderQueueViewModel { EncounterId = 116244958, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });

            QCList.Add(new QCQueueViewModel { EncounterId = 116244951, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            QCList.Add(new QCQueueViewModel { EncounterId = 116244952, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            QCList.Add(new QCQueueViewModel { EncounterId = 116244953, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            QCList.Add(new QCQueueViewModel { EncounterId = 116244954, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            QCList.Add(new QCQueueViewModel { EncounterId = 116244955, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            QCList.Add(new QCQueueViewModel { EncounterId = 116244956, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            QCList.Add(new QCQueueViewModel { EncounterId = 116244957, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });

            RBAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244957, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            RBAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244951, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            RBAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244952, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            RBAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244953, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            RBAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244954, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            RBAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244955, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            RBAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244956, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            RBAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244957, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });

            DraftAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244952, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            DraftAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244953, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            DraftAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244954, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            DraftAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244955, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            DraftAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244956, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });
            DraftAuditingList.Add(new ReadytoBillViewModel { EncounterId = 116244957, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Bill" });

            InactiveList.Add(new InactiveQueueViewModel { EncounterId = 116244951, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            InactiveList.Add(new InactiveQueueViewModel { EncounterId = 116244952, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            InactiveList.Add(new InactiveQueueViewModel { EncounterId = 116244953, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            InactiveList.Add(new InactiveQueueViewModel { EncounterId = 116244954, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            InactiveList.Add(new InactiveQueueViewModel { EncounterId = 116244955, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            InactiveList.Add(new InactiveQueueViewModel { EncounterId = 116244956, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
            InactiveList.Add(new InactiveQueueViewModel { EncounterId = 116244957, MemberId = 1607132022, MemberLastName = "Oliver", MemberFirstName = "Kameisha", ProviderNPI = "3282556434", ProviderLastName = "Rubino", ProviderFirstName = "Ignazio", Facility = "ACCESS 2 HEALTHCARE", DOS = new DateTime(2016, 6, 5), DOC = new DateTime(2016, 5, 6), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On Hold" });
        }

        public List<CoderQueueViewModel> GetCoderList()
        {
            return CoderList;
        }

        public List<CommitteeQueueViewModel> GetCommitteeList()
        {
            return CommitteeList;
        }

        public List<ProviderQueueViewModel> GetProviderList()
        {
            return ProviderList;
        }

        public List<QCQueueViewModel> GetQCList()
        {
            return QCList;
        }

        public List<ReadytoBillViewModel> GetRBAuditingList()
        {
            return RBAuditingList;
        }

        public List<ReadytoBillViewModel> GetDraftAuditingList()
        {
            return DraftAuditingList;
        }

        public List<InactiveQueueViewModel> GetInactiveList()
        {
            return InactiveList;
        }

        public AuditingQueuesCountViewModel GetAuditingListStatusCount()
        {
            AuditingQueuesCountViewModel QueuesCount = new AuditingQueuesCountViewModel();
            QueuesCount.CoderQueueCount = CoderList.Count;
            QueuesCount.CommitteeQueueCount = CommitteeList.Count;
            QueuesCount.ProviderQueueCount = ProviderList.Count;
            QueuesCount.QcQueueCount = QCList.Count;
            QueuesCount.RBQueueCount = RBAuditingList.Count;
            QueuesCount.DraftListCount = DraftAuditingList.Count;
            QueuesCount.InactiveQueueCount = InactiveList.Count;
            return QueuesCount;
        }
        //******************View Auditing*******************************//
        #region View Auditing
        public CreateAuditingViewModel ViewAuditing()
        {
            CreateAuditingViewModel AuditDetails = new CreateAuditingViewModel();
            List<CategoryViewModel> CategoryDetails = new List<CategoryViewModel>();
            CategoryViewModel category1 = new CategoryViewModel { CategoryName = "HISTORY OF PRESENT ILLNESS", Remarks = "PATIENT HAS HISTORY OF ILLNESS", CategoryCode = "001" };
            CategoryViewModel category2 = new CategoryViewModel { CategoryName = "CHIEF COMPLAINT", Remarks = "THERE IS NO CHEIF COMPLAINT AVAILABLE", CategoryCode = "000" };
            CategoryViewModel category3 = new CategoryViewModel { CategoryName = "HISTORY REFERENCES", Remarks = "THERE ARE NO REFERENCES FOR PATIENT HISTORY", CategoryCode = "004" };
            CategoryDetails.Add(category1);
            CategoryDetails.Add(category2);
            CategoryDetails.Add(category3);

            AuditDetails.EncounterDetails.EncounterDetails.CheckInTime = "10:30 am";
            AuditDetails.EncounterDetails.EncounterDetails.CheckOutTime = "11:00 am";
            AuditDetails.EncounterDetails.EncounterDetails.DOSFrom = new DateTime(2016, 10, 09);
            AuditDetails.EncounterDetails.EncounterDetails.DOSTo = new DateTime(2016, 10, 10);
            AuditDetails.EncounterDetails.EncounterDetails.EncounterId = "116244951";
            //AuditDetails.EncounterDetails.EncounterDetails.EncounterNotes = "PROGRESS NOTES NOT ATTACHED";
            AuditDetails.EncounterDetails.EncounterDetails.EncounterStatus = "OPEN";
            AuditDetails.EncounterDetails.EncounterDetails.EncounterType = "HMO";
            AuditDetails.EncounterDetails.EncounterDetails.NextAppointmentDate = new DateTime(2016, 12, 12);
            AuditDetails.EncounterDetails.EncounterDetails.PatientBirthDate = new DateTime(1985, 10, 10);
            AuditDetails.EncounterDetails.EncounterDetails.PatientCity = "AMIDON";
            AuditDetails.EncounterDetails.EncounterDetails.PatientFirstAddress = "";
            AuditDetails.EncounterDetails.EncounterDetails.PatientFirstName = "SMITH";
            AuditDetails.EncounterDetails.EncounterDetails.PatientGender = "MALE";
            AuditDetails.EncounterDetails.EncounterDetails.PatientLastOrOrganizationName = "LUCAS";
            AuditDetails.EncounterDetails.EncounterDetails.PatientMiddleName = "";
            AuditDetails.EncounterDetails.EncounterDetails.PatientSecondAddress = "";
            AuditDetails.EncounterDetails.EncounterDetails.PatientState = "FLORIDA";
            AuditDetails.EncounterDetails.EncounterDetails.PatientZip = "100482";
            AuditDetails.EncounterDetails.EncounterDetails.PlanName = "HUMANA";
            AuditDetails.EncounterDetails.EncounterDetails.ReferringProvider = "PARIKSITH SINGH";
            AuditDetails.EncounterDetails.EncounterDetails.BillingProvider = "PARIKSITH SINGH";
            AuditDetails.EncounterDetails.EncounterDetails.ServiceFacility = "1094 NORTH CLIFFE DLVE";
            AuditDetails.EncounterDetails.EncounterDetails.DOSFrom = new DateTime(2016, 10, 10);
            AuditDetails.EncounterDetails.EncounterDetails.DOSTo = new DateTime(2016, 10, 11);
            AuditDetails.EncounterDetails.EncounterDetails.PlaceOfService = "21-INPATIENT HOSPITAL";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderFirstName = "PARIKSITH";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderLastOrOrganizationName = "SINGH";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderMiddleName = "";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderNPI = "1417989625";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderSpeciality = "INTERNAL MEDICINE";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderTaxonomy = "207R00000X";
            AuditDetails.EncounterDetails.EncounterDetails.SubscriberID = "K277057937";
            AuditDetails.EncounterDetails.EncounterDetails.VisitLength = "30 MINUTES";
            AuditDetails.EncounterDetails.EncounterDetails.VisitReason = "MVA";
            AuditDetails.EncounterDetails.IsAgree = true;
            AuditDetails.EncounterDetails.Categories = CategoryDetails;

            List<HCCCodeDetailsViewModel> HccCodeList1 = new List<HCCCodeDetailsViewModel>();
            List<HCCCodeDetailsViewModel> HccCodeList2 = new List<HCCCodeDetailsViewModel>();
            HCCCodeDetailsViewModel HccCode1 = new HCCCodeDetailsViewModel();
            HCCCodeDetailsViewModel HccCode2 = new HCCCodeDetailsViewModel();
            HccCode1.HCCCode = "23";
            HccCode1.HCCDescription = "Septicemia/Shock";
            HccCode1.HCCType = "Medical";
            HccCode1.HCCVersion = "v22";
            HccCode1.HCCWeight = "0.2";

            HccCode2.HCCCode = "23";
            HccCode2.HCCDescription = "Septicemia/Shock";
            HccCode2.HCCType = "Medical";
            HccCode2.HCCVersion = "v22";
            HccCode2.HCCWeight = "0.2";

            HccCodeList1.Add(HccCode1);
            HccCodeList1.Add(HccCode2);
            HccCodeList2.Add(HccCode2);

            List<ICDCodeDetailsViewModel> ICDCodeLists = new List<ICDCodeDetailsViewModel>();
            ICDCodeLists.Add(new ICDCodeDetailsViewModel { Code = "A207", Description = "Septicemic Plague", HCCCodes = HccCodeList1 });
            ICDCodeLists.Add(new ICDCodeDetailsViewModel { Code = "B207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeDetailsViewModel { Code = "C207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeDetailsViewModel { Code = "D207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeDetailsViewModel { Code = "E207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            AuditDetails.CodingDetails.ICDCodeDetails.ICDCodes = ICDCodeLists;
            AuditDetails.CodingDetails.ICDCodeDetails.ICDIndicatorType = "ICD10";

            List<CPTCodeDetailsViewModel> CPTCodeLists = new List<CPTCodeDetailsViewModel>();
            CPTCodeLists.Add(new CPTCodeDetailsViewModel { Code = "77852", Description = "CELL ENEMERATION ID", Modifier1 = "10", Modifier2 = "15", Modifier3 = "11", Modifier4 = "12", DiagnosisPointer1 = "A207", DiagnosisPointer2 = "C204", DiagnosisPointer3 = "B407", DiagnosisPointer4 = "A206", Fee = 35.66, IsAgree = true });
            CPTCodeLists.Add(new CPTCodeDetailsViewModel { Code = "77853", Description = "CELL ENUMERATION PHYS THERAPY", Modifier1 = "8", Modifier2 = "25", Modifier3 = "51", Modifier4 = "12", DiagnosisPointer1 = "A208", DiagnosisPointer2 = "C205", DiagnosisPointer3 = "B408", DiagnosisPointer4 = "A209", Fee = 32.66, IsAgree = true });
            CPTCodeLists.Add(new CPTCodeDetailsViewModel { Code = "77854", Description = "AUTOLOGOUS BLOOD PROCESS", Modifier1 = "9", Modifier2 = "20", Modifier3 = "19", Modifier4 = "19", DiagnosisPointer1 = "A209", DiagnosisPointer2 = "C206", DiagnosisPointer3 = "B409", DiagnosisPointer4 = "A208", Fee = 45.66, IsAgree = false, Categories = CategoryDetails });
            CPTCodeLists.Add(new CPTCodeDetailsViewModel { Code = "99201", Description = "", Fee = 75.66, isEnM = true, IsAgree = false, Categories = CategoryDetails });
            AuditDetails.CodingDetails.CPTCodeDetails.CPTCodes = CPTCodeLists;

            List<DocumentViewModel> documentList = new List<DocumentViewModel>
            {
                new DocumentViewModel{ DocumentID=1, DocumentName="Notes", Category="Progress Note", UploadedBy="Moore", UploadedOn="12/12/2016"},
                new DocumentViewModel{ DocumentID=2, DocumentName="X-Ray", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"},
                new DocumentViewModel{ DocumentID=3, DocumentName="Sonogram", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"}
            };
            AuditDetails.EncounterDetails.EncounterDetails.DocumentDetails.DocumentHistory = documentList;
            AuditDetails.EncounterDetails.EncounterDetails.DocumentDetails.UploadedDocuments = documentList;

            return AuditDetails;
        }
        #endregion
        //******************View Auditing*******************************//


        //******************Edit Auditing*******************************//
        #region Edit Auditing
        public CreateAuditingViewModel EditAuditing()
        {
            CreateAuditingViewModel AuditDetails = new CreateAuditingViewModel();
            List<CategoryViewModel> CategoryDetails = new List<CategoryViewModel>();
            CategoryViewModel category1 = new CategoryViewModel { CategoryName = "HISTORY OF PRESENT ILLNESS", Remarks = "PATIENT HAS HISTORY OF ILLNESS", CategoryCode = "001", selected = true };
            CategoryViewModel category2 = new CategoryViewModel { CategoryName = "CHIEF COMPLAINT", Remarks = "THERE IS NO CHEIF COMPLAINT AVAILABLE", CategoryCode = "000", selected = true };
            CategoryViewModel category3 = new CategoryViewModel { CategoryName = "HISTORY REFERENCES", Remarks = "THERE ARE NO REFERENCES FOR PATIENT HISTORY", CategoryCode = "004", selected = true };
            CategoryDetails.Add(category1);
            CategoryDetails.Add(category2);
            CategoryDetails.Add(category3);

            AuditDetails.EncounterDetails.EncounterDetails.CheckInTime = "10:30 am";
            AuditDetails.EncounterDetails.EncounterDetails.CheckOutTime = "11:00 am";
            AuditDetails.EncounterDetails.EncounterDetails.DOSFrom = new DateTime(2016, 10, 09);
            AuditDetails.EncounterDetails.EncounterDetails.DOSTo = new DateTime(2016, 10, 10);
            AuditDetails.EncounterDetails.EncounterDetails.EncounterId = "116244951";
            //AuditDetails.EncounterDetails.EncounterDetails.EncounterNotes = "PROGRESS NOTES NOT ATTACHED";
            AuditDetails.EncounterDetails.EncounterDetails.EncounterStatus = "OPEN";
            AuditDetails.EncounterDetails.EncounterDetails.EncounterType = "HMO";
            AuditDetails.EncounterDetails.EncounterDetails.NextAppointmentDate = new DateTime(2016, 12, 12);
            AuditDetails.EncounterDetails.EncounterDetails.PatientBirthDate = new DateTime(1985, 10, 10);
            AuditDetails.EncounterDetails.EncounterDetails.PatientCity = "AMIDON";
            AuditDetails.EncounterDetails.EncounterDetails.PatientFirstAddress = "";
            AuditDetails.EncounterDetails.EncounterDetails.PatientFirstName = "SMITH";
            AuditDetails.EncounterDetails.EncounterDetails.PatientGender = "MALE";
            AuditDetails.EncounterDetails.EncounterDetails.PatientLastOrOrganizationName = "LUCAS";
            AuditDetails.EncounterDetails.EncounterDetails.PatientMiddleName = "";
            AuditDetails.EncounterDetails.EncounterDetails.PatientSecondAddress = "";
            AuditDetails.EncounterDetails.EncounterDetails.PatientState = "FLORIDA";
            AuditDetails.EncounterDetails.EncounterDetails.PatientZip = "100482";
            AuditDetails.EncounterDetails.EncounterDetails.PlanName = "HUMANA";
            AuditDetails.EncounterDetails.EncounterDetails.ReferringProvider = "PARIKSITH SINGH";
            AuditDetails.EncounterDetails.EncounterDetails.BillingProvider = "PARIKSITH SINGH";
            AuditDetails.EncounterDetails.EncounterDetails.ServiceFacility = "1094 NORTH CLIFFE DLVE";
            AuditDetails.EncounterDetails.EncounterDetails.DOSFrom = new DateTime(2016, 10, 10);
            AuditDetails.EncounterDetails.EncounterDetails.DOSTo = new DateTime(2016, 10, 11);
            AuditDetails.EncounterDetails.EncounterDetails.PlaceOfService = "21-INPATIENT HOSPITAL";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderFirstName = "PARIKSITH";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderLastOrOrganizationName = "SINGH";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderMiddleName = "";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderNPI = "1417989625";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderSpeciality = "INTERNAL MEDICINE";
            AuditDetails.EncounterDetails.EncounterDetails.RenderingProviderTaxonomy = "207R00000X";
            AuditDetails.EncounterDetails.EncounterDetails.SubscriberID = "K277057937";
            AuditDetails.EncounterDetails.EncounterDetails.VisitLength = "30 MINUTES";
            AuditDetails.EncounterDetails.EncounterDetails.VisitReason = "MVA";
            AuditDetails.EncounterDetails.IsAgree = false;
            AuditDetails.EncounterDetails.Categories = CategoryDetails;

            List<HCCCodeDetailsViewModel> HccCodeList1 = new List<HCCCodeDetailsViewModel>();
            List<HCCCodeDetailsViewModel> HccCodeList2 = new List<HCCCodeDetailsViewModel>();
            HCCCodeDetailsViewModel HccCode1 = new HCCCodeDetailsViewModel();
            HCCCodeDetailsViewModel HccCode2 = new HCCCodeDetailsViewModel();
            HccCode1.HCCCode = "23";
            HccCode1.HCCDescription = "Septicemia/Shock";
            HccCode1.HCCType = "Medical";
            HccCode1.HCCVersion = "v22";
            HccCode1.HCCWeight = "0.2";

            HccCode2.HCCCode = "23";
            HccCode2.HCCDescription = "Septicemia/Shock";
            HccCode2.HCCType = "Medical";
            HccCode2.HCCVersion = "v22";
            HccCode2.HCCWeight = "0.2";

            HccCodeList1.Add(HccCode1);
            HccCodeList1.Add(HccCode2);
            HccCodeList2.Add(HccCode2);

            List<ICDCodeDetailsViewModel> ICDCodeLists = new List<ICDCodeDetailsViewModel>();
            ICDCodeLists.Add(new ICDCodeDetailsViewModel { Code = "A207", Description = "Septicemic Plague", HCCCodes = HccCodeList1, IsAgree = true });
            ICDCodeLists.Add(new ICDCodeDetailsViewModel { Code = "B207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsAgree = true });
            ICDCodeLists.Add(new ICDCodeDetailsViewModel { Code = "C207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsAgree = true });
            ICDCodeLists.Add(new ICDCodeDetailsViewModel { Code = "D207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsAgree = true });
            ICDCodeLists.Add(new ICDCodeDetailsViewModel { Code = "E207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsAgree = false, Categories = CategoryDetails });
            AuditDetails.CodingDetails.ICDCodeDetails.ICDCodes = ICDCodeLists;
            AuditDetails.CodingDetails.ICDCodeDetails.ICDIndicatorType = "version10";

            List<CPTCodeDetailsViewModel> CPTCodeLists = new List<CPTCodeDetailsViewModel>();
            CPTCodeLists.Add(new CPTCodeDetailsViewModel { Code = "77852", Description = "CELL ENEMERATION ID", Modifier1 = "10", Modifier2 = "15", Modifier3 = "11", Modifier4 = "12", DiagnosisPointer1 = "A207", DiagnosisPointer2 = "C204", DiagnosisPointer3 = "B407", DiagnosisPointer4 = "A206", Fee = 35.66, IsAgree = true });
            CPTCodeLists.Add(new CPTCodeDetailsViewModel { Code = "77853", Description = "CELL ENUMERATION PHYS THERAPY", Modifier1 = "8", Modifier2 = "25", Modifier3 = "51", Modifier4 = "12", DiagnosisPointer1 = "A208", DiagnosisPointer2 = "C205", DiagnosisPointer3 = "B408", DiagnosisPointer4 = "A209", Fee = 32.66, IsAgree = true });
            CPTCodeLists.Add(new CPTCodeDetailsViewModel { Code = "77854", Description = "AUTOLOGOUS BLOOD PROCESS", Modifier1 = "9", Modifier2 = "20", Modifier3 = "19", Modifier4 = "19", DiagnosisPointer1 = "A209", DiagnosisPointer2 = "C206", DiagnosisPointer3 = "B409", DiagnosisPointer4 = "A208", Fee = 45.66, IsAgree = false, Categories = CategoryDetails });
            CPTCodeLists.Add(new CPTCodeDetailsViewModel { Code = "99201", Description = "", Fee = 75.66, isEnM = true, IsAgree = false, Categories = CategoryDetails });
            AuditDetails.CodingDetails.CPTCodeDetails.CPTCodes = CPTCodeLists;

            List<DocumentViewModel> documentList = new List<DocumentViewModel>
            {
                new DocumentViewModel{ DocumentID=1, DocumentName="Notes", Category="Progress Note", UploadedBy="Moore", UploadedOn="12/12/2016"},
                new DocumentViewModel{ DocumentID=2, DocumentName="X-Ray", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"},
                new DocumentViewModel{ DocumentID=3, DocumentName="Sonogram", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"}
            };
            AuditDetails.EncounterDetails.EncounterDetails.DocumentDetails.DocumentHistory = documentList;
            AuditDetails.EncounterDetails.EncounterDetails.DocumentDetails.UploadedDocuments = documentList;

            return AuditDetails;
        }
        #endregion
        //******************Edit Auditing*******************************//


        //******************Get the list of Categories*******************//
        #region Categories List
        public List<CategoryViewModel> GetAllCategories()
        {
            List<CategoryViewModel> AllCategories = new List<CategoryViewModel>();
            AllCategories.Add(new CategoryViewModel { CategoryCode = "000", CategoryName = "Chief Complaint" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "001", CategoryName = "History of present illness" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "002", CategoryName = "Review of Systems" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "003", CategoryName = "Past, Family and/or Social History" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "004", CategoryName = "History References" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "005", CategoryName = "Examination" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "006", CategoryName = "Medical Decision Making Level" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "007", CategoryName = "Modifier" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "008", CategoryName = "Incomplete Notes" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "009", CategoryName = "Missing Signature" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "100", CategoryName = "No E&amp;M Code" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "101", CategoryName = "Others" });
            return AllCategories;
        }
        #endregion
        //******************Get the list of Categories*******************//
    }
}