namespace ImdbLite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ImdbLite.Services.Data.DTOs;

    public interface IGenresService
    {
        Task<List<GenreDTO>> GetAsync(uint skip = 0, uint take = int.MaxValue, string sort = null);

        Task<GenreDTO> GetByIdAsync(int id);

        Task UpdateAsync(GenreDTO genre);

        Task<GenreDTO> AddAsync(GenreDTO genre);

        Task<int> GetCountAsync();
    }
}