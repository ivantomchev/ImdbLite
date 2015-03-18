namespace ImdbLite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Web.Areas.Administration.Controllers.Base;
    using ImdbLite.Common.Extensions;

    using DbModel = ImdbLite.Data.Models.Contact;
    using IndexViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Contacts.ContactIndexViewModel;
    using InputModel = ImdbLite.Web.Areas.Administration.ViewModels.Contacts.ContactInputModel;
    using DeleteViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Contacts.ContactDeleteViewModel;
    using ImdbLite.Web.Infrastructure.Populators;

    public class ContactsController : AdminController
    {
        private IDropDownListPopulator populator;
        private const int PageSize = 10;

        public ContactsController(IImdbLiteData data, IDropDownListPopulator populator)
            : base(data)
        {
            this.populator = populator;
        }

        public ActionResult Index()
        {
            var data = this.GetData<IndexViewModel>();

            return View(data);
        }

        public ActionResult ReadData(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);
            var count = (double)GetData<IndexViewModel>().Count();
            var data = GetData<IndexViewModel>().OrderByDescending(x => x.CreatedOn).Skip((pageNumber - 1) * PageSize).Take(PageSize);

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_ReadContactsPartial", data);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            var model = base.GetViewModel<DbModel, InputModel>(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            model.CitiesList = this.populator.GetCities();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(InputModel model)
        {
            var dbModel = base.Update<DbModel, InputModel>(model, model.Id);
            if (dbModel != null)
            {
                return RedirectToAction("Index");
            }
            model.CitiesList = this.populator.GetCities();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();
            model.CitiesList = this.populator.GetCities();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(InputModel model)
        {
            var dbModel = base.Create<DbModel>(model);

            if (dbModel != null)
            {
                return RedirectToAction("Index");
            }
            model.CitiesList = this.populator.GetCities();
            return View(model);
        }

        [HttpGet]
        public ActionResult ActualDelete(int? id)
        {
            var model = base.GetViewModel<DbModel, DeleteViewModel>(id);

            return PartialView("_DeleteContactPartial", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ActualDelete(DeleteViewModel model)
        {
            base.ActualDelete<DbModel>(model.Id);

            return base.GridOperationAjaxRefreshData();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Contacts.GetById(id) as T;
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Contacts.All().Project().To<TViewModel>();
        }

        protected override string GetReadDataActionUrl()
        {
            return Url.Action("ReadData", "Contacts");
        }
    }
}