namespace ImdbLite.Web.Areas.Administration.ViewModels.Genres
{
    using System.ComponentModel.DataAnnotations;

    using ImdbLite.Services.Data.DTOs;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class GenreInputModel : IMapFrom<GenreDTO>
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }
    }
}