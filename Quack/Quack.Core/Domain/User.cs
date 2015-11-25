using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quack.Core.Domain
{
    public class User //Need to inherit IdentityUser
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<UserCohort> UserCohorts { get; set; } //NSIC

    }
}
