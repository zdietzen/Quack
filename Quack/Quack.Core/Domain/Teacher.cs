using Quack.Core.Models;
using System.Collections.Generic;

namespace Quack.Core.Domain
{
    public class Teacher //Need to inherit IdentityUser
    {
        public int TeacherId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<TeacherCohort> TeacherCohorts { get; set; }

        public void Update(TeacherModel teacher)
        {
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Email = teacher.Email;
        }
    }
}
