namespace ImdbLite.Web.Areas.Administration.ViewModels.Contacts
{
    using AutoMapper;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Areas.Administration.ViewModels.Base;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class ContactIndexViewModel : NotDeletedIndexViewModel, IMapFrom<Contact>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Contact, ContactIndexViewModel>()
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City.Name));
        }
    }
}