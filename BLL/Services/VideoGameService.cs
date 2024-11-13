using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;

namespace BLL.Services
{
    public class VideoGameService : IVideoGameService
    {
        private readonly IVideoGameRepository _gameRepository;

        public VideoGameService(IVideoGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _gameRepository.DeleteAsync(id) > 0;
        }

        public async Task<List<VideoGameDto>> GetAllAsync()
        {
            return await _gameRepository.GetAllAsync();
        }

        public async Task<VideoGameDto> RetrieveAsync(int id)
        {
            return await _gameRepository.RetrieveAsync(id);
        }
        public async Task<List<VideoGameDto>> RetrieveFilteredAsync(string[] genres)
        {
            return await _gameRepository.RetrieveFilteredAsync(genres);
        }

        public async Task<int> CreateAsync(VideoGameDto game)
        {
            if (game?.Id is null)
            {
                return await _gameRepository.CreateAsync(game);
            }
            if (await _gameRepository.CreateAsync(game) > 0)
            {
                return game.Id;
            }
            return 0;
        }

        public async Task<int> UpdateAsync(VideoGameDto game)
        {
            if (game?.Id is null)
            {
                return await _gameRepository.UpdateAsync(game);
            }
            if (await _gameRepository.UpdateAsync(game) > 0)
            {
                return game.Id;
            }
            return 0;
        }

        public async Task<bool> UpdateGenresAsync(int id, string genres)
        {
            return await _gameRepository.UpdateGenresAsync(id, genres) > 0;
        }
    }
}
