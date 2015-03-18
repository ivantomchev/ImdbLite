namespace ImdbLite.Web.Areas.Administration.ViewModels
{
    using System.IO;
    using System.Web;
    using System.Web.WebPages.Html;

    using AutoMapper;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Common;
    using System;

    public class AutomapperMappingRegister : IHaveCustomMappings
    {
        public void CreateMappings(IConfiguration configuration)
        {
            //DropDowns
            //configuration.CreateMap<Celebrity, SelectListItem>()
            //   .ForMember(d => d.Text, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
            //   .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Id.ToString()));

            //configuration.CreateMap<Genre, SelectListItem>()
            //    .ForMember(d => d.Text, opt => opt.MapFrom(s => s.Name))
            //    .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Id.ToString()));

            //HhhtpPostedFileBase -> Photos
            configuration.CreateMap<HttpPostedFileBase, MoviePoster>()
                .ForMember(d => d.Content, opt => opt.ResolveUsing(s =>
                {
                    MemoryStream target = new MemoryStream();
                    s.InputStream.CopyTo(target);
                    return target.ToArray();
                }))
                .ForMember(d => d.FileExtension, opt => opt.MapFrom(s => Path.GetExtension(s.FileName)))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.ContentType));

            configuration.CreateMap<HttpPostedFileBase, CelebrityMainPhoto>()
                .ForMember(d => d.Content, opt => opt.ResolveUsing(s =>
                {
                    MemoryStream target = new MemoryStream();
                    s.InputStream.CopyTo(target);
                    return target.ToArray();
                }))
                .ForMember(d => d.FileExtension, opt => opt.MapFrom(s => Path.GetExtension(s.FileName)))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.ContentType));

            configuration.CreateMap<HttpPostedFileBase, ArticlePhoto>()
                .ForMember(d => d.Content, opt => opt.ResolveUsing(s =>
                {
                    MemoryStream target = new MemoryStream();
                    s.InputStream.CopyTo(target);
                    return target.ToArray();
                }))
                .ForMember(d => d.FileExtension, opt => opt.MapFrom(s => Path.GetExtension(s.FileName)))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.ContentType));

        }
    }
}