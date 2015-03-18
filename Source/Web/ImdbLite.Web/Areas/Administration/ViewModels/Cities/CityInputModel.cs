namespace ImdbLite.Web.Areas.Administration.ViewModels.Cities
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    using System.ComponentModel.DataAnnotations;

    public class CityInputModel : IMapFrom<City>
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }
    }
}