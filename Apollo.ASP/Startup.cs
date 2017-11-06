using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Apollo.ASP.Startup))]
namespace Apollo.ASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
