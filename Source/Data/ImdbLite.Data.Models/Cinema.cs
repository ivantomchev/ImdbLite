namespace ImdbLite.Data.Models
{
    using ImdbLite.Data.Models.Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Cinema : BaseEntity
    {
        private ICollection<Movie> movies;

        public Cinema()
        {
            this.movies = new HashSet<Movie>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        public string WebAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string EMail { get; set; }

        public string GoogleMapsUrl { get; set; }

        public virtual City City { get; set; }

        [Required]
        public int CityId { get; set; }

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
