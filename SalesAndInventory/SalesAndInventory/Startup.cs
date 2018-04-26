using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalesAndInventory.Startup))]
namespace SalesAndInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
