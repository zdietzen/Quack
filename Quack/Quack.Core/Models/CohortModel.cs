using System;

namespace Quack.Core.Models
{
    public class CohortModel
    {
        public int CohortId { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}