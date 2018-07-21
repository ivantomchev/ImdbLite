﻿namespace ImdbLite.Web.Infrastructure.Populators
{
    using System.Collections.Generic;
    using System.Linq;

    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Web.Infrastructure.Caching;

    public class DropDownListPopulator : IDropDownListPopulator
    {
        private IImdbLiteData data;
        private ICacheService cache;

        public DropDownListPopulator(IImdbLiteData data, ICacheService cache)
        {
            this.data = data;
            this.cache = cache;
        }

        public IEnumerable<KeyValuePair<string, string>> Celebrities => this.data.Celebrities
                        .All()
                        .Select(x => new { Key = x.Id.ToString(), Value = x.FirstName + " " + x.LastName })
                        .OrderBy(x => x.Value)
                        .ToList()
                        .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                        .ToList();

        public IEnumerable<KeyValuePair<string, string>> Genres => this.data.Genres
                        .All()
                        .Select(x => new { Key = x.Id.ToString(), Value = x.Name })
                        .OrderBy(x => x.Value)
                        .ToList()
                        .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                        .ToList();

        public IEnumerable<KeyValuePair<string, string>> Cinemas => this.data.Cinemas
                        .All()
                        .Select(x => new { Key = x.Id.ToString(), Value = x.Name + " - " + x.City.Name })
                        .OrderBy(x => x.Value)
                        .ToList()
                        .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                        .ToList();

        public IEnumerable<KeyValuePair<string, string>> Movies => this.data.Movies
                        .All()
                        .Select(x => new { Key = x.Id.ToString(), Value = x.Title })
                        .OrderBy(x => x.Value)
                        .ToList()
                        .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                        .ToList();

        public IEnumerable<KeyValuePair<string, string>> Cities
        {
            get
            {
                var data = this.cache.Get("Cities",
                    () =>
                    {
                        return this.data.Cities
                            .All()
                            .Select(x => new { Key = x.Id.ToString(), Value = x.Name })
                            .OrderBy(x => x.Value)
                            .AsEnumerable()
                            .Select(x => new KeyValuePair<string, string>(x.Key, x.Value));
                    });

                return data;
            }
        }
    }
}
