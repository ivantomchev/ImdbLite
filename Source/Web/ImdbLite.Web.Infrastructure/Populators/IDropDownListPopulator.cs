namespace ImdbLite.Web.Infrastructure.Populators
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IDropDownListPopulator
    {
        IEnumerable<SelectListItem> GetCelebrities();

        IEnumerable<SelectListItem> GetGenres();

        IEnumerable<SelectListItem> GetCinemas();

        IEnumerable<SelectListItem> GetMovies();

        IEnumerable<SelectListItem> GetCities();
    }
}
