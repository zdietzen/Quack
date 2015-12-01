using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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