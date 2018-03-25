using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogisticsProject.Startup))]
namespace LogisticsProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
