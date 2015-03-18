namespace ImdbLite.Data.Models
{
    using ImdbLite.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CelebrityMainPhoto : Image
    {
        public int CelebrityId { get; set; }

        public virtual Celebrity Celebrity { get; set; }
    }
}
