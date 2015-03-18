namespace ImdbLite.Web.ViewModels.Movies
{
    using System;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    using ImdbLite.Common;
    using AutoMapper;

    public class MovieCinemaListItemViewModel : IMapFrom<Movie>,IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeYearFormatString)]
        public DateTime Year { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Movie, MovieCinemaListItemViewModel>()
                .ForMember(d => d.Year, opt => opt.MapFrom(s => s.ReleaseDate));
        }
    }
}