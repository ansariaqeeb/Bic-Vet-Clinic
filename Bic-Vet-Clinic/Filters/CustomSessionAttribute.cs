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
            try
            {
                LoginModels objLog = new LoginModels();
                object UserSession = filterContext.RequestContext.HttpContext.Session["SessionInformation"];

                if (UserSession == null || ((LoginSessionDetails)UserSession).objAgent.ID == 0)
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
                            filterContext.Result = new RedirectResult(h.Action("Index", "Home").ToString());
                        }
                    }
                    else
                    {
                        UrlHelper h = new UrlHelper(HttpContext.Current.Request.RequestContext);
                        filterContext.Result = new RedirectResult(h.Action("Login", "Account").ToString());
                    }
                }

            }
            catch (Exception)
            {
                UrlHelper h = new UrlHelper(HttpContext.Current.Request.RequestContext);
                filterContext.Result = new RedirectResult(h.Action("Login", "Account").ToString());
            }

        }
    }
}