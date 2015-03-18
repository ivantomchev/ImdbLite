namespace ImdbLite.Web.Areas.Administration.ViewModels.Characters
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Data.Models;
    using System.Web.Mvc;

    public class CharacterInputModel : IMapFrom<Character>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [UIHint("SingleLineText")]
        [Required(ErrorMessage = "Please enter the character name!")]
        [StringLength(60, MinimumLength = 1)]
        public string CharacterName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CelebrityId { get; set; }

        public string CelebrityName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int MovieId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Character, CharacterInputModel>()
                .ForMember(d => d.CelebrityName, opt => opt.MapFrom(s => s.Celebrity.FirstName + " " + s.Celebrity.LastName));
        }
    }
}