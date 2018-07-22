namespace ImdbLite.Web.Areas.Administration.ViewModels.Characters
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using ImdbLite.Services.Data.DTOs;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class CharacterInputModel : IMapFrom<CharacterDTO>, IHaveCustomMappings
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
            configuration.CreateMap<CharacterDTO, CharacterInputModel>()
                .ForMember(d => d.CharacterName, opt => opt.MapFrom(s => s.Name));

            configuration.CreateMap<CharacterInputModel, CharacterDTO>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CharacterName));
        }
    }
}