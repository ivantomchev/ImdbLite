namespace ImdbLite.Web.ViewModels.Articles
{
    using System;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class ArticleHomePageCarouselViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public ArticlePhoto Photo { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}