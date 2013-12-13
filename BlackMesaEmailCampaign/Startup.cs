using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlackMesaEmailCampaign.Startup))]
namespace BlackMesaEmailCampaign
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
