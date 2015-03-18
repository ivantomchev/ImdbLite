namespace ImdbLite.Data.Models
{
    using ImdbLite.Data.Models.Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre : BaseEntity
    {
        private ICollection<Movie> movies;

        public Genre()
        {
            this.movies = new HashSet<Movie>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get
            {
                return this.movies;
            }
            set
            {
                this.movies = value;
            }
        }
    }
}
