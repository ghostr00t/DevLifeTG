using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevLifeTG.Startup))]
namespace DevLifeTG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
