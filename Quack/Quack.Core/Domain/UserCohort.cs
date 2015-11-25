using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quack.Core.Domain
{
    public class UserCohort
    {
        public int UserId { get; set; }
        public int CohortId { get; set; }
        
        public virtual User User { get; set; }
        public virtual Cohort Cohort { get; set; } 
    }
}
