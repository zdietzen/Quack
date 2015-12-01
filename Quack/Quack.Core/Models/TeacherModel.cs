using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quack.Core.Models
{
    public class TeacherModel
    {
        public int TeacherId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}