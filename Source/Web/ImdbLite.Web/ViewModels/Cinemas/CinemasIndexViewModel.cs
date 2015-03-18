namespace ImdbLite.Web.ViewModels.Cinemas
{
    using AutoMapper;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CinemasIndexViewModel : IMapFrom<Cinema>,IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string WebAddress { get; set; }

        public string City { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Cinema, CinemasIndexViewModel>()
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City.Name));
        }
    }
}