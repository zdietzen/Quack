using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quack.Core.Domain
{
    public class Bookmark
    {
        public int BookmarkId { get; set; }

        public int StudentId { get; set; }  // NSIC
        public int MediaTypeId { get; set; } // NSIC

        public string Title { get; set; }
        public string Description { get; set;  }
        public int Likes { get; set; }
        public string URL { get; set; }

        public virtual ICollection<MediaType> MediaTypes { get; set; } // NSIC
    }
}
