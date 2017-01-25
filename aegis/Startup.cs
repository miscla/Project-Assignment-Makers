using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(aegis.Startup))]
namespace aegis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
