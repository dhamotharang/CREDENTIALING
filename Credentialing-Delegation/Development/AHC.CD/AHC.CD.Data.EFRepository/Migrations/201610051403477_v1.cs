namespace AHC.CD.Data.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UpdateHistories",
                c => new
                    {
                        UpdateHistoryID = c.Int(nullable: false, identity: true),
                        ProfileIDOfRecord = c.Int(nullable: false),
                        OldRecord = c.String(),
                        SectionTableID = c.Int(nullable: false),
                        SectionName = c.String(),
                        UpdatedById = c.Int(),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.UpdateHistoryID)
                .ForeignKey("dbo.CDUsers", t => t.UpdatedById)
                .Index(t => t.UpdatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UpdateHistories", "UpdatedById", "dbo.CDUsers");
            DropIndex("dbo.UpdateHistories", new[] { "UpdatedById" });
            DropTable("dbo.UpdateHistories");
        }
    }
}
