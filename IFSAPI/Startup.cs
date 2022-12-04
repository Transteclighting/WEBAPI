using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IFSAPI.Startup))]
namespace IFSAPI
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
