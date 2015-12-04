using Quack.Core.Models;

namespace Quack.Core.Domain
{
    public class TeacherCohort
    {
        public string QuackUserId { get; set; }
        public int CohortId { get; set; }

        public virtual QuackUser Teacher { get; set; }
        public virtual Cohort Cohort { get; set; }
    }
}
