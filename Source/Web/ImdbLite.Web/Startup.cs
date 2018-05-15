using Microsoft.Owin;
using Ninject;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImdbLite.Web.Startup))]
namespace ImdbLite.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
