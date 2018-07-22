namespace ImdbLite.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ImdbLite.Services.Data.DTOs;

    public interface IMoviesService
    {
        Task<List<MovieListItemDTO>> GetAsync(uint skip = 0, uint take = int.MaxValue, string sort = null);

        Task<MovieDTO> GetByIdAsync(int id);

        Task<MovieDTO> AddAsync(MovieDTO movie);

        Task UpdateAsync(MovieDTO movie);

        Task<int> GetCountAsync();
    }
}
