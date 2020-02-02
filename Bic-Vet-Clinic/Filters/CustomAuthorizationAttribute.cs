using System.Web.Mvc;

namespace Bic_Vet_Clinic.Filters
{
    public class CustomAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        //Authorization filter for checking the user is authenticated user or not 
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            //LoginModels objLog = new LoginModels();
            //LoginSessionDetails objLogSession = new DataModel.LoginSessionDetails();
            //object UserSession = filterContext.RequestContext.HttpContext.Session["SessionInformation"];

            //var path = filterContext.HttpContext.Request.Path;
            //var verb = filterContext.HttpContext.Request.HttpMethod;

            //// these values combined are our roleName
            //string[] Parametrs = path.Split('/');// String.Format("{0}/{1}", path, verb);
            //string currentAction = "";
            //string currentController = "";
            //string MENUID = "0";
            //string DOCID = "0";

            //if (Parametrs.Count() > 0)
            //{
            //    currentAction = Parametrs[1];
            //    currentController = Parametrs[2];
            //    MENUID = Parametrs[3];
            //    if (Parametrs.Count() > 4)
            //    {
            //        DOCID = Parametrs[4];
            //    }
            //}
            //string currentAction = filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString(); 
            //string currentController = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            //string MENUID = "0";
            //string DOCID = "";
            //if (currentAction != "Index" && currentController != "Home")
            //{
            //    if (filterContext.Controller.ControllerContext.RouteData.Values["id"].ToString() != null)
            //    {
            //        MENUID = filterContext.Controller.ControllerContext.RouteData.Values["id"].ToString();
            //    }
            //    else
            //    {
            //        MENUID = "0";
            //    }
            //} 
            //if (UserSession == null || ((DataModel.LoginSessionDetails)UserSession).UserId == 0)
            //{
            //    if (filterContext.HttpContext.Request.Cookies["rem_user"] != null)
            //    {
            //        var rem = filterContext.HttpContext.Request.Cookies.AllKeys.Contains("RememberMe") == false ? "False" : (filterContext.HttpContext.Request.Cookies["RememberMe"].Value).ToString();

            //        if (rem != "False")
            //        {
            //            var objLogin = new DataModel.LoginModel.LoginModels();
            //            objLogin.RememberMe = true;
            //            var userdecrypt = objLogin.Decryptdata(filterContext.HttpContext.Request.Cookies["rem_user"].Value);
            //            string[] strTemp = userdecrypt.Split(',');
            //            objLogin.UserName = strTemp[0];
            //            objLogin.Password = strTemp[1];

            //            string[] user;
            //            if (filterContext.HttpContext.Request.LogonUserIdentity.Name.Contains("\\"))
            //            {
            //                user = filterContext.HttpContext.Request.LogonUserIdentity.Name.Replace("\\\\", "\\").Split('\\');
            //            }
            //            else
            //            {
            //                user = new string[2] { "", filterContext.HttpContext.Request.LogonUserIdentity.Name.Replace("\\\\", "\\").ToString() };

            //            }

            //            string username = user[1];
            //            string mc = Environment.MachineName;
            //            string domainName = user[0];
            //            string bTyp = filterContext.HttpContext.Request.Browser.Type;
            //            string bVer = filterContext.HttpContext.Request.Browser.Version;
            //            /*GET IP*/
            //            string localIP = "";
            //            IPHostEntry host = host = Dns.GetHostEntry(Dns.GetHostName());
            //            foreach (IPAddress ip in host.AddressList)
            //            {
            //                if (ip.AddressFamily == AddressFamily.InterNetwork)
            //                {
            //                    localIP = ip.ToString();
            //                    break;
            //                }
            //            }

            //            bool ISDomain = false;
            //            string emailaddress = "";
            //            string[] aduname = objLogin.UserName.Split('\\');
            //            if (aduname.Length > 1)
            //            {
            //                var userdata = objLogin.ADIsValid(aduname[0], aduname[1], objLogin.Password);
            //                ISDomain = userdata.Item1;
            //                emailaddress = userdata.Item2;
            //            }

            //            string strErrorMsg = "";
            //            objLogSession = objLog.GETLOGININFO(0, objLogin, mc, username, domainName, bTyp, bVer, localIP, (ISDomain ? "1" : "0"), emailaddress, out strErrorMsg);
            //            if (objLogSession.UserId != 0)
            //            {
            //                filterContext.RequestContext.HttpContext.Session["SessionInformation"] = objLogSession;
            //            }
            //            else
            //            {
            //                UrlHelper h = new UrlHelper(HttpContext.Current.Request.RequestContext);
            //                filterContext.Result = new RedirectResult(h.Action("Index", "Home", new { returnUrl = (filterContext.HttpContext.Request).RawUrl }).ToString());
            //            }

            //        }
            //    }
            //    else
            //    {

            //        UrlHelper h = new UrlHelper(HttpContext.Current.Request.RequestContext);
            //        filterContext.Result = new RedirectResult(h.Action("Login", "Account", new { returnUrl = (filterContext.HttpContext.Request).RawUrl }).ToString());
            //    }
            //}
            //else
            //{
            //    if (currentAction != "Index" && currentController != "Home")
            //    {
            //        if (!CheckURL.CheckURLACTION(1, currentController, currentAction, ((LoginSessionDetails)UserSession).UserId, Convert.ToInt32(MENUID), Convert.ToInt32(DOCID), ((LoginSessionDetails)UserSession).PLANTID, ((LoginSessionDetails)UserSession).DEPTID, ((LoginSessionDetails)UserSession).DESIGID, ((LoginSessionDetails)UserSession).CONTROLNO, ((LoginSessionDetails)UserSession).LOGXML))
            //        {
            //            //TempData["Msg"] = "This User is not Authorized to this action.";
            //            UrlHelper h = new UrlHelper(HttpContext.Current.Request.RequestContext);

            //            filterContext.Controller.ViewData["status"] = -1;
            //            filterContext.Controller.ViewData["Msg"] = "You are not authorize to view this page";
            //            filterContext.Result = new RedirectResult(h.Action("Index", "Home", new { returnUrl = "" }));

            //        }
            //    }
            //}
            //filterContext.Controller.ViewBag.OnAuthorization = "IAuthorizationFilter.OnAuthorization filter called";
        }
    }

}