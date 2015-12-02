using Quack.Core.Models;
using System;
using System.Collections.Generic;

namespace Quack.Core.Domain
{
    public class Cohort
    {
        public int CohortId { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<TeacherCohort> TeacherCohorts { get; set; }

        public void Update(CohortModel cohort)
        {
            Name = cohort.Name;
            StartDate = cohort.StartDate;
            EndDate = cohort.EndDate;
        }
    }
}
