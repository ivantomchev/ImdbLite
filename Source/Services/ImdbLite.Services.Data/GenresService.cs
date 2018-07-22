namespace ImdbLite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using ImdbLite.Common;
    using ImdbLite.Common.Extensions;
    using ImdbLite.Data.Models;
    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Services.Data.DTOs;

    public class GenresService : IGenresService
    {
        private readonly IImdbLiteData _data;

        public GenresService(IImdbLiteData data)
            => _data = data;

        public async Task<GenreDTO> GetByIdAsync(int id)
        {
            return await _data.Genres
                            .All()
                            .Where(g => g.Id == id)
                            .Select(g => new GenreDTO
                            {
                                Id = g.Id,
                                Name = g.Name,
                                CreatedOn = g.CreatedOn,
                                DeletedOn = g.DeletedOn,
                                ModifiedOn = g.ModifiedOn,
                                IsDeleted = g.IsDeleted
                            })
                            .SingleAsync();
        }

        public async Task<List<GenreDTO>> GetAsync(uint skip = 0, uint take = int.MaxValue, string sort = null)
        {
            var query = _data.Genres
                            .All()
                            .Select(g => new GenreDTO
                            {
                                Id = g.Id,
                                Name = g.Name,
                                CreatedOn = g.CreatedOn,
                                DeletedOn = g.DeletedOn,
                                ModifiedOn = g.ModifiedOn,
                                IsDeleted = g.IsDeleted
                            });

            query = String.Equals(sort, GlobalConstants.ASC, StringComparison.InvariantCultureIgnoreCase)
                    ? query.OrderBy(m => m.CreatedOn)
                    : query.OrderByDescending(m => m.CreatedOn);

            return await query
                            .Skip(skip.ToInt())
                            .Take(take.ToInt())
                            .ToListAsync();
        }

        public async Task UpdateAsync(GenreDTO genre)
        {
            var dbModel = _data.Genres.GetById(genre.Id);
            dbModel.Name = genre.Name;
            _data.Genres.Update(dbModel);
            await _data.SaveChangesAsync();
        }

        public async Task<GenreDTO> AddAsync(GenreDTO genre)
        {
            var result = _data.Genres.Add(new Genre { Name = genre.Name});
            await _data.SaveChangesAsync();

            genre.Id = result.Id;
            return genre;
        }

        public async Task<int> GetCountAsync()
            => await _data.Genres.All().CountAsync();
    }
}
