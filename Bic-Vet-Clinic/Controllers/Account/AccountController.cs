using DataModel.LoginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pastel.Evolution;
using DataModel.Database;
using System.Web.Security;
using DataModel.Evolution;

namespace BIC_Web_Services.Controllers.Account
{
    public class AccountController : Controller
    {
        LoginSessionDetails SessLogObj = new LoginSessionDetails();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //Login page for all users
        public ActionResult Login()
        {
            object UserSession = Session["SessionInformation"];

            if (UserSession != null && ((LoginSessionDetails)UserSession).objAgent.ID != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //Login Post action for getting authenticating user and maintaining its session
        [HttpPost]
        public ActionResult Login(LoginModels objLogin)
        {
            try
            {
                Response.Cookies["RememberMe"].Value = objLogin.rememberMe.ToString();
                if (objLogin.rememberMe)
                {
                    var userencrypt = string.Join(",", objLogin.userName, objLogin.password, objLogin.rememberMe);
                    Response.Cookies["rem_user"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["rem_user"].Value = objLogin.Encryptdata(userencrypt);

                    Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["RememberMe"].Value = objLogin.rememberMe.ToString();
                }
                else
                {
                    Response.Cookies["rem_user"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(-1);
                }


                if (ModelState.IsValid)
                {
                    if (objLogin.DbConfigId > 0)
                    {
                        dbConfig objDb = new dbConfig();
                        objDb = objDb.getDatabse(0, "", 0);
                        if (objDb != null && objDb.DbConfigId > 0)
                        {
                            LoginModels obj = new LoginModels();
                            Agent objAgent = obj.validateAget(objLogin.userName, objLogin.password, objDb.DbConStr, objDb.DbCommonConStr, objDb.SerialNumber, objDb.AuthCode);

                            if (objAgent != null && objAgent.ID != 0)
                            {
                                int isAdmin = AgentGroup.Find("idAgentGroups IN (SELECT iGroupID FROM  dbo.[_rtblAgentGroupMembers] WHERE iAgentID = " + Convert.ToString(objAgent.ID) + ") AND cGroupName='" + objDb.AdminGroup + "'");
                                int isReceptionist = AgentGroup.Find("idAgentGroups IN (SELECT iGroupID FROM  dbo.[_rtblAgentGroupMembers] WHERE iAgentID = " + Convert.ToString(objAgent.ID) + ") AND cGroupName='" + objDb.UserReceptionistGroup + "'");
                                int isDoctor = AgentGroup.Find("idAgentGroups IN (SELECT iGroupID FROM  dbo.[_rtblAgentGroupMembers] WHERE iAgentID = " + Convert.ToString(objAgent.ID) + ") AND cGroupName='" + objDb.UserDoctorGroup + "'");
                                int isCashier = AgentGroup.Find("idAgentGroups IN (SELECT iGroupID FROM  dbo.[_rtblAgentGroupMembers] WHERE iAgentID = " + Convert.ToString(objAgent.ID) + ") AND cGroupName='" + objDb.UserCashierGroup + "'");

                                if (isAdmin > 0 || isReceptionist > 0 || isDoctor > 0 || isCashier > 0)
                                {
                                    objDb.IsAdmin = isAdmin > 0 ? true : false;
                                    objDb.IsReceptionist = isReceptionist > 0 ? true : false;
                                    objDb.IsDoctor = isDoctor > 0 ? true : false;
                                    objDb.IsCashier = isCashier > 0 ? true : false;

                                    LoginSessionDetails objLogSession = new LoginSessionDetails();
                                    objLogSession.objDb = objDb;
                                    objLogSession.objAgent = objAgent;

                                    Session["SessionInformation"] = objLogSession;
                                    string time = DateTime.Now.AddMinutes(1).ToString("mm.ss");
                                    Session["ReminderTime"] = time;
                                    return RedirectToAction("Index", "Home", new { returnUrl = (this.HttpContext.Request).Path });
                                }
                                else
                                {
                                    ModelState.AddModelError("ErrorMgr", "Invalid Agent or Password!");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("ErrorMgr", "Invalid Agent or Password!");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("ErrorMgr", "This branch is not configured with BIC App");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorMgr", "This branch is not configured with BIC App");
                    }

                }
                else
                {
                    ModelState.AddModelError("ErrorMgr", "Error in login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objLogin);
        }
        //Login page for all users
        public ActionResult DefaultLogin()
        {
            object UserSession = Session["SessionInformation"];

            if (UserSession != null && ((LoginSessionDetails)UserSession).objLoginM.UserId != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //Login Post action for getting authenticating user and maintaining its session
        [HttpPost]
        public ActionResult DefaultLogin(LoginModels objLogin)
        {
            try
            {
                Response.Cookies["RememberMe"].Value = objLogin.rememberMe.ToString();
                if (objLogin.rememberMe)
                {
                    var userencrypt = string.Join(",", objLogin.userName, objLogin.password, objLogin.rememberMe);
                    Response.Cookies["rem_user"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["rem_user"].Value = objLogin.Encryptdata(userencrypt);

                    Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["RememberMe"].Value = objLogin.rememberMe.ToString();
                }
                else
                {
                    Response.Cookies["rem_user"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(-1);
                }


                if (ModelState.IsValid)
                {
                    LoginModels obj = new LoginModels();


                    obj = obj.getLoginInfo(objLogin);

                    if (obj != null)
                    {
                        LoginSessionDetails objLogSession = new LoginSessionDetails();
                        objLogSession.objLoginM = obj;
                        Session["SessionInformation"] = objLogSession;
                        if (obj.IsAdmin)
                        {
                            return RedirectToAction("Admin", "Account", new { returnUrl = (this.HttpContext.Request).Path });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Account", new { returnUrl = (this.HttpContext.Request).Path });
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("ErrorMgr", "Invalid Agent or Password!");
                    }


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objLogin);
        }

        public ActionResult Admin()
        {
            try
            {

                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                dbConfig obj = new dbConfig();

                List<dbConfig> objListdbConfig = obj.getdbList(0, "", SessLogObj.objLoginM.UserId, 1);
                return View(objListdbConfig);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public ActionResult DefLogOff()
        {
            SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
            this.Response.Cookies["rem_user"].Expires = DateTime.Now.AddDays(-1);
            this.Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(-1);
            this.Response.Cookies["RememberMe"].Value = "False";
            this.Response.Cookies["rem_user"].Value = "False";
            this.Response.Cookies.Remove("rem_user");
            this.Response.Cookies.Remove("RememberMe");


            if (Request.Cookies["RememberMe"] != null)
            {
                var c = new HttpCookie("RememberMe");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            if (Request.Cookies["rem_user"] != null)
            {
                var c = new HttpCookie("rem_user");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                var c = new HttpCookie("ASP.NET_SessionId");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            //Result rseXml = objLog.LOGOUT(SessLogObj.LOGINLOGID, SessLogObj.LOGXML);

            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("DefaultLogin", "Account");
        }


        public ActionResult LogOff()
        {
            SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
            this.Response.Cookies["rem_user"].Expires = DateTime.Now.AddDays(-1);
            this.Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(-1);
            this.Response.Cookies["RememberMe"].Value = "False";
            this.Response.Cookies["rem_user"].Value = "False";
            this.Response.Cookies.Remove("rem_user");
            this.Response.Cookies.Remove("RememberMe");


            if (Request.Cookies["RememberMe"] != null)
            {
                var c = new HttpCookie("RememberMe");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            if (Request.Cookies["rem_user"] != null)
            {
                var c = new HttpCookie("rem_user");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                var c = new HttpCookie("ASP.NET_SessionId");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            //Result rseXml = objLog.LOGOUT(SessLogObj.LOGINLOGID, SessLogObj.LOGXML);

            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        //Action to fill itemtype select2
        public JsonResult _FillBranch(int branchId, string description)
        {
            try
            {
                dbConfig obj = new dbConfig();
                List<dbConfig> data = obj.getdbList(0,"", 0);
                string DbConStr = (data != null && data.Count>0) ? data.FirstOrDefault().DbConStr : "";
                string DbCommonConStr = (data != null && data.Count > 0) ? data.FirstOrDefault().DbCommonConStr : "";
                string SerialNumber = (data != null && data.Count > 0) ? data.FirstOrDefault().SerialNumber : "";
                string AuthCode = (data != null && data.Count > 0) ? data.FirstOrDefault().AuthCode : "";

                EvolutionSDK objEvol = new EvolutionSDK(DbConStr,DbCommonConStr,SerialNumber,AuthCode);
                List<Branch> branchList = objEvol.branchList("cBranchCode like '%"+description+"%'");
                var dbResult = branchList != null ? (from row in branchList
                                                     select new
                                                     {
                                                         id = row.ID,
                                                         text = row.Code + '-' + row.Description
                                                     }).ToList() : null;
                return Json(dbResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex; ;
            }

        }

    }
}