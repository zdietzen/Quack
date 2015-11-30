using System;
using System.Collections.Generic;

namespace Quack.Core.Domain
{
    public class Student
    {
        public int StudentId { get; set; }

        public int CohortId { get; set; } 

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> Approved { get; set; }

        
        public virtual Cohort Cohort { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}
