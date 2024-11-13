using Domain.DTOs;
using VideoGameLibrary.Models;

namespace VideoGameLibrary.Mapping.Interfaces
{
    public interface IVideoGameMapper
    {
        public VideoGameDto Map(VideoGame game);
        VideoGame Map(VideoGameDto game);
        List<VideoGame> Map(List<VideoGameDto> games);
    }
}
