using Domain.DTOs;
namespace Domain.Repositories
{
    public interface IVideoGameRepository
    {
        Task<VideoGameDto> RetrieveAsync(int id);
        Task<List<VideoGameDto>> RetrieveFilteredAsync(string[] genre);
        Task<List<VideoGameDto>> GetAllAsync();
        Task<int> CreateAsync(VideoGameDto game);
        Task<int> UpdateAsync(VideoGameDto game);
        Task<int> UpdateGenresAsync(int id, string genre);
        Task<int> DeleteAsync(int id);
    }
}
