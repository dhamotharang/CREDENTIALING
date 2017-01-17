namespace AHC.CD.Data.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class integration1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuestionProfileDisclosureQuestions", newName: "ProfileDisclosureQuestionQuestions");
            DropPrimaryKey("dbo.ProfileDisclosureQuestionQuestions");
            CreateTable(
                "dbo.PlanContactDetails",
                c => new
                    {
                        PlanContactDetailID = c.Int(nullable: false, identity: true),
                        ContactPersonName = c.String(),
                        EmailAddress = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        Number = c.String(),
                        ContactPersonFax = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false),
                        Plan_PlanID = c.Int(),
                        PlanLOB_PlanLOBID = c.Int(),
                    })
                .PrimaryKey(t => t.PlanContactDetailID)
                .ForeignKey("dbo.Plans", t => t.Plan_PlanID)
                .ForeignKey("dbo.PlanLOBs", t => t.PlanLOB_PlanLOBID)
                .Index(t => t.Plan_PlanID)
                .Index(t => t.PlanLOB_PlanLOBID);
            
            CreateTable(
                "dbo.CredentialingInfoes",
                c => new
                    {
                        CredentialingInfoID = c.Int(nullable: false, identity: true),
                        ProfileID = c.Int(),
                        PlanID = c.Int(),
                        InitiationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        InitiatedByID = c.Int(),
                        IsDelegated = c.Boolean(nullable: false),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CredentialingInfoID)
                .ForeignKey("dbo.CDUsers", t => t.InitiatedByID)
                .ForeignKey("dbo.Plans", t => t.PlanID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .Index(t => t.ProfileID)
                .Index(t => t.PlanID)
                .Index(t => t.InitiatedByID);
            
            CreateTable(
                "dbo.AppointmentSchedules",
                c => new
                    {
                        AppointmentScheduleID = c.Int(nullable: false, identity: true),
                        AppointmentDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ScheduledByID = c.Int(),
                        ScheduledDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsNotified = c.Boolean(nullable: false),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CCMReport_CCMReportID = c.Int(),
                        PlanCCMDetail_PlanCCMDetailID = c.Int(),
                        CredentialingInfo_CredentialingInfoID = c.Int(),
                    })
                .PrimaryKey(t => t.AppointmentScheduleID)
                .ForeignKey("dbo.CCMReports", t => t.CCMReport_CCMReportID)
                .ForeignKey("dbo.PlanCCMDetails", t => t.PlanCCMDetail_PlanCCMDetailID)
                .ForeignKey("dbo.CDUsers", t => t.ScheduledByID)
                .ForeignKey("dbo.CredentialingInfoes", t => t.CredentialingInfo_CredentialingInfoID)
                .Index(t => t.ScheduledByID)
                .Index(t => t.CCMReport_CCMReportID)
                .Index(t => t.PlanCCMDetail_PlanCCMDetailID)
                .Index(t => t.CredentialingInfo_CredentialingInfoID);
            
            CreateTable(
                "dbo.CCMReports",
                c => new
                    {
                        CCMReportID = c.Int(nullable: false, identity: true),
                        ReportByID = c.Int(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CCMReportID)
                .ForeignKey("dbo.CDUsers", t => t.ReportByID)
                .Index(t => t.ReportByID);
            
            CreateTable(
                "dbo.OtherDocuments",
                c => new
                    {
                        OtherDocumentID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsPrivate = c.Boolean(nullable: false),
                        DocumentPath = c.String(),
                        ModifiedBy = c.String(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Profile_ProfileID = c.Int(),
                    })
                .PrimaryKey(t => t.OtherDocumentID)
                .ForeignKey("dbo.Profiles", t => t.Profile_ProfileID)
                .Index(t => t.Profile_ProfileID);
            
            CreateTable(
                "dbo.OtherDocumentHistories",
                c => new
                    {
                        OtherDocumentHistoryID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsPrivate = c.Boolean(nullable: false),
                        DocumentPath = c.String(),
                        ModifiedBy = c.String(),
                        HistoryStatus = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OtherDocument_OtherDocumentID = c.Int(),
                    })
                .PrimaryKey(t => t.OtherDocumentHistoryID)
                .ForeignKey("dbo.OtherDocuments", t => t.OtherDocument_OtherDocumentID)
                .Index(t => t.OtherDocument_OtherDocumentID);
            
            CreateTable(
                "dbo.PlanCCMDetails",
                c => new
                    {
                        PlanCCMDetailID = c.Int(nullable: false, identity: true),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PlanCCMDetailID);
            
            CreateTable(
                "dbo.PlanPSVDetails",
                c => new
                    {
                        PlanPSVDetailID = c.Int(nullable: false, identity: true),
                        PSVDetailID = c.Int(),
                        PlanID = c.Int(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        AppointmentSchedule_AppointmentScheduleID = c.Int(),
                    })
                .PrimaryKey(t => t.PlanPSVDetailID)
                .ForeignKey("dbo.Plans", t => t.PlanID)
                .ForeignKey("dbo.PSVDetails", t => t.PSVDetailID)
                .ForeignKey("dbo.AppointmentSchedules", t => t.AppointmentSchedule_AppointmentScheduleID)
                .Index(t => t.PSVDetailID)
                .Index(t => t.PlanID)
                .Index(t => t.AppointmentSchedule_AppointmentScheduleID);
            
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        PlanID = c.Int(nullable: false, identity: true),
                        PlanCode = c.String(),
                        PlanName = c.String(),
                        PlanDescription = c.String(),
                        IsDelegated = c.Boolean(nullable: false),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PlanID);
            
            CreateTable(
                "dbo.PlanAddresses",
                c => new
                    {
                        PlanAddressID = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        Appartment = c.String(),
                        State = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        County = c.String(),
                        ZipCode = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        Plan_PlanID = c.Int(),
                        PlanLOB_PlanLOBID = c.Int(),
                    })
                .PrimaryKey(t => t.PlanAddressID)
                .ForeignKey("dbo.Plans", t => t.Plan_PlanID)
                .ForeignKey("dbo.PlanLOBs", t => t.PlanLOB_PlanLOBID)
                .Index(t => t.Plan_PlanID)
                .Index(t => t.PlanLOB_PlanLOBID);
            
            CreateTable(
                "dbo.PlanLOBs",
                c => new
                    {
                        PlanLOBID = c.Int(nullable: false, identity: true),
                        PlanID = c.Int(nullable: false),
                        LOBID = c.Int(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PlanLOBID)
                .ForeignKey("dbo.LOBs", t => t.LOBID, cascadeDelete: true)
                .ForeignKey("dbo.Plans", t => t.PlanID, cascadeDelete: true)
                .Index(t => t.PlanID)
                .Index(t => t.LOBID);
            
            CreateTable(
                "dbo.LOBs",
                c => new
                    {
                        LOBID = c.Int(nullable: false, identity: true),
                        LOBCode = c.String(),
                        LOBName = c.String(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LOBID);
            
            CreateTable(
                "dbo.SubPlans",
                c => new
                    {
                        SubPlanId = c.Int(nullable: false, identity: true),
                        SubPlanCode = c.String(),
                        SubPlanDescription = c.String(),
                        SubPlanName = c.String(),
                        Status = c.Int(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        PlanLOB_PlanLOBID = c.Int(),
                        LOBDetail_LOBDetailID = c.Int(),
                    })
                .PrimaryKey(t => t.SubPlanId)
                .ForeignKey("dbo.PlanLOBs", t => t.PlanLOB_PlanLOBID)
                .ForeignKey("dbo.LOBDetails", t => t.LOBDetail_LOBDetailID)
                .Index(t => t.PlanLOB_PlanLOBID)
                .Index(t => t.LOBDetail_LOBDetailID);
            
            CreateTable(
                "dbo.PSVDetails",
                c => new
                    {
                        PSVDetailID = c.Int(nullable: false, identity: true),
                        ProfileID = c.Int(),
                        VerificationStatus = c.String(),
                        Remark = c.String(),
                        VerificationSource = c.String(),
                        VerificationDocumentPath = c.String(),
                        LastVerifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DataVerified = c.String(),
                        PSVMasterID = c.Int(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PSVDetailID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .ForeignKey("dbo.PSVMasters", t => t.PSVMasterID)
                .Index(t => t.ProfileID)
                .Index(t => t.PSVMasterID);
            
            CreateTable(
                "dbo.PSVDetailHistories",
                c => new
                    {
                        PSVDetailHistoryID = c.Int(nullable: false, identity: true),
                        ProfileID = c.Int(),
                        VerificationStatus = c.String(),
                        Remark = c.String(),
                        VerificationSource = c.String(),
                        VerificationDocumentPath = c.String(),
                        LastVerifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DataVerified = c.String(),
                        PSVMasterID = c.Int(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PSVDetail_PSVDetailID = c.Int(),
                    })
                .PrimaryKey(t => t.PSVDetailHistoryID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .ForeignKey("dbo.PSVMasters", t => t.PSVMasterID)
                .ForeignKey("dbo.PSVDetails", t => t.PSVDetail_PSVDetailID)
                .Index(t => t.ProfileID)
                .Index(t => t.PSVMasterID)
                .Index(t => t.PSVDetail_PSVDetailID);
            
            CreateTable(
                "dbo.PSVMasters",
                c => new
                    {
                        PSVMasterID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Code = c.String(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PSVMasterID);
            
            CreateTable(
                "dbo.CredentialingLogs",
                c => new
                    {
                        CredentialingLogID = c.Int(nullable: false, identity: true),
                        Credentialing = c.String(),
                        LastModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CredentialingAppointmentDetail_CredentialingAppointmentDetailID = c.Int(),
                        CredentialingVerificationInfo_CredentialingVerificationInfoId = c.Int(),
                        CredentialingInfo_CredentialingInfoID = c.Int(),
                    })
                .PrimaryKey(t => t.CredentialingLogID)
                .ForeignKey("dbo.CredentialingAppointmentDetails", t => t.CredentialingAppointmentDetail_CredentialingAppointmentDetailID)
                .ForeignKey("dbo.CredentialingVerificationInfoes", t => t.CredentialingVerificationInfo_CredentialingVerificationInfoId)
                .ForeignKey("dbo.CredentialingInfoes", t => t.CredentialingInfo_CredentialingInfoID)
                .Index(t => t.CredentialingAppointmentDetail_CredentialingAppointmentDetailID)
                .Index(t => t.CredentialingVerificationInfo_CredentialingVerificationInfoId)
                .Index(t => t.CredentialingInfo_CredentialingInfoID);
            
            CreateTable(
                "dbo.CredentialingActivityLogs",
                c => new
                    {
                        CredentialingActivityLogID = c.Int(nullable: false, identity: true),
                        ActivityByID = c.Int(),
                        ActivityStatus = c.String(),
                        Activity = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CredentialingLog_CredentialingLogID = c.Int(),
                    })
                .PrimaryKey(t => t.CredentialingActivityLogID)
                .ForeignKey("dbo.CDUsers", t => t.ActivityByID)
                .ForeignKey("dbo.CredentialingLogs", t => t.CredentialingLog_CredentialingLogID)
                .Index(t => t.ActivityByID)
                .Index(t => t.CredentialingLog_CredentialingLogID);
            
            CreateTable(
                "dbo.CredentialingAppointmentDetails",
                c => new
                    {
                        CredentialingAppointmentDetailID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        ProviderType = c.String(),
                        SpecialtyID = c.Int(),
                        BoardCertified = c.String(),
                        RemarksForBoardCertification = c.String(),
                        HospitalPrivileges = c.String(),
                        RemarksForHospitalPrivileges = c.String(),
                        GapsInPractice = c.String(),
                        RemarksForGapsInPractice = c.String(),
                        CleanLicense = c.String(),
                        RemarksForCleanLicense = c.String(),
                        NPDBIssue = c.String(),
                        RemarksForNPDBIssue = c.String(),
                        MalpracticeIssue = c.String(),
                        RemarksForMalpracticeIssue = c.String(),
                        YearsInPractice = c.String(),
                        SiteVisitRequired = c.String(),
                        RemarksForSiteVisitRequired = c.String(),
                        RecommendedLevel = c.String(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CredentialingAppointmentResult_CredentialingAppointmentResultID = c.Int(),
                        CredentialingAppointmentSchedule_CredentialingAppointmentScheduleID = c.Int(),
                    })
                .PrimaryKey(t => t.CredentialingAppointmentDetailID)
                .ForeignKey("dbo.CredentialingAppointmentResults", t => t.CredentialingAppointmentResult_CredentialingAppointmentResultID)
                .ForeignKey("dbo.CredentialingAppointmentSchedules", t => t.CredentialingAppointmentSchedule_CredentialingAppointmentScheduleID)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyID)
                .Index(t => t.SpecialtyID)
                .Index(t => t.CredentialingAppointmentResult_CredentialingAppointmentResultID)
                .Index(t => t.CredentialingAppointmentSchedule_CredentialingAppointmentScheduleID);
            
            CreateTable(
                "dbo.CredentialingAppointmentResults",
                c => new
                    {
                        CredentialingAppointmentResultID = c.Int(nullable: false, identity: true),
                        SignaturePath = c.String(),
                        SignedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SignedByID = c.Int(),
                        Level = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CredentialingAppointmentResultID)
                .ForeignKey("dbo.CDUsers", t => t.SignedByID)
                .Index(t => t.SignedByID);
            
            CreateTable(
                "dbo.CredentialingAppointmentSchedules",
                c => new
                    {
                        CredentialingAppointmentScheduleID = c.Int(nullable: false, identity: true),
                        AppointmentDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AppointmentSetByID = c.Int(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CredentialingAppointmentScheduleID)
                .ForeignKey("dbo.CDUsers", t => t.AppointmentSetByID)
                .Index(t => t.AppointmentSetByID);
            
            CreateTable(
                "dbo.CredentialingCoveringPhysicians",
                c => new
                    {
                        CredentialingCoveringPhysicianID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CredentialingAppointmentDetail_CredentialingAppointmentDetailID = c.Int(),
                    })
                .PrimaryKey(t => t.CredentialingCoveringPhysicianID)
                .ForeignKey("dbo.CredentialingAppointmentDetails", t => t.CredentialingAppointmentDetail_CredentialingAppointmentDetailID)
                .Index(t => t.CredentialingAppointmentDetail_CredentialingAppointmentDetailID);
            
            CreateTable(
                "dbo.CredentialingVerificationInfoes",
                c => new
                    {
                        CredentialingVerificationInfoId = c.Int(nullable: false, identity: true),
                        CredentialingVerificationStartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CredentialingVerificationEndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ProfileVerificationInfoId = c.Int(),
                        VerifiedById = c.Int(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CredentialingInfo_CredentialingInfoID = c.Int(),
                    })
                .PrimaryKey(t => t.CredentialingVerificationInfoId)
                .ForeignKey("dbo.ProfileVerificationInfoes", t => t.ProfileVerificationInfoId)
                .ForeignKey("dbo.CDUsers", t => t.VerifiedById)
                .ForeignKey("dbo.CredentialingInfoes", t => t.CredentialingInfo_CredentialingInfoID)
                .Index(t => t.ProfileVerificationInfoId)
                .Index(t => t.VerifiedById)
                .Index(t => t.CredentialingInfo_CredentialingInfoID);
            
            CreateTable(
                "dbo.ProfileVerificationInfoes",
                c => new
                    {
                        ProfileVerificationInfoId = c.Int(nullable: false, identity: true),
                        ProfileID = c.Int(),
                        ProfileVerificationStatus = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ProfileVerificationInfoId)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .Index(t => t.ProfileID);
            
            CreateTable(
                "dbo.ProfileVerificationDetails",
                c => new
                    {
                        ProfileVerificationDetailId = c.Int(nullable: false, identity: true),
                        ProfileVerificationParameterId = c.Int(),
                        VerificationResultId = c.Int(),
                        VerificationData = c.String(),
                        VerifiedById = c.Int(),
                        VerificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ProfileVerificationInfo_ProfileVerificationInfoId = c.Int(),
                    })
                .PrimaryKey(t => t.ProfileVerificationDetailId)
                .ForeignKey("dbo.ProfileVerificationParameters", t => t.ProfileVerificationParameterId)
                .ForeignKey("dbo.VerificationResults", t => t.VerificationResultId)
                .ForeignKey("dbo.CDUsers", t => t.VerifiedById)
                .ForeignKey("dbo.ProfileVerificationInfoes", t => t.ProfileVerificationInfo_ProfileVerificationInfoId)
                .Index(t => t.ProfileVerificationParameterId)
                .Index(t => t.VerificationResultId)
                .Index(t => t.VerifiedById)
                .Index(t => t.ProfileVerificationInfo_ProfileVerificationInfoId);
            
            CreateTable(
                "dbo.ProfileVerificationParameters",
                c => new
                    {
                        ProfileVerificationParameterId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Code = c.String(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ProfileVerificationParameterId);
            
            CreateTable(
                "dbo.VerificationResults",
                c => new
                    {
                        VerificationResultId = c.Int(nullable: false, identity: true),
                        Remark = c.String(),
                        Source = c.String(),
                        VerificationDocumentPath = c.String(),
                        VerificationResultStatus = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.VerificationResultId);
            
            CreateTable(
                "dbo.LoadedContracts",
                c => new
                    {
                        LoadedContractID = c.Int(nullable: false, identity: true),
                        LoadedByID = c.Int(),
                        LoadedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Credentialing = c.String(),
                        BusinessEntityID = c.Int(nullable: false),
                        LOBID = c.Int(nullable: false),
                        SpecialtyID = c.Int(nullable: false),
                        CredentialingRequestStatus = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Docket_DocketID = c.Int(),
                        Location_LoadedLocationID = c.Int(),
                        PlanReport_PlanReportID = c.Int(),
                        CredentialingInfo_CredentialingInfoID = c.Int(),
                    })
                .PrimaryKey(t => t.LoadedContractID)
                .ForeignKey("dbo.OrganizationGroups", t => t.BusinessEntityID, cascadeDelete: true)
                .ForeignKey("dbo.Dockets", t => t.Docket_DocketID)
                .ForeignKey("dbo.CDUsers", t => t.LoadedByID)
                .ForeignKey("dbo.LOBs", t => t.LOBID, cascadeDelete: true)
                .ForeignKey("dbo.LoadedLocations", t => t.Location_LoadedLocationID)
                .ForeignKey("dbo.PlanReports", t => t.PlanReport_PlanReportID)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyID, cascadeDelete: true)
                .ForeignKey("dbo.CredentialingInfoes", t => t.CredentialingInfo_CredentialingInfoID)
                .Index(t => t.LoadedByID)
                .Index(t => t.BusinessEntityID)
                .Index(t => t.LOBID)
                .Index(t => t.SpecialtyID)
                .Index(t => t.Docket_DocketID)
                .Index(t => t.Location_LoadedLocationID)
                .Index(t => t.PlanReport_PlanReportID)
                .Index(t => t.CredentialingInfo_CredentialingInfoID);
            
            CreateTable(
                "dbo.Dockets",
                c => new
                    {
                        DocketID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DocketID);
            
            CreateTable(
                "dbo.ApplicationForms",
                c => new
                    {
                        ApplicationFormID = c.Int(nullable: false, identity: true),
                        ApplicationFormPath = c.String(),
                        CreatedByID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Docket_DocketID = c.Int(),
                    })
                .PrimaryKey(t => t.ApplicationFormID)
                .ForeignKey("dbo.CDUsers", t => t.CreatedByID)
                .ForeignKey("dbo.Dockets", t => t.Docket_DocketID)
                .Index(t => t.CreatedByID)
                .Index(t => t.Docket_DocketID);
            
            CreateTable(
                "dbo.LoadedDocuments",
                c => new
                    {
                        LoadedDocumentID = c.Int(nullable: false, identity: true),
                        LoadedDocumentName = c.String(),
                        LoadedDocumentPath = c.String(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Docket_DocketID = c.Int(),
                    })
                .PrimaryKey(t => t.LoadedDocumentID)
                .ForeignKey("dbo.Dockets", t => t.Docket_DocketID)
                .Index(t => t.Docket_DocketID);
            
            CreateTable(
                "dbo.LoadedContractHistories",
                c => new
                    {
                        LoadedContractHistoryID = c.Int(nullable: false, identity: true),
                        LoadedByID = c.Int(),
                        LoadedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Credentialing = c.String(),
                        CredentialingType = c.Int(),
                        BusinessEntityID = c.Int(nullable: false),
                        LOBID = c.Int(nullable: false),
                        SpecialtyID = c.Int(nullable: false),
                        CredentialingRequestStatus = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Docket_DocketID = c.Int(),
                        Location_LoadedLocationID = c.Int(),
                        PlanReportHistory_PlanReportHistoryID = c.Int(),
                        LoadedContract_LoadedContractID = c.Int(),
                    })
                .PrimaryKey(t => t.LoadedContractHistoryID)
                .ForeignKey("dbo.OrganizationGroups", t => t.BusinessEntityID, cascadeDelete: true)
                .ForeignKey("dbo.Dockets", t => t.Docket_DocketID)
                .ForeignKey("dbo.CDUsers", t => t.LoadedByID)
                .ForeignKey("dbo.LOBs", t => t.LOBID, cascadeDelete: true)
                .ForeignKey("dbo.LoadedLocations", t => t.Location_LoadedLocationID)
                .ForeignKey("dbo.PlanReportHistories", t => t.PlanReportHistory_PlanReportHistoryID)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyID, cascadeDelete: true)
                .ForeignKey("dbo.LoadedContracts", t => t.LoadedContract_LoadedContractID)
                .Index(t => t.LoadedByID)
                .Index(t => t.BusinessEntityID)
                .Index(t => t.LOBID)
                .Index(t => t.SpecialtyID)
                .Index(t => t.Docket_DocketID)
                .Index(t => t.Location_LoadedLocationID)
                .Index(t => t.PlanReportHistory_PlanReportHistoryID)
                .Index(t => t.LoadedContract_LoadedContractID);
            
            CreateTable(
                "dbo.LoadedLocations",
                c => new
                    {
                        LoadedLocationID = c.Int(nullable: false, identity: true),
                        Building = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        County = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.LoadedLocationID);
            
            CreateTable(
                "dbo.PlanReportHistories",
                c => new
                    {
                        PlanReportHistoryID = c.Int(nullable: false, identity: true),
                        ProviderID = c.String(),
                        GroupID = c.String(),
                        InitiatedByID = c.Int(),
                        InitiatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CredentialedByID = c.Int(),
                        CredentialedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TerminationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PlanReport_PlanReportID = c.Int(),
                    })
                .PrimaryKey(t => t.PlanReportHistoryID)
                .ForeignKey("dbo.CDUsers", t => t.CredentialedByID)
                .ForeignKey("dbo.CDUsers", t => t.InitiatedByID)
                .ForeignKey("dbo.PlanReports", t => t.PlanReport_PlanReportID)
                .Index(t => t.InitiatedByID)
                .Index(t => t.CredentialedByID)
                .Index(t => t.PlanReport_PlanReportID);
            
            CreateTable(
                "dbo.PlanReports",
                c => new
                    {
                        PlanReportID = c.Int(nullable: false, identity: true),
                        ProviderID = c.String(),
                        GroupID = c.String(),
                        InitiatedByID = c.Int(),
                        InitiatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CredentialedByID = c.Int(),
                        CredentialedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TerminationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PlanReportID)
                .ForeignKey("dbo.CDUsers", t => t.CredentialedByID)
                .ForeignKey("dbo.CDUsers", t => t.InitiatedByID)
                .Index(t => t.InitiatedByID)
                .Index(t => t.CredentialedByID);
            
            CreateTable(
                "dbo.PlanLOBAddresses",
                c => new
                    {
                        PlanLOBAddressID = c.Int(nullable: false, identity: true),
                        PlanLOBID = c.Int(nullable: false),
                        PlanAddressID = c.Int(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PlanLOBAddressID)
                .ForeignKey("dbo.PlanAddresses", t => t.PlanAddressID, cascadeDelete: true)
                .ForeignKey("dbo.PlanLOBs", t => t.PlanLOBID, cascadeDelete: true)
                .Index(t => t.PlanLOBID)
                .Index(t => t.PlanAddressID);
            
            CreateTable(
                "dbo.LobBEs",
                c => new
                    {
                        LobBEID = c.Int(nullable: false, identity: true),
                        LOBID = c.Int(nullable: false),
                        OrganizationGroupID = c.Int(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        LOBDetail_LOBDetailID = c.Int(),
                    })
                .PrimaryKey(t => t.LobBEID)
                .ForeignKey("dbo.OrganizationGroups", t => t.OrganizationGroupID, cascadeDelete: true)
                .ForeignKey("dbo.LOBs", t => t.LOBID, cascadeDelete: true)
                .ForeignKey("dbo.LOBDetails", t => t.LOBDetail_LOBDetailID)
                .Index(t => t.LOBID)
                .Index(t => t.OrganizationGroupID)
                .Index(t => t.LOBDetail_LOBDetailID);
            
            CreateTable(
                "dbo.PlanLOBContacts",
                c => new
                    {
                        PlanLOBContactID = c.Int(nullable: false, identity: true),
                        PlanLOBID = c.Int(nullable: false),
                        PlanContactDetailID = c.Int(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PlanLOBContactID)
                .ForeignKey("dbo.PlanContactDetails", t => t.PlanContactDetailID, cascadeDelete: true)
                .ForeignKey("dbo.PlanLOBs", t => t.PlanLOBID, cascadeDelete: true)
                .Index(t => t.PlanLOBID)
                .Index(t => t.PlanContactDetailID);
            
            CreateTable(
                "dbo.LOBDetails",
                c => new
                    {
                        LOBDetailID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LOBDetailID);
            
            CreateTable(
                "dbo.PlanBEs",
                c => new
                    {
                        PlanBEID = c.Int(nullable: false, identity: true),
                        PlanID = c.Int(nullable: false),
                        OrganizationGroupID = c.Int(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PlanBEID)
                .ForeignKey("dbo.OrganizationGroups", t => t.OrganizationGroupID, cascadeDelete: true)
                .ForeignKey("dbo.Plans", t => t.PlanID, cascadeDelete: true)
                .Index(t => t.PlanID)
                .Index(t => t.OrganizationGroupID);
            
            CreateTable(
                "dbo.PlanContracts",
                c => new
                    {
                        PlanContractID = c.Int(nullable: false, identity: true),
                        PlanLOBID = c.Int(nullable: false),
                        OrganizationGroupID = c.Int(nullable: false),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PlanContractID)
                .ForeignKey("dbo.OrganizationGroups", t => t.OrganizationGroupID, cascadeDelete: true)
                .ForeignKey("dbo.PlanLOBs", t => t.PlanLOBID, cascadeDelete: true)
                .Index(t => t.PlanLOBID)
                .Index(t => t.OrganizationGroupID);
            
            CreateTable(
                "dbo.PlanContractDetails",
                c => new
                    {
                        PlanContractDetailID = c.Int(nullable: false, identity: true),
                        LOBID = c.Int(nullable: false),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false),
                        AddressDetail_PlanLOBAddressID = c.Int(),
                        ContactDetail_PlanLOBContactID = c.Int(),
                    })
                .PrimaryKey(t => t.PlanContractDetailID)
                .ForeignKey("dbo.PlanLOBAddresses", t => t.AddressDetail_PlanLOBAddressID)
                .ForeignKey("dbo.PlanLOBContacts", t => t.ContactDetail_PlanLOBContactID)
                .ForeignKey("dbo.LOBs", t => t.LOBID, cascadeDelete: true)
                .Index(t => t.LOBID)
                .Index(t => t.AddressDetail_PlanLOBAddressID)
                .Index(t => t.ContactDetail_PlanLOBContactID);
            
            CreateTable(
                "dbo.PlanContractBEMappings",
                c => new
                    {
                        PlanContractBEMappingId = c.Int(nullable: false, identity: true),
                        LOBID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                        PlanContractDetail_PlanContractDetailID = c.Int(),
                    })
                .PrimaryKey(t => t.PlanContractBEMappingId)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.LOBs", t => t.LOBID, cascadeDelete: true)
                .ForeignKey("dbo.PlanContractDetails", t => t.PlanContractDetail_PlanContractDetailID)
                .Index(t => t.LOBID)
                .Index(t => t.GroupID)
                .Index(t => t.PlanContractDetail_PlanContractDetailID);
            
            CreateTable(
                "dbo.ProfileUpdatesTrackers",
                c => new
                    {
                        ProfileUpdatesTrackerId = c.Int(nullable: false, identity: true),
                        oldData = c.String(),
                        NewData = c.String(),
                        NewConvertedData = c.String(),
                        Section = c.String(),
                        SubSection = c.String(),
                        Url = c.String(),
                        ApprovalStatus = c.String(),
                        Modification = c.String(),
                        RejectionReason = c.String(),
                        ProfileId = c.Int(nullable: false),
                        LastModifiedBy = c.Int(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ProfileUpdatesTrackerId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            AddColumn("dbo.PracticeProviderSpecialties", "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID", c => c.Int());
            AddColumn("dbo.PracticeProviderTypes", "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID", c => c.Int());
            AddPrimaryKey("dbo.ProfileDisclosureQuestionQuestions", new[] { "ProfileDisclosureQuestion_ProfileDisclosureQuestionID", "Question_QuestionID" });
            CreateIndex("dbo.PracticeProviderSpecialties", "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID");
            CreateIndex("dbo.PracticeProviderTypes", "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID");
            AddForeignKey("dbo.PracticeProviderSpecialties", "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID", "dbo.CredentialingCoveringPhysicians", "CredentialingCoveringPhysicianID");
            AddForeignKey("dbo.PracticeProviderTypes", "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID", "dbo.CredentialingCoveringPhysicians", "CredentialingCoveringPhysicianID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileUpdatesTrackers", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.PlanContractBEMappings", "PlanContractDetail_PlanContractDetailID", "dbo.PlanContractDetails");
            DropForeignKey("dbo.PlanContractBEMappings", "LOBID", "dbo.LOBs");
            DropForeignKey("dbo.PlanContractBEMappings", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.PlanContractDetails", "LOBID", "dbo.LOBs");
            DropForeignKey("dbo.PlanContractDetails", "ContactDetail_PlanLOBContactID", "dbo.PlanLOBContacts");
            DropForeignKey("dbo.PlanContractDetails", "AddressDetail_PlanLOBAddressID", "dbo.PlanLOBAddresses");
            DropForeignKey("dbo.PlanContracts", "PlanLOBID", "dbo.PlanLOBs");
            DropForeignKey("dbo.PlanContracts", "OrganizationGroupID", "dbo.OrganizationGroups");
            DropForeignKey("dbo.PlanBEs", "PlanID", "dbo.Plans");
            DropForeignKey("dbo.PlanBEs", "OrganizationGroupID", "dbo.OrganizationGroups");
            DropForeignKey("dbo.SubPlans", "LOBDetail_LOBDetailID", "dbo.LOBDetails");
            DropForeignKey("dbo.LobBEs", "LOBDetail_LOBDetailID", "dbo.LOBDetails");
            DropForeignKey("dbo.PlanLOBContacts", "PlanLOBID", "dbo.PlanLOBs");
            DropForeignKey("dbo.PlanLOBContacts", "PlanContactDetailID", "dbo.PlanContactDetails");
            DropForeignKey("dbo.LobBEs", "LOBID", "dbo.LOBs");
            DropForeignKey("dbo.LobBEs", "OrganizationGroupID", "dbo.OrganizationGroups");
            DropForeignKey("dbo.PlanLOBAddresses", "PlanLOBID", "dbo.PlanLOBs");
            DropForeignKey("dbo.PlanLOBAddresses", "PlanAddressID", "dbo.PlanAddresses");
            DropForeignKey("dbo.CredentialingInfoes", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.CredentialingInfoes", "PlanID", "dbo.Plans");
            DropForeignKey("dbo.LoadedContracts", "CredentialingInfo_CredentialingInfoID", "dbo.CredentialingInfoes");
            DropForeignKey("dbo.LoadedContracts", "SpecialtyID", "dbo.Specialties");
            DropForeignKey("dbo.LoadedContracts", "PlanReport_PlanReportID", "dbo.PlanReports");
            DropForeignKey("dbo.PlanReportHistories", "PlanReport_PlanReportID", "dbo.PlanReports");
            DropForeignKey("dbo.PlanReports", "InitiatedByID", "dbo.CDUsers");
            DropForeignKey("dbo.PlanReports", "CredentialedByID", "dbo.CDUsers");
            DropForeignKey("dbo.LoadedContracts", "Location_LoadedLocationID", "dbo.LoadedLocations");
            DropForeignKey("dbo.LoadedContracts", "LOBID", "dbo.LOBs");
            DropForeignKey("dbo.LoadedContracts", "LoadedByID", "dbo.CDUsers");
            DropForeignKey("dbo.LoadedContractHistories", "LoadedContract_LoadedContractID", "dbo.LoadedContracts");
            DropForeignKey("dbo.LoadedContractHistories", "SpecialtyID", "dbo.Specialties");
            DropForeignKey("dbo.LoadedContractHistories", "PlanReportHistory_PlanReportHistoryID", "dbo.PlanReportHistories");
            DropForeignKey("dbo.PlanReportHistories", "InitiatedByID", "dbo.CDUsers");
            DropForeignKey("dbo.PlanReportHistories", "CredentialedByID", "dbo.CDUsers");
            DropForeignKey("dbo.LoadedContractHistories", "Location_LoadedLocationID", "dbo.LoadedLocations");
            DropForeignKey("dbo.LoadedContractHistories", "LOBID", "dbo.LOBs");
            DropForeignKey("dbo.LoadedContractHistories", "LoadedByID", "dbo.CDUsers");
            DropForeignKey("dbo.LoadedContractHistories", "Docket_DocketID", "dbo.Dockets");
            DropForeignKey("dbo.LoadedContractHistories", "BusinessEntityID", "dbo.OrganizationGroups");
            DropForeignKey("dbo.LoadedContracts", "Docket_DocketID", "dbo.Dockets");
            DropForeignKey("dbo.LoadedDocuments", "Docket_DocketID", "dbo.Dockets");
            DropForeignKey("dbo.ApplicationForms", "Docket_DocketID", "dbo.Dockets");
            DropForeignKey("dbo.ApplicationForms", "CreatedByID", "dbo.CDUsers");
            DropForeignKey("dbo.LoadedContracts", "BusinessEntityID", "dbo.OrganizationGroups");
            DropForeignKey("dbo.CredentialingInfoes", "InitiatedByID", "dbo.CDUsers");
            DropForeignKey("dbo.CredentialingVerificationInfoes", "CredentialingInfo_CredentialingInfoID", "dbo.CredentialingInfoes");
            DropForeignKey("dbo.CredentialingLogs", "CredentialingInfo_CredentialingInfoID", "dbo.CredentialingInfoes");
            DropForeignKey("dbo.CredentialingLogs", "CredentialingVerificationInfo_CredentialingVerificationInfoId", "dbo.CredentialingVerificationInfoes");
            DropForeignKey("dbo.CredentialingVerificationInfoes", "VerifiedById", "dbo.CDUsers");
            DropForeignKey("dbo.CredentialingVerificationInfoes", "ProfileVerificationInfoId", "dbo.ProfileVerificationInfoes");
            DropForeignKey("dbo.ProfileVerificationDetails", "ProfileVerificationInfo_ProfileVerificationInfoId", "dbo.ProfileVerificationInfoes");
            DropForeignKey("dbo.ProfileVerificationDetails", "VerifiedById", "dbo.CDUsers");
            DropForeignKey("dbo.ProfileVerificationDetails", "VerificationResultId", "dbo.VerificationResults");
            DropForeignKey("dbo.ProfileVerificationDetails", "ProfileVerificationParameterId", "dbo.ProfileVerificationParameters");
            DropForeignKey("dbo.ProfileVerificationInfoes", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.CredentialingLogs", "CredentialingAppointmentDetail_CredentialingAppointmentDetailID", "dbo.CredentialingAppointmentDetails");
            DropForeignKey("dbo.CredentialingAppointmentDetails", "SpecialtyID", "dbo.Specialties");
            DropForeignKey("dbo.CredentialingCoveringPhysicians", "CredentialingAppointmentDetail_CredentialingAppointmentDetailID", "dbo.CredentialingAppointmentDetails");
            DropForeignKey("dbo.PracticeProviderTypes", "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID", "dbo.CredentialingCoveringPhysicians");
            DropForeignKey("dbo.PracticeProviderSpecialties", "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID", "dbo.CredentialingCoveringPhysicians");
            DropForeignKey("dbo.CredentialingAppointmentDetails", "CredentialingAppointmentSchedule_CredentialingAppointmentScheduleID", "dbo.CredentialingAppointmentSchedules");
            DropForeignKey("dbo.CredentialingAppointmentSchedules", "AppointmentSetByID", "dbo.CDUsers");
            DropForeignKey("dbo.CredentialingAppointmentDetails", "CredentialingAppointmentResult_CredentialingAppointmentResultID", "dbo.CredentialingAppointmentResults");
            DropForeignKey("dbo.CredentialingAppointmentResults", "SignedByID", "dbo.CDUsers");
            DropForeignKey("dbo.CredentialingActivityLogs", "CredentialingLog_CredentialingLogID", "dbo.CredentialingLogs");
            DropForeignKey("dbo.CredentialingActivityLogs", "ActivityByID", "dbo.CDUsers");
            DropForeignKey("dbo.AppointmentSchedules", "CredentialingInfo_CredentialingInfoID", "dbo.CredentialingInfoes");
            DropForeignKey("dbo.AppointmentSchedules", "ScheduledByID", "dbo.CDUsers");
            DropForeignKey("dbo.PlanPSVDetails", "AppointmentSchedule_AppointmentScheduleID", "dbo.AppointmentSchedules");
            DropForeignKey("dbo.PlanPSVDetails", "PSVDetailID", "dbo.PSVDetails");
            DropForeignKey("dbo.PSVDetails", "PSVMasterID", "dbo.PSVMasters");
            DropForeignKey("dbo.PSVDetailHistories", "PSVDetail_PSVDetailID", "dbo.PSVDetails");
            DropForeignKey("dbo.PSVDetailHistories", "PSVMasterID", "dbo.PSVMasters");
            DropForeignKey("dbo.PSVDetailHistories", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.PSVDetails", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.PlanPSVDetails", "PlanID", "dbo.Plans");
            DropForeignKey("dbo.SubPlans", "PlanLOB_PlanLOBID", "dbo.PlanLOBs");
            DropForeignKey("dbo.PlanLOBs", "PlanID", "dbo.Plans");
            DropForeignKey("dbo.PlanAddresses", "PlanLOB_PlanLOBID", "dbo.PlanLOBs");
            DropForeignKey("dbo.PlanLOBs", "LOBID", "dbo.LOBs");
            DropForeignKey("dbo.PlanContactDetails", "PlanLOB_PlanLOBID", "dbo.PlanLOBs");
            DropForeignKey("dbo.PlanAddresses", "Plan_PlanID", "dbo.Plans");
            DropForeignKey("dbo.PlanContactDetails", "Plan_PlanID", "dbo.Plans");
            DropForeignKey("dbo.AppointmentSchedules", "PlanCCMDetail_PlanCCMDetailID", "dbo.PlanCCMDetails");
            DropForeignKey("dbo.AppointmentSchedules", "CCMReport_CCMReportID", "dbo.CCMReports");
            DropForeignKey("dbo.CCMReports", "ReportByID", "dbo.CDUsers");
            DropForeignKey("dbo.OtherDocuments", "Profile_ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.OtherDocumentHistories", "OtherDocument_OtherDocumentID", "dbo.OtherDocuments");
            DropIndex("dbo.ProfileUpdatesTrackers", new[] { "ProfileId" });
            DropIndex("dbo.PlanContractBEMappings", new[] { "PlanContractDetail_PlanContractDetailID" });
            DropIndex("dbo.PlanContractBEMappings", new[] { "GroupID" });
            DropIndex("dbo.PlanContractBEMappings", new[] { "LOBID" });
            DropIndex("dbo.PlanContractDetails", new[] { "ContactDetail_PlanLOBContactID" });
            DropIndex("dbo.PlanContractDetails", new[] { "AddressDetail_PlanLOBAddressID" });
            DropIndex("dbo.PlanContractDetails", new[] { "LOBID" });
            DropIndex("dbo.PlanContracts", new[] { "OrganizationGroupID" });
            DropIndex("dbo.PlanContracts", new[] { "PlanLOBID" });
            DropIndex("dbo.PlanBEs", new[] { "OrganizationGroupID" });
            DropIndex("dbo.PlanBEs", new[] { "PlanID" });
            DropIndex("dbo.PlanLOBContacts", new[] { "PlanContactDetailID" });
            DropIndex("dbo.PlanLOBContacts", new[] { "PlanLOBID" });
            DropIndex("dbo.LobBEs", new[] { "LOBDetail_LOBDetailID" });
            DropIndex("dbo.LobBEs", new[] { "OrganizationGroupID" });
            DropIndex("dbo.LobBEs", new[] { "LOBID" });
            DropIndex("dbo.PlanLOBAddresses", new[] { "PlanAddressID" });
            DropIndex("dbo.PlanLOBAddresses", new[] { "PlanLOBID" });
            DropIndex("dbo.PlanReports", new[] { "CredentialedByID" });
            DropIndex("dbo.PlanReports", new[] { "InitiatedByID" });
            DropIndex("dbo.PlanReportHistories", new[] { "PlanReport_PlanReportID" });
            DropIndex("dbo.PlanReportHistories", new[] { "CredentialedByID" });
            DropIndex("dbo.PlanReportHistories", new[] { "InitiatedByID" });
            DropIndex("dbo.LoadedContractHistories", new[] { "LoadedContract_LoadedContractID" });
            DropIndex("dbo.LoadedContractHistories", new[] { "PlanReportHistory_PlanReportHistoryID" });
            DropIndex("dbo.LoadedContractHistories", new[] { "Location_LoadedLocationID" });
            DropIndex("dbo.LoadedContractHistories", new[] { "Docket_DocketID" });
            DropIndex("dbo.LoadedContractHistories", new[] { "SpecialtyID" });
            DropIndex("dbo.LoadedContractHistories", new[] { "LOBID" });
            DropIndex("dbo.LoadedContractHistories", new[] { "BusinessEntityID" });
            DropIndex("dbo.LoadedContractHistories", new[] { "LoadedByID" });
            DropIndex("dbo.LoadedDocuments", new[] { "Docket_DocketID" });
            DropIndex("dbo.ApplicationForms", new[] { "Docket_DocketID" });
            DropIndex("dbo.ApplicationForms", new[] { "CreatedByID" });
            DropIndex("dbo.LoadedContracts", new[] { "CredentialingInfo_CredentialingInfoID" });
            DropIndex("dbo.LoadedContracts", new[] { "PlanReport_PlanReportID" });
            DropIndex("dbo.LoadedContracts", new[] { "Location_LoadedLocationID" });
            DropIndex("dbo.LoadedContracts", new[] { "Docket_DocketID" });
            DropIndex("dbo.LoadedContracts", new[] { "SpecialtyID" });
            DropIndex("dbo.LoadedContracts", new[] { "LOBID" });
            DropIndex("dbo.LoadedContracts", new[] { "BusinessEntityID" });
            DropIndex("dbo.LoadedContracts", new[] { "LoadedByID" });
            DropIndex("dbo.ProfileVerificationDetails", new[] { "ProfileVerificationInfo_ProfileVerificationInfoId" });
            DropIndex("dbo.ProfileVerificationDetails", new[] { "VerifiedById" });
            DropIndex("dbo.ProfileVerificationDetails", new[] { "VerificationResultId" });
            DropIndex("dbo.ProfileVerificationDetails", new[] { "ProfileVerificationParameterId" });
            DropIndex("dbo.ProfileVerificationInfoes", new[] { "ProfileID" });
            DropIndex("dbo.CredentialingVerificationInfoes", new[] { "CredentialingInfo_CredentialingInfoID" });
            DropIndex("dbo.CredentialingVerificationInfoes", new[] { "VerifiedById" });
            DropIndex("dbo.CredentialingVerificationInfoes", new[] { "ProfileVerificationInfoId" });
            DropIndex("dbo.CredentialingCoveringPhysicians", new[] { "CredentialingAppointmentDetail_CredentialingAppointmentDetailID" });
            DropIndex("dbo.CredentialingAppointmentSchedules", new[] { "AppointmentSetByID" });
            DropIndex("dbo.CredentialingAppointmentResults", new[] { "SignedByID" });
            DropIndex("dbo.CredentialingAppointmentDetails", new[] { "CredentialingAppointmentSchedule_CredentialingAppointmentScheduleID" });
            DropIndex("dbo.CredentialingAppointmentDetails", new[] { "CredentialingAppointmentResult_CredentialingAppointmentResultID" });
            DropIndex("dbo.CredentialingAppointmentDetails", new[] { "SpecialtyID" });
            DropIndex("dbo.CredentialingActivityLogs", new[] { "CredentialingLog_CredentialingLogID" });
            DropIndex("dbo.CredentialingActivityLogs", new[] { "ActivityByID" });
            DropIndex("dbo.CredentialingLogs", new[] { "CredentialingInfo_CredentialingInfoID" });
            DropIndex("dbo.CredentialingLogs", new[] { "CredentialingVerificationInfo_CredentialingVerificationInfoId" });
            DropIndex("dbo.CredentialingLogs", new[] { "CredentialingAppointmentDetail_CredentialingAppointmentDetailID" });
            DropIndex("dbo.PSVDetailHistories", new[] { "PSVDetail_PSVDetailID" });
            DropIndex("dbo.PSVDetailHistories", new[] { "PSVMasterID" });
            DropIndex("dbo.PSVDetailHistories", new[] { "ProfileID" });
            DropIndex("dbo.PSVDetails", new[] { "PSVMasterID" });
            DropIndex("dbo.PSVDetails", new[] { "ProfileID" });
            DropIndex("dbo.SubPlans", new[] { "LOBDetail_LOBDetailID" });
            DropIndex("dbo.SubPlans", new[] { "PlanLOB_PlanLOBID" });
            DropIndex("dbo.PlanLOBs", new[] { "LOBID" });
            DropIndex("dbo.PlanLOBs", new[] { "PlanID" });
            DropIndex("dbo.PlanAddresses", new[] { "PlanLOB_PlanLOBID" });
            DropIndex("dbo.PlanAddresses", new[] { "Plan_PlanID" });
            DropIndex("dbo.PlanPSVDetails", new[] { "AppointmentSchedule_AppointmentScheduleID" });
            DropIndex("dbo.PlanPSVDetails", new[] { "PlanID" });
            DropIndex("dbo.PlanPSVDetails", new[] { "PSVDetailID" });
            DropIndex("dbo.PracticeProviderTypes", new[] { "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID" });
            DropIndex("dbo.PracticeProviderSpecialties", new[] { "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID" });
            DropIndex("dbo.OtherDocumentHistories", new[] { "OtherDocument_OtherDocumentID" });
            DropIndex("dbo.OtherDocuments", new[] { "Profile_ProfileID" });
            DropIndex("dbo.CCMReports", new[] { "ReportByID" });
            DropIndex("dbo.AppointmentSchedules", new[] { "CredentialingInfo_CredentialingInfoID" });
            DropIndex("dbo.AppointmentSchedules", new[] { "PlanCCMDetail_PlanCCMDetailID" });
            DropIndex("dbo.AppointmentSchedules", new[] { "CCMReport_CCMReportID" });
            DropIndex("dbo.AppointmentSchedules", new[] { "ScheduledByID" });
            DropIndex("dbo.CredentialingInfoes", new[] { "InitiatedByID" });
            DropIndex("dbo.CredentialingInfoes", new[] { "PlanID" });
            DropIndex("dbo.CredentialingInfoes", new[] { "ProfileID" });
            DropIndex("dbo.PlanContactDetails", new[] { "PlanLOB_PlanLOBID" });
            DropIndex("dbo.PlanContactDetails", new[] { "Plan_PlanID" });
            DropPrimaryKey("dbo.ProfileDisclosureQuestionQuestions");
            DropColumn("dbo.PracticeProviderTypes", "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID");
            DropColumn("dbo.PracticeProviderSpecialties", "CredentialingCoveringPhysician_CredentialingCoveringPhysicianID");
            DropTable("dbo.ProfileUpdatesTrackers");
            DropTable("dbo.PlanContractBEMappings");
            DropTable("dbo.PlanContractDetails");
            DropTable("dbo.PlanContracts");
            DropTable("dbo.PlanBEs");
            DropTable("dbo.LOBDetails");
            DropTable("dbo.PlanLOBContacts");
            DropTable("dbo.LobBEs");
            DropTable("dbo.PlanLOBAddresses");
            DropTable("dbo.PlanReports");
            DropTable("dbo.PlanReportHistories");
            DropTable("dbo.LoadedLocations");
            DropTable("dbo.LoadedContractHistories");
            DropTable("dbo.LoadedDocuments");
            DropTable("dbo.ApplicationForms");
            DropTable("dbo.Dockets");
            DropTable("dbo.LoadedContracts");
            DropTable("dbo.VerificationResults");
            DropTable("dbo.ProfileVerificationParameters");
            DropTable("dbo.ProfileVerificationDetails");
            DropTable("dbo.ProfileVerificationInfoes");
            DropTable("dbo.CredentialingVerificationInfoes");
            DropTable("dbo.CredentialingCoveringPhysicians");
            DropTable("dbo.CredentialingAppointmentSchedules");
            DropTable("dbo.CredentialingAppointmentResults");
            DropTable("dbo.CredentialingAppointmentDetails");
            DropTable("dbo.CredentialingActivityLogs");
            DropTable("dbo.CredentialingLogs");
            DropTable("dbo.PSVMasters");
            DropTable("dbo.PSVDetailHistories");
            DropTable("dbo.PSVDetails");
            DropTable("dbo.SubPlans");
            DropTable("dbo.LOBs");
            DropTable("dbo.PlanLOBs");
            DropTable("dbo.PlanAddresses");
            DropTable("dbo.Plans");
            DropTable("dbo.PlanPSVDetails");
            DropTable("dbo.PlanCCMDetails");
            DropTable("dbo.OtherDocumentHistories");
            DropTable("dbo.OtherDocuments");
            DropTable("dbo.CCMReports");
            DropTable("dbo.AppointmentSchedules");
            DropTable("dbo.CredentialingInfoes");
            DropTable("dbo.PlanContactDetails");
            AddPrimaryKey("dbo.ProfileDisclosureQuestionQuestions", new[] { "Question_QuestionID", "ProfileDisclosureQuestion_ProfileDisclosureQuestionID" });
            RenameTable(name: "dbo.ProfileDisclosureQuestionQuestions", newName: "QuestionProfileDisclosureQuestions");
        }
    }
}
