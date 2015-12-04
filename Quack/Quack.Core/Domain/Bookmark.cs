using Quack.Core.Models;
using System;

namespace Quack.Core.Domain
{
    public class Bookmark
    {
        public int BookmarkId { get; set; }

        public string QuackUserId { get; set; }  
        public int MediaTypeId { get; set; } 

        public string Title { get; set; }
        public string Description { get; set;  }
        public bool Likes { get; set; }
        public string URL { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MediaType MediaType { get; set; } 
        public virtual QuackUser Student { get; set; }

        public void Update(BookmarkModel bookmark)
        {
            if (bookmark.BookmarkId == 0)
            {
                CreatedDate = DateTime.Now;
            }
            Title = bookmark.Title;
            Description = bookmark.Description;
            Likes = bookmark.Likes;
            URL = bookmark.URL;
        }
    }
}
