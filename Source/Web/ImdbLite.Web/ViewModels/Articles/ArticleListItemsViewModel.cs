namespace ImdbLite.Web.ViewModels.Articles
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class ArticleListItemsViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string WrittenBy { get; set; }
    }
}