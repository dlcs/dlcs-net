using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iiifly.Startup))]
namespace iiifly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
