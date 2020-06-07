using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Marigold_Application.Startup))]
namespace Marigold_Application
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
