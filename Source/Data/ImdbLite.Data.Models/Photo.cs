namespace ImdbLite.Data.Models
{
    using ImdbLite.Data.Models.Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Photo : BaseEntity
    {
        private ICollection<Movie> relatedMovies;
        private ICollection<Celebrity> relatedCelebrities;

        public Photo()
        {
            this.relatedMovies = new HashSet<Movie>();
            this.relatedCelebrities = new HashSet<Celebrity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        public virtual ICollection<Movie> RelatedMovies
        {
            get
            {
                return this.relatedMovies;
            }
            set
            {
                this.relatedMovies = value;
            }
        }

        public virtual ICollection<Celebrity> RelatedCelebrities
        {
            get
            {
                return this.relatedCelebrities;
            }
            set
            {
                this.relatedCelebrities = value;
            }
        }
    }
}
