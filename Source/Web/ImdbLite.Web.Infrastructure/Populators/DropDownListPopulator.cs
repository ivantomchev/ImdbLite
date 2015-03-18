namespace ImdbLite.Web.Infrastructure.Populators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using ImdbLite.Common.Extensions;
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

        public IEnumerable<SelectListItem> GetCelebrities()
        {
            var data = this.cache.Get<IEnumerable<SelectListItem>>("Celebrities",
                () =>
                {
                    return this.data.Celebrities.All().ToSelectList(x => x.FirstName + " " + x.LastName, x => x.Id).OrderBy(x => x.Text).ToList();
                });

            return data;
        }

        public IEnumerable<SelectListItem> GetGenres()
        {
            var data = this.cache.Get<IEnumerable<SelectListItem>>("Genres",
                () =>
                {
                    return this.data.Genres.All().ToSelectList(x => x.Name, x => x.Id).OrderBy(x => x.Text).ToList();
                });

            return data;
        }

        public IEnumerable<SelectListItem> GetCinemas()
        {
            var data = this.cache.Get<IEnumerable<SelectListItem>>("Cinemas",
                () =>
                {
                    return this.data.Cinemas.All().Select(x => new 
                    {
                       Id = x.Id,
                       Name = x.Name,
                       City = x.City.Name

                    }).ToSelectList(x => x.Name + " - " + x.City, x => x.Id).OrderBy(x => x.Text).ToList();
                });

            return data;
        }

        public IEnumerable<SelectListItem> GetMovies()
        {
            var data = this.cache.Get<IEnumerable<SelectListItem>>("Movies",
                () =>
                {
                    return this.data.Movies.All().ToSelectList(x => x.Title, x => x.Id).OrderBy(x => x.Text).ToList();
                });

            return data;
        }

        public IEnumerable<SelectListItem> GetCities()
        {
            var data = this.cache.Get<IEnumerable<SelectListItem>>("Cities",
                () =>
                {
                    return this.data.Cities.All().ToSelectList(x => x.Name, x => x.Id).ToList();
                });

            return data;
        }
    }
}
