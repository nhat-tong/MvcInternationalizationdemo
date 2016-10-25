using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcInternationalizationdemo.Startup))]
namespace MvcInternationalizationdemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
