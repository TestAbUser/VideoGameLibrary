namespace Domain.DTOs
{
    public class VideoGameDto
    {
        public int Id { get; set; }
        public required string  Name { get; set; }
        public required string Studio { get; set; }
        public required string Genres { get; set; }
    }
}
