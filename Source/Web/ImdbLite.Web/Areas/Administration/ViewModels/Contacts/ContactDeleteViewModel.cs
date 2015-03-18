namespace ImdbLite.Web.Areas.Administration.ViewModels.Contacts
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.Web.Mvc;

    public class ContactDeleteViewModel : IMapFrom<Contact>
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}