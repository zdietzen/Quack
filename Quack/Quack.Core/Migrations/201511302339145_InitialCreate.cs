namespace Quack.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        BookmarkId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        MediaTypeId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Likes = c.Int(nullable: false),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.BookmarkId)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.MediaTypeId);
            
            CreateTable(
                "dbo.MediaTypes",
                c => new
                    {
                        MediaTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.MediaTypeId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        CohortId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Approved = c.Int(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Cohorts", t => t.CohortId, cascadeDelete: true)
                .Index(t => t.CohortId);
            
            CreateTable(
                "dbo.Cohorts",
                c => new
                    {
                        CohortId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CohortId);
            
            CreateTable(
                "dbo.TeacherCohorts",
                c => new
                    {
                        TeacherId = c.Int(nullable: false),
                        CohortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeacherId, t.CohortId })
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Cohorts", t => t.CohortId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.CohortId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherCohorts", "CohortId", "dbo.Cohorts");
            DropForeignKey("dbo.TeacherCohorts", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Students", "CohortId", "dbo.Cohorts");
            DropForeignKey("dbo.Bookmarks", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Bookmarks", "MediaTypeId", "dbo.MediaTypes");
            DropIndex("dbo.TeacherCohorts", new[] { "CohortId" });
            DropIndex("dbo.TeacherCohorts", new[] { "TeacherId" });
            DropIndex("dbo.Students", new[] { "CohortId" });
            DropIndex("dbo.Bookmarks", new[] { "MediaTypeId" });
            DropIndex("dbo.Bookmarks", new[] { "StudentId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.TeacherCohorts");
            DropTable("dbo.Cohorts");
            DropTable("dbo.Students");
            DropTable("dbo.MediaTypes");
            DropTable("dbo.Bookmarks");
        }
    }
}
