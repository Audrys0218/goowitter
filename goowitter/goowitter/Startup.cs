using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(goowitter.Startup))]
namespace goowitter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
