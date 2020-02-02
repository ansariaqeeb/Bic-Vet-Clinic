using Bic_Vet_Clinic.App_Start;
using Bic_Vet_Clinic.Filters;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Bic_Vet_Clinic
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        { 
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
             

            //Added By Amol N
            AreaRegistration.RegisterAllAreas(); 
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //GlobalFilters.Filters.Add(new CustomExceptionAttribute());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
