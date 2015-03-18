namespace ImdbLite.Web.ViewModels.Articles
{
    using ImdbLite.Data.Models;
using ImdbLite.Web.Infrastructure.Mapping;
using ImdbLite.Web.ViewModels.Celebrities;
using ImdbLite.Web.ViewModels.Movies;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

    public class ArticleDetailsViewModel : IMapFrom<Article>
    {
        public string Title { get; set; }

        [Display(Name = "by")]
        public string WrittenBy { get; set; }

        public ArticlePhoto Photo { get; set; }

        public string Content { get; set; }

        [UIHint("MovieCinemaListItems")]
        [Display(Name="Related Movies")]
        public IEnumerable<MovieCinemaListItemViewModel> RelatedMovies { get; set; }

        [UIHint("CelebrityArticleListItems")]
        [Display(Name = "Related Celebrities")]
        public IEnumerable<CelebrityArticleListItemViewModel> RelatedCelebrities { get; set; }
    }
}