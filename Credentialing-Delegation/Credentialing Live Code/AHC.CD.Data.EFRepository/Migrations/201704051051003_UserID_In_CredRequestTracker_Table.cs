namespace AHC.CD.Data.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserID_In_CredRequestTracker_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CredentialingRequestTrackers", "DecisionMadeBy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CredentialingRequestTrackers", "DecisionMadeBy");
        }
    }
}
