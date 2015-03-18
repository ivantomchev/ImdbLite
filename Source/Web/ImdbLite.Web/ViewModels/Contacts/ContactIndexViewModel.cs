namespace ImdbLite.Web.ViewModels.Contacts
{
    using AutoMapper;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class ContactIndexViewModel : IMapFrom<Contact>,IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string FullName { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Contact, ContactIndexViewModel>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City.Name));
        }
    }
}