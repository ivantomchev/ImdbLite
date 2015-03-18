namespace ImdbLite.Web.Areas.Administration.ViewModels.Cities
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CityUpdateModel : IMapFrom<City>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }
    }
}