using Domain.DTOs;

namespace Domain.Services
{
    public interface IVideoGameService
    {
        Task<VideoGameDto> RetrieveAsync(int id);
        Task <List<VideoGameDto>> RetrieveFilteredAsync(string[] genres);
        Task<List<VideoGameDto>> GetAllAsync();
        Task<int> CreateAsync(VideoGameDto game);
        Task<int> UpdateAsync(VideoGameDto game);
        Task<bool> UpdateGenresAsync(int id, string genres);
        Task<bool> DeleteAsync(int id);
    }
}
