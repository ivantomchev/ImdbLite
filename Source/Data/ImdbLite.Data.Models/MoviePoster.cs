namespace ImdbLite.Data.Models
{
    using ImdbLite.Data.Models.Base;

    public class MoviePoster : Image
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
