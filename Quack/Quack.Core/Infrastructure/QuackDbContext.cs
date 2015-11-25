using Quack.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserCohort> UserCohorts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Need help here not sure what to do with the many to many.

            modelBuilder.Entity<Bookmark>().HasKey(b => b.BookmarkId);
            modelBuilder.Entity<Bookmark>().HasMany(b => b.MediaTypes);
            //need to add StudentId an MediaTypeId foreign keys
                                          
                                       
            modelBuilder.Entity<Cohort>().HasKey(c => c.CohortId);
            modelBuilder.Entity<Cohort>().HasMany(c => c.Students);
            modelBuilder.Entity<Cohort>().HasMany(c => c.UserCohorts);

            modelBuilder.Entity<MediaType>().HasKey(mt => mt.MediaTypeId);

            modelBuilder.Entity<Student>().HasKey(s => s.StudentId);
            modelBuilder.Entity<Student>().HasMany(s => s.Bookmarks);
            //need to add CohortId foreign Key


            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().HasMany(u => u.UserCohorts);

            modelBuilder.Entity<UserCohort>();   //not sure what to do here for the many to many relationship

        }
    }
}
