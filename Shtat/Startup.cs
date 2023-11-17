using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shtat.Startup))]
namespace Shtat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
