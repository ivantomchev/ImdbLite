namespace ImdbLite.Web.Areas.Administration.ViewModels.Articles
{
    using System.Web.Mvc;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Areas.Administration.ViewModels.Base;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class ArticleIndexViewModel : NotDeletedIndexViewModel, IMapFrom<Article>
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}