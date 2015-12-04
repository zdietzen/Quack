using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quack.Core.Models;

namespace Quack.Core.Domain
{
    // Represents both a Student and a Teacher.
    // You can figure out which is which by inspecting the roles of this object
    public class QuackUser : IdentityUser
    {
        public int? CohortId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsStudent => false;
        public bool IsTeacher => false;

        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<TeacherCohort> Cohorts { get; set; }

        public virtual Cohort Cohort { get; set; }

        public void Update(StudentModel student)
        {
            throw new NotImplementedException();
        }

        public void Update(TeacherModel teacher)
        {
            throw new NotImplementedException();
        }
    }
}
