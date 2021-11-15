using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_APP.Startup))]
namespace MVC_APP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
