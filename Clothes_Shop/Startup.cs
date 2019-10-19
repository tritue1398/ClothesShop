using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Clothes_Shop.Startup))]
namespace Clothes_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
