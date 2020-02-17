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
                string criteria = searchtext !=null && searchtext!=""? "Account like '%" + searchtext + "%' OR Name like '%" + searchtext + "%'":"1=1";
                List<Pastel.Evolution.Customer> objcustList = objEvol.customerList(criteria);
                ViewBag.PageNo = PageNo;
                ViewBag.totalpages = objcustList == null ? 0 : objcustList.Count / 10;
                return PartialView(objcustList);
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
                obj = objEvol.addCustomer(obj);
                 
                Result rseXml =new Result(obj.Code == "" ? 0 : 1, "I");
                return Json(rseXml, JsonRequestBehavior.AllowGet);
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
                Result rseXml = new Result(obj.Code == "" ? 0 : 1, "E");
                return Json(rseXml, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}