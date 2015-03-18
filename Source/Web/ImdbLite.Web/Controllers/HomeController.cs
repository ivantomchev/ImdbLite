
namespace ImdbLite.Web.Controllers
{
    using System.Web.Mvc;

    using ImdbLite.Data.UnitOfWork;
    using System.Linq;
    using ImdbLite.Web.ViewModels.Home;

    using AutoMapper.QueryableExtensions;
    using ImdbLite.Web.ViewModels.Articles;
    using ImdbLite.Web.ViewModels.Movies;

    public class HomeController : BaseController
    {
        public HomeController(IImdbLiteData data)
            :base(data)
        {

        }

        public ActionResult Index()
        {
            var model = new HomePageViewModel();
            model.CarouselArticles = this.Data.Articles.All().Project().To<ArticleHomePageCarouselViewModel>().OrderByDescending(x => x.CreatedOn).Take(7).ToList();
            model.Trailers = this.Data.Movies.All().Project().To<MovieHomePageTrailersViewModel>().OrderByDescending(x => x.CreatedOn).Take(3);
            model.OnTheaters = this.Data.Cinemas.All().SelectMany(x => x.Movies).Distinct().Project().To<MovieHomePageOnTheathersViewModel>();
            model.Articles = this.Data.Articles.All().Project().To<ArticleListItemsViewModel>().Take(10);
            model.Movies = this.Data.Movies.All().Project().To<MovieCinemaListItemViewModel>().Take(10);

            return View(model);
        }
    }
}