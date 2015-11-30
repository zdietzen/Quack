namespace Quack.Core.Domain
{
    public class TeacherCohort
    {
        public int TeacherId { get; set; }
        public int CohortId { get; set; }
        
        public virtual Teacher Teacher { get; set; }
        public virtual Cohort Cohort { get; set; } 
    }
}
