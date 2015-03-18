namespace ImdbLite.Web.Areas.Administration.ViewModels.Contacts
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ContactInputModel : IMapFrom<Contact>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Contact Title")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Title { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "First Name")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Last Name")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Company Name")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string CompanyName { get; set; }

        [UIHint("SmallMultiLineText")]
        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Address { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Web Address")]
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string WebAddress { get; set; }

        [Display(Name = "City")]
        [Required]
        public int CityId { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Phone Number")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string PhoneNumber { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "E-Mail")]
        [Required]
        public string EMail { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Google Maps URL")]
        [Required]
        public string GoogleMapsUrl { get; set; }

        public IEnumerable<SelectListItem> CitiesList { get; set; }
    }
}