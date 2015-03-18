namespace ImdbLite.Web.Areas.Administration.ViewModels.Cinemas
{
    using AutoMapper;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Areas.Administration.ViewModels.Base;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class CinemaIndexViewModel : NotDeletedIndexViewModel, IMapFrom<Cinema>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Cinema, CinemaIndexViewModel>()
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City.Name));
        }
    }
}