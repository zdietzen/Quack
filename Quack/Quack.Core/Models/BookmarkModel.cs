namespace Quack.Core.Models
{
    public class BookmarkModel
    {
        public int BookmarkId { get; set; }

        public int StudentId { get; set; }
        public int MediaTypeId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool Likes { get; set; }
        public string URL { get; set; }
    }
}