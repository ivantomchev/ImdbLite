namespace ImdbLite.Data.Models
{
    using ImdbLite.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;

    public class Contact : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(30)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [Required]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string WebAddress { get; set; }

        [MaxLength(50)]
        public string EMail { get; set; }

        [Required]
        public string GoogleMapsUrl { get; set; }
    }
}
