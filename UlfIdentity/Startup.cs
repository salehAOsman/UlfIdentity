using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UlfIdentity.Startup))]
namespace UlfIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
