﻿namespace ImdbLite.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ImdbLite.Web.Areas.Administration.Controllers.Base;
    using ImdbLite.Data.UnitOfWork;

    using DbModel = ImdbLite.Data.Models.Celebrity;
    using IndexViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Celebrities.CelebrityIndexViewModel;
    using InputModel = ImdbLite.Web.Areas.Administration.ViewModels.Celebrities.CelebrityInputModel;
    using UpdateModel = ImdbLite.Web.Areas.Administration.ViewModels.Celebrities.CelebrityUpdateModel;
    using DeleteViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Celebrities.CelebrityDeleteViewModel;
    using ImdbLite.Web.Infrastructure.Caching;


    public class CelebritiesController : AdminController
    {
        private readonly ICacheService service;
        private const int PageSize = 10;

        public CelebritiesController(IImdbLiteData data, ICacheService service)
            : base(data)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            var data = GetData<IndexViewModel>();

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

            return PartialView("_ReadCelebritiesPartial", data);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            var model = base.GetViewModel<DbModel, UpdateModel>(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateModel model)
        {
            var dbModel = base.Update<DbModel, UpdateModel>(model, model.Id);
            if (dbModel != null)
            {
                this.ClearCelebritiesCache();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(InputModel model)
        {
            var dbModel = base.Create<DbModel>(model);

            if (dbModel != null)
            {
                this.ClearCelebritiesCache();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult ActualDelete(int? id)
        {
            var model = base.GetViewModel<DbModel, DeleteViewModel>(id);

            return PartialView("_DeleteCelebrityPartial", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ActualDelete(DeleteViewModel model)
        {
            base.ActualDelete<DbModel>(model.Id);
            this.ClearCelebritiesCache();

            return base.GridOperationAjaxRefreshData();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Celebrities.GetById(id) as T;
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Celebrities.All().Project().To<TViewModel>();
        }

        protected override string GetReadDataActionUrl()
        {
            return Url.Action("ReadData", "Celebrities");
        }

        private void ClearCelebritiesCache()
        {
            this.service.Clear("Celebrities");
        }
    }
}