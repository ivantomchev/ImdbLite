namespace ImdbLite.Data.Models
{
    using ImdbLite.Data.Models.Base;

    public class ArticlePhoto : Image
    {
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
