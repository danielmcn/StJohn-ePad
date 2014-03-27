namespace StJohnEPAD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        Address = c.String(),
                        TelephoneNumber = c.String(),
                        Skills = c.String(),
                        Rank = c.String(),
                        OperationalRoles = c.String(),
                        NonOperationalRoles = c.String(),
                        EmergencyContact_MemberID = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Members", t => t.EmergencyContact_MemberID)
                .Index(t => t.EmergencyContact_MemberID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        Address = c.String(),
                        TelephoneNumber = c.String(),
                        Skills = c.String(),
                        Rank = c.String(),
                        OperationalRoles = c.String(),
                        NonOperationalRoles = c.String(),
                        EmergencyContact_MemberID = c.Int(),
                    })
                .PrimaryKey(t => t.MemberID)
                .ForeignKey("dbo.Members", t => t.EmergencyContact_MemberID)
                .Index(t => t.EmergencyContact_MemberID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Members", new[] { "EmergencyContact_MemberID" });
            DropIndex("dbo.UserProfile", new[] { "EmergencyContact_MemberID" });
            DropForeignKey("dbo.Members", "EmergencyContact_MemberID", "dbo.Members");
            DropForeignKey("dbo.UserProfile", "EmergencyContact_MemberID", "dbo.Members");
            DropTable("dbo.Members");
            DropTable("dbo.UserProfile");
        }
    }
}
