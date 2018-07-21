namespace ImdbLite.Web.Controllers
{
    using ImdbLite.Data.UnitOfWork;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using IndexViewModel = ImdbLite.Web.ViewModels.Photos.PhotoIndexViewModel;

    public class PhotosController : BaseEntityController
    {
        public PhotosController(IImdbLiteData data)
            : base(data)
        {
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>() 
            => this.Data.Gallery.All().Project().To<TViewModel>();

        protected override T GetById<T>(object id) 
            => this.Data.Gallery.GetById(id) as T;
    }
}