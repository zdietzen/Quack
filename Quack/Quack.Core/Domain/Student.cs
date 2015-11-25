using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quack.Core.Domain
{
    public class Student
    {
        public int StudentId { get; set; }

        public int CohortId { get; set; } //NSIC

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> Approved { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; } //NSIC

    }
}
