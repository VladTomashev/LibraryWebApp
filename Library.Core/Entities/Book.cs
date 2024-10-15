namespace Library.Core.Entities
{
    public class Book : AbstractEntity
    {
        public string Isbn { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set;}
        public string ImagePath { get; set; }
    }
}
