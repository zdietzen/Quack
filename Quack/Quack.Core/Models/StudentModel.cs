using System;

namespace Quack.Core.Models
{
    public class StudentModel
    {
        public string Id { get; set; }

        public int CohortId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> Approved { get; set; }
        public class Register
        {
            public string Email { get; set; }

        }
    }
}
