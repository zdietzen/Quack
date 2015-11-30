using Quack.Core.Domain;
using System.Data.Entity;

namespace Quack.Core.Infrastructure
{
    public class QuackDbContext : DbContext
    {
        public QuackDbContext() : base("Quack")
        {
        }

        public IDbSet<Bookmark> Bookmarks { get; set; } 
        public IDbSet<Cohort> Cohorts { get; set; } 
        public IDbSet<MediaType> MediaTypes { get; set; }
        public IDbSet<Student> Students { get; set; }   
        public IDbSet<Teacher> Teachers { get; set; }
        public IDbSet<TeacherCohort> TeacherCohorts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmark>().HasKey(b => b.BookmarkId);

          

            modelBuilder.Entity<Cohort>().HasKey(c => c.CohortId);

            modelBuilder.Entity<Cohort>().HasMany(c => c.Students)
                                         .WithRequired(s => s.Cohort)
                                         .HasForeignKey(s => s.CohortId);

            modelBuilder.Entity<Cohort>().HasMany(c => c.TeacherCohorts)
                                         .WithRequired(tc => tc.Cohort)
                                         .HasForeignKey(tc => tc.CohortId);

            modelBuilder.Entity<MediaType>().HasKey(mt => mt.MediaTypeId);

            modelBuilder.Entity<MediaType>().HasMany(mt => mt.Bookmarks)
                                            .WithRequired(b => b.MediaType)
                                            .HasForeignKey(b => b.MediaTypeId);


            modelBuilder.Entity<Student>().HasKey(s => s.StudentId);

            modelBuilder.Entity<Student>().HasMany(s => s.Bookmarks)
                                          .WithRequired(b => b.Student)
                                          .HasForeignKey(b => b.StudentId);


            modelBuilder.Entity<Teacher>().HasKey(t => t.TeacherId);

            modelBuilder.Entity<Teacher>().HasMany(t => t.TeacherCohorts)
                                          .WithRequired(tc => tc.Teacher)
                                          .HasForeignKey(tc => tc.TeacherId);  


            modelBuilder.Entity<TeacherCohort>().HasKey(tc => new { tc.TeacherId, tc.CohortId });
                                               
        }
    }
}
