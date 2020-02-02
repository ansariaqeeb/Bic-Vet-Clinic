using System.Web.Mvc;
using System.Web.Routing;

namespace Bic_Vet_Clinic
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
               "Transaction", // Route name
               "{controller}/{action}/{id}/{*DMID}", // URL with parameters
               new { controller = "Account", action = "LogOn", id = UrlParameter.Optional, DMID = UrlParameter.Optional } // Parameter defaults
           );
        }
    }
}
