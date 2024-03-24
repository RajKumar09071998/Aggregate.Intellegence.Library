namespace Aggregate.Intellegence.Library.Web.UI.Model
{
    public class Book:Common
    {
        public long BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public int? PublishYear { get; set; }
        public string? Genre { get; set; }
        public string? Language { get; set; }
        public int? PageCount { get; set; }
        public string? Description { get; set; }
    }
}
