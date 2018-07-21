namespace ImdbLite.Web.Controllers
{
    using AutoMapper;
    using ImdbLite.Data.UnitOfWork;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;

    public abstract class AuthorizedUserController : BaseController
    {
        public AuthorizedUserController(IImdbLiteData data)
            : base(data)
        {
        }

        protected abstract T GetById<T>(object id) where T : class;

        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        protected virtual TModel Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : class
            where TViewModel : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);

                return dbModel;
            }
            return null;
        }

        protected void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            this.Data.SaveChanges();
        }
    }
}