namespace ImdbLite.Data.Models
{
    using ImdbLite.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;

    public class Vote : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public double Value { get; set; }

        public string VotedById { get; set; }

        public virtual User VotedBy { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}