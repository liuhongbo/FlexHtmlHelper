using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlexHtmlHelper.WebSite.Startup))]
namespace FlexHtmlHelper.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
