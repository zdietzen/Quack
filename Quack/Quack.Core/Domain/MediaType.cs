using Quack.Core.Models;
using System.Collections.Generic;

namespace Quack.Core.Domain
{
    public class MediaType
    {
       
        public int MediaTypeId { get; set; }

        public string Type { get; set; }

        public ICollection<Bookmark> Bookmarks { get; set; }

        public void Update(MediaTypeModel mediatype)
        {
            Type = mediatype.Type;
        }
    }
}
