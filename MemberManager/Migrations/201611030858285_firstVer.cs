namespace Medusa.Business.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstVer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        MemberName = c.String(maxLength: 50),
                        Address = c.String(maxLength: 100),
                        Birthday = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 50),
                        AccountId = c.String(maxLength: 20),
                        Password = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(maxLength: 100),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.MemberSkills",
                c => new
                    {
                        MemberId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        YearsOfExp = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberId, t.SkillId })
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        SkillName = c.String(maxLength: 50),
                        Descriptions = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.SkillId);
            
            CreateTable(
                "dbo.ProjectMembers",
                c => new
                    {
                        Project_ProjectId = c.Int(nullable: false),
                        Member_MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_ProjectId, t.Member_MemberId })
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_MemberId, cascadeDelete: true)
                .Index(t => t.Project_ProjectId)
                .Index(t => t.Member_MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.MemberSkills", "MemberId", "dbo.Members");
            DropForeignKey("dbo.ProjectMembers", "Member_MemberId", "dbo.Members");
            DropForeignKey("dbo.ProjectMembers", "Project_ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectMembers", new[] { "Member_MemberId" });
            DropIndex("dbo.ProjectMembers", new[] { "Project_ProjectId" });
            DropIndex("dbo.MemberSkills", new[] { "SkillId" });
            DropIndex("dbo.MemberSkills", new[] { "MemberId" });
            DropTable("dbo.ProjectMembers");
            DropTable("dbo.Skills");
            DropTable("dbo.MemberSkills");
            DropTable("dbo.Projects");
            DropTable("dbo.Members");
        }
    }
}
