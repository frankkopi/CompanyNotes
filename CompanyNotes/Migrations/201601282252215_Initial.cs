namespace CompanyNotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        CaseId = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Manager = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CaseId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstMidName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(nullable: false, maxLength: 50),
                        WorkTitleId = c.Int(nullable: false),
                        SubcontractorId = c.Int(),
                        HireDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.WorkTitles", t => t.WorkTitleId, cascadeDelete: true)
                .ForeignKey("dbo.Subcontractors", t => t.SubcontractorId)
                .Index(t => t.WorkTitleId)
                .Index(t => t.SubcontractorId);
            
            CreateTable(
                "dbo.WorkNotes",
                c => new
                    {
                        WorkNoteId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Caption = c.String(),
                        Text = c.String(),
                        CaseId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkNoteId)
                .ForeignKey("dbo.Cases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.CaseId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.WorkTitles",
                c => new
                    {
                        WorkTitleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.WorkTitleId);
            
            CreateTable(
                "dbo.Subcontractors",
                c => new
                    {
                        SubcontractorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.SubcontractorId);
            
            CreateTable(
                "dbo.Residents",
                c => new
                    {
                        ResidentId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        CaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResidentId)
                .ForeignKey("dbo.Cases", t => t.CaseId, cascadeDelete: true)
                .Index(t => t.CaseId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EmployeeCases",
                c => new
                    {
                        Employee_EmployeeId = c.Int(nullable: false),
                        Case_CaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Employee_EmployeeId, t.Case_CaseId })
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Cases", t => t.Case_CaseId, cascadeDelete: true)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.Case_CaseId);
            
            CreateTable(
                "dbo.SubcontractorCases",
                c => new
                    {
                        Subcontractor_SubcontractorId = c.Int(nullable: false),
                        Case_CaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subcontractor_SubcontractorId, t.Case_CaseId })
                .ForeignKey("dbo.Subcontractors", t => t.Subcontractor_SubcontractorId, cascadeDelete: true)
                .ForeignKey("dbo.Cases", t => t.Case_CaseId, cascadeDelete: true)
                .Index(t => t.Subcontractor_SubcontractorId)
                .Index(t => t.Case_CaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Residents", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.Employees", "SubcontractorId", "dbo.Subcontractors");
            DropForeignKey("dbo.SubcontractorCases", "Case_CaseId", "dbo.Cases");
            DropForeignKey("dbo.SubcontractorCases", "Subcontractor_SubcontractorId", "dbo.Subcontractors");
            DropForeignKey("dbo.Employees", "WorkTitleId", "dbo.WorkTitles");
            DropForeignKey("dbo.WorkNotes", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.WorkNotes", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.EmployeeCases", "Case_CaseId", "dbo.Cases");
            DropForeignKey("dbo.EmployeeCases", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Cases", "ClientId", "dbo.Clients");
            DropIndex("dbo.SubcontractorCases", new[] { "Case_CaseId" });
            DropIndex("dbo.SubcontractorCases", new[] { "Subcontractor_SubcontractorId" });
            DropIndex("dbo.EmployeeCases", new[] { "Case_CaseId" });
            DropIndex("dbo.EmployeeCases", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Residents", new[] { "CaseId" });
            DropIndex("dbo.WorkNotes", new[] { "EmployeeId" });
            DropIndex("dbo.WorkNotes", new[] { "CaseId" });
            DropIndex("dbo.Employees", new[] { "SubcontractorId" });
            DropIndex("dbo.Employees", new[] { "WorkTitleId" });
            DropIndex("dbo.Cases", new[] { "ClientId" });
            DropTable("dbo.SubcontractorCases");
            DropTable("dbo.EmployeeCases");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Residents");
            DropTable("dbo.Subcontractors");
            DropTable("dbo.WorkTitles");
            DropTable("dbo.WorkNotes");
            DropTable("dbo.Employees");
            DropTable("dbo.Clients");
            DropTable("dbo.Cases");
        }
    }
}
