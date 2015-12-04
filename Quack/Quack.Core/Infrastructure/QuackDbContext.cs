using Microsoft.AspNet.Identity.EntityFramework;
using Quack.Core.Domain;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Quack.Core.Infrastructure
{
    public class QuackDbContext : IdentityDbContext<QuackUser>
    {
        public QuackDbContext() : base("Quack")
        {
        }

        public IDbSet<Bookmark> Bookmarks { get; set; } 
        public IDbSet<Cohort> Cohorts { get; set; } 
        public IDbSet<MediaType> MediaTypes { get; set; }
        public IDbSet<TeacherCohort> TeacherCohorts { get; set; }
        public IDbSet<Client> Clients { get; set; }
        public IDbSet<RefreshToken> RefreshTokens { get; set; }

        public IEnumerable<QuackUser> Teachers
        {
            get
            {
                return Users.Where(u => IsUserInRole(u, "Teacher"));
            }
        }
        public IEnumerable<QuackUser> Students
        {
            get
            {
                return Users.Where(u => IsUserInRole(u, "Student"));
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmark>().HasKey(b => b.BookmarkId);

          

            modelBuilder.Entity<Cohort>().HasKey(c => c.CohortId);

            modelBuilder.Entity<Cohort>().HasMany(c => c.Students)
                                         .WithOptional(s => s.Cohort)
                                         .HasForeignKey(s => s.CohortId);

            modelBuilder.Entity<Cohort>().HasMany(c => c.TeacherCohorts)
                                         .WithRequired(tc => tc.Cohort)
                                         .HasForeignKey(tc => tc.CohortId);

            modelBuilder.Entity<QuackUser>().HasMany(qu => qu.Bookmarks)
                                            .WithRequired(b => b.Student)
                                            .HasForeignKey(b => b.QuackUserId);

            modelBuilder.Entity<QuackUser>().HasMany(qu => qu.Cohorts)
                                            .WithRequired(tc => tc.Teacher)
                                            .HasForeignKey(tc => tc.QuackUserId);

            modelBuilder.Entity<MediaType>().HasKey(mt => mt.MediaTypeId);

            modelBuilder.Entity<MediaType>().HasMany(mt => mt.Bookmarks)
                                            .WithRequired(b => b.MediaType)
                                            .HasForeignKey(b => b.MediaTypeId);


            modelBuilder.Entity<TeacherCohort>().HasKey(tc => new { tc.QuackUserId, tc.CohortId });

            modelBuilder.Entity<QuackUser>().HasEntitySetName("QuackUsers");

            base.OnModelCreating(modelBuilder);                                               
        }

        /// <summary>
        /// Give a user a role. If the role doesn't exist, this method will create one before giving it to the user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public void AddUserToRole(QuackUser user, string role)
        {
            // Try to get the role
            var dbRole = Roles.FirstOrDefault(r => r.Name == role);

            // If it doesn't exist
            if (dbRole == null)
            {
                // Add it to the database
                dbRole = Roles.Add(new IdentityRole { Name = role });
                SaveChanges();
            }
            
            // Assign the user to the role AS LONG AS the user does not already have that role
            if(!IsUserInRole(user, role))
            {
                user.Roles.Add(new IdentityUserRole { RoleId = dbRole.Id });
            }
        }

        public bool IsUserInRole(QuackUser user, string role)
        {
            var dbRole = Roles.FirstOrDefault(r => r.Name == role);

            if (dbRole == null) return false;

            return user.Roles.Any(r => r.RoleId == dbRole.Id);
        }

        public static QuackDbContext Create()
        {
            return new QuackDbContext();
        }
    }
}
