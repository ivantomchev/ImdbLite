namespace ImdbLite.Web.ViewModels.Character
{
    using AutoMapper;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.Web.Mvc;
    using ImdbLite.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using ImdbLite.Common;
    using System;

    public class CharacterCelebrityViewModel : IMapFrom<Character>, IHaveCustomMappings
    {
        [HiddenInput]
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public string CharacterName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeYearFormatString)]
        public DateTime MovieReleaseDate { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Character, CharacterCelebrityViewModel>()
                .ForMember(d => d.MovieTitle, opt => opt.MapFrom(s => s.Movie.Title))
                .ForMember(d => d.CharacterName, opt => opt.MapFrom(s => s.CharacterName))
                .ForMember(d => d.MovieReleaseDate,opt => opt.MapFrom(s => s.Movie.ReleaseDate));
        }
    }
}