using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Marigold.Startup))]
namespace Marigold
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
