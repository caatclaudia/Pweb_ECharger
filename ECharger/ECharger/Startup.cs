using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECharger.Startup))]
namespace ECharger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
