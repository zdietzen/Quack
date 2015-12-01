using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quack.Core.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }

        public int CohortId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> Approved { get; set; }

    }
}