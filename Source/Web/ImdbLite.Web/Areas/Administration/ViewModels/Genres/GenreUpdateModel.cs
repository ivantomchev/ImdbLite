namespace ImdbLite.Web.Areas.Administration.ViewModels.Genres
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ImdbLite.Services.Data.DTOs;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class GenreUpdateModel : IMapFrom<GenreDTO>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }
    }
}