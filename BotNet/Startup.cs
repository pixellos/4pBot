using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BotNet.Startup))]
namespace BotNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
