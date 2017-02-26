using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InstPublish.Startup))]
namespace InstPublish
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
