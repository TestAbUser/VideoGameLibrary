using VideoGameLibrary.Mapping.Interfaces;
using VideoGameLibrary.Models;
using Domain.DTOs;

namespace VideoGameLibrary.Mapping
{
    public class VideoGameMapper : IVideoGameMapper
    {
        public VideoGameDto Map(VideoGame game)
        {
            return game != null ? new VideoGameDto
            {
                Id = game.Id.Value,
                Name = game.Name,
                Studio = game.Studio,
                Genres = game.Genres,
            } : null;
        }

        public VideoGame Map(VideoGameDto game)
        {
            return game != null ? new VideoGame
            {
                Id = game.Id,
                Name = game.Name,
                Studio = game.Studio,
                Genres = game.Genres,
            } : null;
        }

        public List<VideoGame> Map(List<VideoGameDto> games)
        {
            return games.Select(Map).ToList();
        }
    }
}
