namespace ImdbLite.Web.ViewModels.Home
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.ViewModels.Articles;
    using ImdbLite.Web.ViewModels.Movies;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class HomePageViewModel
    {
        [Display(Name="Articles")]
        [UIHint("HomePageCarouselPanel")]
        public IList<ArticleHomePageCarouselViewModel> CarouselArticles { get; set; }

        [UIHint("HomePageTrailersPanel")]
        public IEnumerable<MovieHomePageTrailersViewModel> Trailers { get; set; }

        public IEnumerable<MovieHomePageOnTheathersViewModel> OnTheaters { get; set; }

        [UIHint("HomePageArticlePanel")]
        [Display(Name = "Last 10 Articles")]
        public IEnumerable<ArticleListItemsViewModel> Articles { get; set; }

        [UIHint("HomePageMoviesPanel")]
        [Display(Name = "Last Added Movies")]
        public IEnumerable<MovieCinemaListItemViewModel> Movies { get; set; }

        [UIHint("MovieTopBottomListPanel")]
        [Display(Name = "Top 10 Movies")]
        public IEnumerable<MovieTopBottomListViewModel> TopTenMovies { get; set; }

        [UIHint("MovieTopBottomListPanel")]
        [Display(Name = "Bottom 10 Movies")]
        public IEnumerable<MovieTopBottomListViewModel> BottomTenMovies { get; set; }
    }
}