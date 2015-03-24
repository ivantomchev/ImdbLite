namespace ImdbLite.Web.Controllers
{
    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Web.Areas.Administration.Controllers.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using DbModel = ImdbLite.Data.Models.Vote;
    using InputModel = ImdbLite.Web.ViewModels.Votes.VoteInputModel;
    using AutoMapper;
    using ImdbLite.Data.Models;

    public class VotesController : AuthorizedUserController
    {
        public VotesController(IImdbLiteData data)
            : base(data)
        {

        }

        public ActionResult GetCurrentUserRating(int Id)
        {
            var model = new InputModel();
            model.MovieId = Id;

            var currentUser = this.CurrentUser;

            if (currentUser != null)
            {
                model = this.Data.Votes.All().Project().To<InputModel>().Where(x => x.MovieId == Id && x.VotedById == currentUser.Id).FirstOrDefault();

            }
            if (model == null)
            {
                model = new InputModel();
                model.MovieId = Id;
            }

            return PartialView("_AddOrUpdateVotePartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdateVote(InputModel model)
        {
            if (this.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Details", "Movies", new { id = model.MovieId }) });
            }

            var isVoted = this.Data.Votes.All().Any(x => x.MovieId == model.MovieId && x.VotedById == this.CurrentUser.Id);

            if (isVoted)
            {
                model.VotedById = CurrentUser.Id;
                var dbModel = base.Update<DbModel, InputModel>(model, model.VoteId);
            }
            else
            {
                model.VotedById = CurrentUser.Id;
                var dbModel = base.Create<DbModel>(model);
            }

            return RedirectToAction("Details", "Movies", new { id = model.MovieId });
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Votes.GetById(id) as T;
        }
    }
}