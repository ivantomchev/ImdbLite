namespace ImdbLite.Web.ViewModels.Articles
{
    using AutoMapper;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ArticleIndexViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name="by")]
        public string WrittenBy { get; set; }

        public ArticlePhoto Photo { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}