
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
            model.CarouselArticles = this.Data.Articles.All().OrderByDescending(x => x.CreatedOn).Take(7).Project().To<ArticleHomePageCarouselViewModel>().ToList();
            model.Trailers = this.Data.Movies.All().OrderByDescending(x => x.CreatedOn).Take(3).Project().To<MovieHomePageTrailersViewModel>();
            model.OnTheaters = this.Data.Cinemas.All().SelectMany(x => x.Movies).Distinct().Project().To<MovieHomePageOnTheathersViewModel>();
            model.Articles = this.Data.Articles.All().OrderByDescending(x => x.CreatedOn).Take(10).Project().To<ArticleListItemsViewModel>();
            model.Movies = this.Data.Movies.All().OrderByDescending(x => x.CreatedOn).Take(10).Project().To<MovieCinemaListItemViewModel>();
            model.TopTenMovies = this.Data.Movies.All().Project().To<MovieTopBottomListViewModel>().OrderByDescending(x => x.Rating).Take(10);
            model.BottomTenMovies = this.Data.Movies.All().Project().To<MovieTopBottomListViewModel>().OrderBy(x => x.Rating).Take(10);

            return View(model);
        }
    }
}