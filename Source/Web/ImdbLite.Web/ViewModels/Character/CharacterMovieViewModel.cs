namespace ImdbLite.Web.ViewModels.Character
{
    using AutoMapper;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.Web.Mvc;

    public class CharacterMovieViewModel : IMapFrom<Character>, IHaveCustomMappings
    {
        [HiddenInput]
        public int CelebrityId { get; set; }

        public string CelebrityFullName { get; set; }

        public string CharacterName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Character, CharacterMovieViewModel>()
                .ForMember(d => d.CelebrityFullName, opt => opt.MapFrom(s => s.Celebrity.FirstName + " " + s.Celebrity.LastName))
                .ForMember(d => d.CelebrityId, opt => opt.MapFrom(s => s.CelebrityId))
                .ForMember(d => d.CharacterName, opt => opt.MapFrom(s => s.CharacterName));
        }
    }
}