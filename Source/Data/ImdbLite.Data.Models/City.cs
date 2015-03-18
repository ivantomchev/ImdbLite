namespace ImdbLite.Data.Models
{
    using ImdbLite.Data.Models.Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class City : BaseEntity
    {
        private ICollection<Cinema> cinemas;

        public City() 
        {
            this.cinemas = new HashSet<Cinema>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Cinema> Cinemas
        {
            get 
            {
                return this.cinemas;
            }
            set
            {
                this.cinemas = value;
            }
        }
    }
}
