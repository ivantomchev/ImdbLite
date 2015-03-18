namespace ImdbLite.Web.ViewModels.CastMember
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using ImdbLite.Common;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class CastMemberCelebrityViewModel : IMapFrom<CastMember>, IHaveCustomMappings
    {
        [HiddenInput]
        public string MovieId { get; set; }

        public string MovieTitle { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeYearFormatString)]
        [Display(Name = "Year")]
        public DateTime MovieReleaseDate { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CastMember, CastMemberCelebrityViewModel>()
                .ForMember(d => d.MovieTitle, opt => opt.MapFrom(s => s.Movie.Title))
                .ForMember(d => d.MovieReleaseDate, opt => opt.MapFrom(s => s.Movie.ReleaseDate));
        }
    }
}