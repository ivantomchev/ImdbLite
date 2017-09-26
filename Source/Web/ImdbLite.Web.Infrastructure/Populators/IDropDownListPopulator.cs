namespace ImdbLite.Web.Infrastructure.Populators
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IDropDownListPopulator
    {
        IEnumerable<KeyValuePair<string,string>> GetCelebrities();

        IEnumerable<KeyValuePair<string, string>> GetGenres();

        IEnumerable<KeyValuePair<string, string>> GetCinemas();

        IEnumerable<KeyValuePair<string, string>> GetMovies();

        IEnumerable<KeyValuePair<string, string>> GetCities();
    }
}
