namespace ImdbLite.Web.ViewModels.Cinemas
{
    using AutoMapper;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class CinemaMovieDetailsViewModel : IMapFrom<Cinema>,IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Cinema, CinemaMovieDetailsViewModel>()
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City.Name));
        }
    }
}