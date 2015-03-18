namespace ImdbLite.Web.Areas.Administration.ViewModels.Genres
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Data.Models;

    public class GenreUpdateModel : IMapFrom<Genre>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }
    }
}