namespace ImdbLite.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    
    public class ContactQuickViewViewModel : IMapFrom<Contact>, IHaveCustomMappings
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        public string Address { get; set; }

        [Display(Name = "Web Address")]
        public string WebAddress { get; set; }

        public string City { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-Mail")]
        public string EMail { get; set; }

        [Display(Name = "Google Maps URL")]
        public string GoogleMapsUrl { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Contact, ContactQuickViewViewModel>()
            .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.City.Name));
        }
    }
}