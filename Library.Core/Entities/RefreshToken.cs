namespace Library.Core.Entities
{
    public class RefreshToken : AbstractEntity
    {
        public string? Token { get; set; }
        public DateTime? LifeTime { get; set; }
    }
}
