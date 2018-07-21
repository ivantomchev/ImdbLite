namespace ImdbLite.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using ImdbLite.Services.Data.DTOs;

    public interface IMoviesService
    {
        Task<MovieDTO> GetByIdAsync(int id);

        Task<MovieDTO> AddAsync(MovieDTO movie);

        Task UpdateAsync(MovieDTO movie);
    }
}
