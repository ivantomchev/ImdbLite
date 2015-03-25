namespace ImdbLite.Web.ViewModels.Movies
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;

    using ImdbLite.Common;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class MovieTopBottomListViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeYearFormatString)]
        public DateTime ReleaseDate { get; set; }

        public double? Rating { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Movie, MovieTopBottomListViewModel>()
                .ForMember(d => d.Rating, opt => opt.MapFrom(s => s.Votes.Select(x => x.Value).Average()));
        }
    }
}