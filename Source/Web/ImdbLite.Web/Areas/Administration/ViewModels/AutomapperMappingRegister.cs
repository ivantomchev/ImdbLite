namespace ImdbLite.Web.Areas.Administration.ViewModels
{
    using System.IO;
    using System.Web;

    using AutoMapper;

    using ImdbLite.Data.Models;
    using ImdbLite.Services.Data.DTOs;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class AutomapperMappingRegister : IHaveCustomMappings
    {
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<HttpPostedFileBase, FileDTO>()
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