using System.Web.Mvc;
using System.Web.Routing;

namespace Bic_Vet_Clinic.Filters
{
    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter
    { 
        public void OnException(ExceptionContext filterContext)
        {

            filterContext.ExceptionHandled = true;

            // Redirect on error:
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                action = "ErrorHandler",
                controller = "ExceptionHandler",
                Exception = filterContext.Exception.Message.ToString(),
                RequestType = filterContext.HttpContext.Request.IsAjaxRequest(),
            }));
        }
    }
}