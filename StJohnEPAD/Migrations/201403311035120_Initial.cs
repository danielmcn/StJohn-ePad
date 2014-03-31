namespace StJohnEPAD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Duties",
                c => new
                    {
                        DutyID = c.Int(nullable: false, identity: true),
                        DutyName = c.String(nullable: false),
                        DutyDate = c.DateTime(nullable: false),
                        DutyStartTime = c.DateTime(nullable: false),
                        DutyEndTime = c.DateTime(nullable: false),
                        DutyLocation = c.String(),
                        DutyDescription = c.String(),
                        DutyAdditionalNotes = c.String(),
                        DutyOrganiser = c.String(),
                        DutyOrganiserPhoneNumber = c.String(),
                        DutyOrganiserEmailAddress = c.String(),
                        DutyCreator_MemberID = c.Int(),
                    })
                .PrimaryKey(t => t.DutyID)
                .ForeignKey("dbo.Members", t => t.DutyCreator_MemberID)
                .Index(t => t.DutyCreator_MemberID);
            
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
                        Duty_DutyID = c.Int(),
                    })
                .PrimaryKey(t => t.MemberID)
                .ForeignKey("dbo.Members", t => t.EmergencyContact_MemberID)
                .ForeignKey("dbo.Duties", t => t.Duty_DutyID)
                .Index(t => t.EmergencyContact_MemberID)
                .Index(t => t.Duty_DutyID);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        EquipmentID = c.Int(nullable: false, identity: true),
                        EquipmentName = c.String(nullable: false),
                        LastCheck = c.DateTime(nullable: false),
                        NextCheck = c.DateTime(nullable: false),
                        ItemDescription = c.String(),
                        LastCheckedOut_MemberID = c.Int(),
                        CurrentCheckedOut_MemberID = c.Int(),
                    })
                .PrimaryKey(t => t.EquipmentID)
                .ForeignKey("dbo.Members", t => t.LastCheckedOut_MemberID)
                .ForeignKey("dbo.Members", t => t.CurrentCheckedOut_MemberID)
                .Index(t => t.LastCheckedOut_MemberID)
                .Index(t => t.CurrentCheckedOut_MemberID);
            
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
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserProfile", new[] { "EmergencyContact_MemberID" });
            DropIndex("dbo.Equipments", new[] { "CurrentCheckedOut_MemberID" });
            DropIndex("dbo.Equipments", new[] { "LastCheckedOut_MemberID" });
            DropIndex("dbo.Members", new[] { "Duty_DutyID" });
            DropIndex("dbo.Members", new[] { "EmergencyContact_MemberID" });
            DropIndex("dbo.Duties", new[] { "DutyCreator_MemberID" });
            DropForeignKey("dbo.UserProfile", "EmergencyContact_MemberID", "dbo.Members");
            DropForeignKey("dbo.Equipments", "CurrentCheckedOut_MemberID", "dbo.Members");
            DropForeignKey("dbo.Equipments", "LastCheckedOut_MemberID", "dbo.Members");
            DropForeignKey("dbo.Members", "Duty_DutyID", "dbo.Duties");
            DropForeignKey("dbo.Members", "EmergencyContact_MemberID", "dbo.Members");
            DropForeignKey("dbo.Duties", "DutyCreator_MemberID", "dbo.Members");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Equipments");
            DropTable("dbo.Members");
            DropTable("dbo.Duties");
        }
    }
}
