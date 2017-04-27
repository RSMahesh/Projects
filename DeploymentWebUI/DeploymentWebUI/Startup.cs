using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeploymentWebUI.Startup))]
namespace DeploymentWebUI
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
