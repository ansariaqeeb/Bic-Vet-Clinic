using Bic_Vet_Clinic;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Bic_Vet_Clinic
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var hubConfiguration = new HubConfiguration();
            //hubConfiguration.EnableDetailedErrors = true;
            //app.MapSignalR();
        }
    }
}