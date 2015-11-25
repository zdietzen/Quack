using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quack.Core.Domain
{
    public class Cohort
    {
        public int CohortId { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Student> Students { get; set; } //NSIC
        public virtual ICollection<UserCohort> UserCohorts { get; set; } //NSIC
    }
}
