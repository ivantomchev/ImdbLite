namespace ImdbLite.Web.Areas.Administration.ViewModels.Cities
{
    using System.Web.Mvc;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Web.Areas.Administration.ViewModels.Base;


    public class CityIndexViewModel : NotDeletedIndexViewModel, IMapFrom<City>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}