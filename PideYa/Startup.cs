using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PideYa.Startup))]
namespace PideYa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
