namespace ImdbLite.Web.Areas.Administration.Controllers.Base
{
    using System.Data.Entity;
    using System.IO;
    using System.Web.Mvc;
    using System.Web;
    using System.Collections;

    using AutoMapper;

    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Web.Controllers;
    using ImdbLite.Common;

    public abstract class AdminController : BaseEntityController
    {

        // [Authorize(Roles = "Admin")]
        public AdminController(IImdbLiteData data)
            : base(data)
        {
        }

        protected abstract string GetReadDataActionUrl();

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

        protected virtual T Create<T>(object model, HttpPostedFileBase file) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                bool isSuccess = TryUploadPhoto(file);
                if (!isSuccess)
                {
                    return null;
                }
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }
            return null;
        }

        protected virtual T CreateWithOutSave<T>(object model) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
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

        protected virtual TModel Update<TModel, TViewModel>(TViewModel model, object id, HttpPostedFileBase file, string existingPath)
            where TModel : class
            where TViewModel : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                bool isSuccess = TryUpdatePhoto(file, existingPath);
                if (!isSuccess)
                {
                    return null;
                }
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);

                return dbModel;
            }
            return null;
        }

        protected virtual void Delete<TModel>(object model)
            where TModel : class
        {
            this.ChangeEntityStateAndSave(model, EntityState.Modified);
        }

        //TODO Make this return null if fail
        protected virtual void ActualDelete<T>(object id)
            where T : class
        {
            var dbModel = this.GetById<T>(id);
            this.ChangeEntityStateAndSave(dbModel, EntityState.Deleted);
        }

        //TODO Make this return null if fail
        protected virtual void ActualDelete<T>(object id, string fileToDeleteUrl)
            where T : class
        {
            var dbModel = this.GetById<T>(id);

            TryDeleteFile(fileToDeleteUrl);

            this.ChangeEntityStateAndSave(dbModel, EntityState.Deleted);
        }

        protected void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            this.Data.SaveChanges();
        }

        protected JsonResult GridOperation() 
            => Json(new { success = true });

        protected JsonResult GridOperationAjaxRefreshData() 
            => Json(new { success = true, url = this.GetReadDataActionUrl() });

        private bool TryUploadPhoto(HttpPostedFileBase file)
        {
            try
            {
                var fileName = file.GetHashCode() + file.FileName.GetHashCode() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(GlobalConstants.ImageDirectory), fileName);
                file.SaveAs(path);
                return true;
            }
            catch
            {
                ModelState.AddModelError("FileToUpload", "Unable To Upload File.Please Try Again");
                return false;
            }
        }

        private bool TryUpdatePhoto(HttpPostedFileBase file, string path)
        {
            if (file != null)
            {
                try
                {
                    path = Server.MapPath(path);
                    file.SaveAs(path);
                    return true;
                }
                catch
                {
                    ModelState.AddModelError("FileToUpload", "Unable To Update File.Please Try Again");
                    return false;
                }
            }
            return true;
        }

        private void TryDeleteFile(string fileToDeleteUrl)
        {
            var path = Request.MapPath(fileToDeleteUrl);

            if (System.IO.File.Exists(path))
            {
                var file = new FileInfo(path);
                file.Delete();
            }
        }
    }
}