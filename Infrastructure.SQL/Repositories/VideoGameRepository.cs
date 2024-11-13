using Infrastructure.SQL.Database.Entities;
using Domain.DTOs;
using Domain.Repositories;
using Infrastructure.SQL.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.SQL.Repositories
{
    public class VideoGameRepository : IVideoGameRepository
    {
        private readonly LibraryContext _libContext;

        public VideoGameRepository(LibraryContext libContext)
        {
            _libContext = libContext;
        }
        public async Task<int> CreateAsync(VideoGameDto game)
        {
            var gameEntity = new VideoGameEntity
            {
                Name = game.Name,
                Studio = game.Studio,
                Genres = game.Genres
            };
            await _libContext.AddAsync(gameEntity);
            await _libContext.SaveChangesAsync();
            return gameEntity.Id;
        }
        public async Task<int> UpdateAsync(VideoGameDto game)
        {
            var gameEntity = new VideoGameEntity
            {
                Id = game.Id,
                Name = game.Name,
                Studio = game.Studio,
                Genres = game.Genres
            };
            return await _libContext.VideoGames
            .Where(x =>
            x.Id == gameEntity.Id)
            .ExecuteUpdateAsync(s =>

            s.SetProperty
            (p => p.Studio, gameEntity.Studio)
            .SetProperty
            (p => p.Genres, gameEntity.Genres)
            .SetProperty
            (p => p.Name, gameEntity.Name));
        }
        public async Task<int> DeleteAsync(int id)
        {
            return await _libContext.VideoGames
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        }
        public async Task<List<VideoGameDto>> GetAllAsync()
        {
            return await _libContext.VideoGames
            .AsNoTracking()
            .Select(x => new VideoGameDto
            {
                Id = x.Id,
                Name = x.Name,
                Studio = x.Studio,
                Genres = x.Genres
            })
            .ToListAsync();
        }
        public async Task<VideoGameDto> RetrieveAsync(int id)
        {
            return await _libContext.VideoGames
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new VideoGameDto
            {
                Id = x.Id,
                Name = x.Name,
                Studio = x.Studio,
                Genres = x.Genres
            })
            .FirstOrDefaultAsync();
        }

        public async Task<List<VideoGameDto>> RetrieveFilteredAsync(string[] genres)
        {
            var bbb = await _libContext.VideoGames
                 .AsNoTracking()
                 .Select(x => new VideoGameDto
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Studio = x.Studio,
                     Genres = x.Genres
                 }).ToListAsync();

            return bbb.Where(x => genres.All(x1 => x.Genres.Split(',')
            .Select(p => p.Trim())
            .Contains(x1))).ToList();
        }

        public async Task<int> UpdateGenresAsync(int id, string genre)
        {
            return await _libContext.VideoGames
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync
            (s => s.SetProperty(p => p.Genres, genre));
        }
    }
}
