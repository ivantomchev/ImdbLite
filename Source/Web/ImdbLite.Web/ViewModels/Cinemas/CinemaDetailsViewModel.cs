namespace ImdbLite.Web.ViewModels.Cinemas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Web.ViewModels.Movies;

    public class CinemaDetailsViewModel : IMapFrom<Cinema>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Address { get; set; }

        [Display(Name = "Web Address")]
        public string WebAddress { get; set; }

        public string City { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-Mail")]
        public string EMail { get; set; }

        [Display(Name = "Location")]
        public string GoogleMapsUrl { get; set; }

        [UIHint("MovieCinemaListItems")]
        public IEnumerable<MovieCinemaListItemViewModel> Movies { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Cinema, CinemaDetailsViewModel>()
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City.Name));
        }
    }
}