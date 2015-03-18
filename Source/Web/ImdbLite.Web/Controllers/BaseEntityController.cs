namespace ImdbLite.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using ImdbLite.Data.Models;
    using ImdbLite.Data.UnitOfWork;
    using System.Collections;
    using AutoMapper;

    public abstract class BaseEntityController : BaseController
    {
        public BaseEntityController(IImdbLiteData data)
            : base(data)
        {
        }

        protected abstract IQueryable<TViewModel> GetData<TViewModel>() where TViewModel : class;

        protected abstract T GetById<T>(object id) where T : class;

        protected virtual TViewModel GetViewModel<TModel, TViewModel>(object id)
            where TModel : class
            where TViewModel : class
        {
            if (id == null)
            {
                return null;
            }

            var dbModel = this.GetById<TModel>(id);

            if (dbModel == null)
            {
                return null;
            }

            var model = Mapper.Map<TViewModel>(dbModel);

            return model;
        }
    }
}