namespace ImdbLite.Web.Areas.Administration.ViewModels.Characters
{
    using AutoMapper;

    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Data.Models;

    public class CharacterViewModel : IMapFrom<Character>, IHaveCustomMappings
    {
        public string CharacterName { get; set; }

        public string CelebrityName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Character, CharacterViewModel>()
                .ForMember(d => d.CelebrityName, opt => opt.MapFrom(s => s.Celebrity.FirstName + " " + s.Celebrity.LastName));
        }
    }
}