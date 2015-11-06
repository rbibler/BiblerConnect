using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Humidity.Startup))]
namespace Humidity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
