namespace ImdbLite.Web.Controllers
{
    using ImdbLite.Data.UnitOfWork;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using DbModel = ImdbLite.Data.Models.Contact;
    using IndexViewModel = ImdbLite.Web.ViewModels.Contacts.ContactIndexViewModel;
    using QuickViewViewModel = ImdbLite.Web.ViewModels.Contacts.ContactQuickViewViewModel;

    public class ContactsController : BaseEntityController
    {
        public ContactsController(IImdbLiteData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var data = GetData<IndexViewModel>();

            return View(data.ToList());
        }

        [HttpGet]
        public ActionResult QuickView(int? id)
        {
            var model = base.GetViewModel<DbModel, QuickViewViewModel>(id);
            if (model == null)
            {
                model = GetData<QuickViewViewModel>().FirstOrDefault();
            }

            return PartialView("_ContactQuickViewPartial", model);
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Contacts.All().Project().To<TViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Contacts.GetById(id) as T;
        }
    }
}