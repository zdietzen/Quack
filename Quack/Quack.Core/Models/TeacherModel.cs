﻿namespace Quack.Core.Models
{
    public class TeacherModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public class Register
        {
            public string Email { get; set; }
        }
    }
}