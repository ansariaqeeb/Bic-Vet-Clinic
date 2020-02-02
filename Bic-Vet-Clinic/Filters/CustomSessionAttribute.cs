using DataModel.LoginModel;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Bic_Vet_Clinic.Filters
{
    public class CustomSessionAttribute : FilterAttribute, IAuthorizationFilter
    {
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            LoginModels objLog = new LoginModels();
            object UserSession = filterContext.RequestContext.HttpContext.Session["SessionInformation"];

            if (UserSession == null || ((LoginSessionDetails)UserSession).objAgent.ID == 0)
            {
                try
                {
                    if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                    {
                        filterContext.HttpContext.Session.RemoveAll();
                        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                        LoginSessionDetails serializeModel = JsonConvert.DeserializeObject<LoginSessionDetails>(authTicket.UserData);
                        if (serializeModel.objAgent.ID > 0)
                        {
                            filterContext.HttpContext.Session["SessionInformation"] = serializeModel;
                        }
                        else
                        {
                            UrlHelper h = new UrlHelper(HttpContext.Current.Request.RequestContext);
                            filterContext.Result = new RedirectResult(h.Action("DefaultLogin", "Account", new { returnUrl = (filterContext.HttpContext.Request).RawUrl }).ToString());
                        }
                    }
                    else
                    {
                        UrlHelper h = new UrlHelper(HttpContext.Current.Request.RequestContext);
                        filterContext.Result = new RedirectResult(h.Action("DefaultLogin", "Account", new { returnUrl = (filterContext.HttpContext.Request).RawUrl }).ToString());
                    }
                }
                catch (Exception ex)
                {
                    UrlHelper h = new UrlHelper(HttpContext.Current.Request.RequestContext);
                    filterContext.Result = new RedirectResult(h.Action("DefaultLogin", "Account", new { returnUrl = (filterContext.HttpContext.Request).RawUrl }).ToString());
                }

            }
        }
    }
}