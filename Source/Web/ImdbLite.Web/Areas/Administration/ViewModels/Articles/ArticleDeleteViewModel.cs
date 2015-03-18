namespace ImdbLite.Web.Areas.Administration.ViewModels.Articles
{
    using System.Web.Mvc;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class ArticleDeleteViewModel : IMapFrom<Article>
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}