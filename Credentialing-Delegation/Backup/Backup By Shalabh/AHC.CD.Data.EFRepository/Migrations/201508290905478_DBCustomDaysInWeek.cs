namespace AHC.CD.Data.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBCustomDaysInWeek : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomDaysInWeeks",
                c => new
                    {
                        CustomDaysInWeekID = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                        Status = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false),
                        EmailRecurrenceDetail_EmailRecurrenceDetailID = c.Int(),
                    })
                .PrimaryKey(t => t.CustomDaysInWeekID)
                .ForeignKey("dbo.EmailRecurrenceDetails", t => t.EmailRecurrenceDetail_EmailRecurrenceDetailID)
                .Index(t => t.EmailRecurrenceDetail_EmailRecurrenceDetailID);
            
            AddColumn("dbo.EmailInfoes", "SendingDate", c => c.DateTime());
            AddColumn("dbo.EmailRecurrenceDetails", "IsRecurrenceScheduled", c => c.String());
            DropColumn("dbo.EmailTemplates", "Action");
            DropColumn("dbo.EmailTemplates", "EmailNotificationType");
            DropColumn("dbo.EmailTemplates", "To");
            DropColumn("dbo.EmailTemplates", "CC");
            DropColumn("dbo.EmailTemplates", "BCC");
            DropColumn("dbo.EmailTemplates", "IsRecurrenceEnabled");
            DropColumn("dbo.EmailTemplates", "IntervalFactor");
            DropColumn("dbo.EmailTemplates", "RecurrenceIntervalType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmailTemplates", "RecurrenceIntervalType", c => c.String());
            AddColumn("dbo.EmailTemplates", "IntervalFactor", c => c.Int());
            AddColumn("dbo.EmailTemplates", "IsRecurrenceEnabled", c => c.String());
            AddColumn("dbo.EmailTemplates", "BCC", c => c.String());
            AddColumn("dbo.EmailTemplates", "CC", c => c.String());
            AddColumn("dbo.EmailTemplates", "To", c => c.String());
            AddColumn("dbo.EmailTemplates", "EmailNotificationType", c => c.String());
            AddColumn("dbo.EmailTemplates", "Action", c => c.String());
            DropForeignKey("dbo.CustomDaysInWeeks", "EmailRecurrenceDetail_EmailRecurrenceDetailID", "dbo.EmailRecurrenceDetails");
            DropIndex("dbo.CustomDaysInWeeks", new[] { "EmailRecurrenceDetail_EmailRecurrenceDetailID" });
            DropColumn("dbo.EmailRecurrenceDetails", "IsRecurrenceScheduled");
            DropColumn("dbo.EmailInfoes", "SendingDate");
            DropTable("dbo.CustomDaysInWeeks");
        }
    }
}
