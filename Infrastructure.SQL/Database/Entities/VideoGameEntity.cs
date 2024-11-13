namespace Infrastructure.SQL.Database.Entities
{
    public class VideoGameEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Studio { get; set; }
        public required string Genres { get; set; }
    }
}
