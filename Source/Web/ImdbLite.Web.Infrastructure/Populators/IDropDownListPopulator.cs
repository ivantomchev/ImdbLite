namespace ImdbLite.Web.Infrastructure.Populators
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IDropDownListPopulator
    {
        IEnumerable<KeyValuePair<string, string>> Celebrities { get; }

        IEnumerable<KeyValuePair<string, string>> Genres { get; }

        IEnumerable<KeyValuePair<string, string>> Cinemas { get; }

        IEnumerable<KeyValuePair<string, string>> Movies { get; }

        IEnumerable<KeyValuePair<string, string>> Cities { get; }
    }
}
