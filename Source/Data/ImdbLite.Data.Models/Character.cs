namespace ImdbLite.Data.Models
{
    using ImdbLite.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Character : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string CharacterName { get; set; }

        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        public int CelebrityId { get; set; }

        [ForeignKey("CelebrityId")]
        public virtual Celebrity Celebrity { get; set; }
    }
}
