using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.Evolution;
using DataModel.LoginModel;
using Bic_Vet_Clinic.Filters;
using Pastel.Evolution;
using Pastel.Evolution.Common;
using DataModel.Result;

namespace Bic_Vet_Clinic.Controllers.Customer
{
    [CustomSessionAttribute]
    public class CustomerController : Controller
    {
        LoginSessionDetails SessLogObj = new LoginSessionDetails();
        Result objRes= new Result();
        // GET: Customer
        [Route("Customer/Customers")]
        public ActionResult Index()
        {
            return View();
        }

        private EvolutionSDK EvolutionSDKInstance(LoginSessionDetails SessLogObj)
        {
            string DbConStr = SessLogObj != null ? SessLogObj.objDb.DbConStr : "";
            string DbCommonConStr = SessLogObj != null ? SessLogObj.objDb.DbCommonConStr : "";
            string SerialNumber = SessLogObj != null ? SessLogObj.objDb.SerialNumber : "";
            string AuthCode = SessLogObj != null ? SessLogObj.objDb.AuthCode : "";
            int branchId = SessLogObj != null ? SessLogObj.objDb.BranchId : 0;
            EvolutionSDK obj = new EvolutionSDK(DbConStr,
                                                DbCommonConStr,
                                                SerialNumber,
                                                AuthCode,
                                                branchId,
                                                SessLogObj.objAgent);
            return obj;
        }

        public ActionResult CustomerList(string searchtext = null, int PageNo = 1)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                EvolutionSDK objEvol = EvolutionSDKInstance(SessLogObj);
                string criteria = searchtext != null && searchtext != "" ? "Account like '%" + searchtext + "%' OR Name like '%" + searchtext + "%'" : "1=1";
                var dbresult = objEvol.customerList(criteria);
                return PartialView(dbresult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult CustomerListJson()
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                EvolutionSDK objEvol = EvolutionSDKInstance(SessLogObj);
                var dbresult = objEvol.customerList();
                return Json(new { aaData = dbresult }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult AddCustomer()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult AddCustomer(Pastel.Evolution.Customer obj)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                EvolutionSDK objEvol = EvolutionSDKInstance(SessLogObj);
                string strxml = null;
                var dbresult = objEvol.customerList("cIDNumber = '" + obj.IDNumber+"'");
                if (dbresult == null)
                {
                    obj = objEvol.addCustomer(obj);
                    strxml = (obj != null && obj.Code != "")
                    ? "<SYSMSGS><ID>1</ID><ERRORMSGS>Succefully Added</ERRORMSGS><TYPE>S</TYPE><TITLE>Customer</TITLE><EXTRA>"+obj.Code+"</EXTRA></SYSMSGS>"
                    : "<SYSMSGS><ID>-1</ID><ERRORMSGS>Error In Adding Record</ERRORMSGS><TYPE>E</TYPE><TITLE>Customer</TITLE><EXTRA>" + obj.Code + "</EXTRA></SYSMSGS>";
                }
                else
                {
                    strxml =  "<SYSMSGS><ID>-1</ID><ERRORMSGS>Customer With This National Id Already Exist !</ERRORMSGS><TYPE>W</TYPE><TITLE>Warning !</TITLE></SYSMSGS>";
                }
                objRes = objRes.ReadBIErrors(strxml);
                return Json(objRes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult EditCustomer(string code)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                EvolutionSDK objEvol = EvolutionSDKInstance(SessLogObj);
                Pastel.Evolution.Customer objcust = objEvol.getCustomer(code);
                return PartialView(objcust);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult EditCustomer(Pastel.Evolution.Customer obj)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                EvolutionSDK objEvol = EvolutionSDKInstance(SessLogObj);
                obj = objEvol.editCustomer(obj);
                string strxml =
                    (obj != null && obj.Code != "")
                    ? "<SYSMSGS><ID>1</ID><ERRORMSGS>Succefully Added</ERRORMSGS><TYPE>S</TYPE><TITLE>Pet</TITLE></SYSMSGS>"
                    : "<SYSMSGS><ID>-1</ID><ERRORMSGS>Error In Ading Record</ERRORMSGS><TYPE>E</TYPE><TITLE>Pet</TITLE></SYSMSGS>";

                Result rseXml = objRes.ReadBIErrors(strxml);
                return Json(rseXml, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult CheckCustomerUnique(string nationalId)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                EvolutionSDK objEvol = EvolutionSDKInstance(SessLogObj);
                var dbresult = objEvol.customerList();
                return Json(dbresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}