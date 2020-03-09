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

namespace BIC_Web_Services.Controllers.Pet
{
    [CustomSessionAttribute]
    public class PetController : Controller
    {
        LoginSessionDetails SessLogObj = new LoginSessionDetails();
        Result objRes;

        // GET: Pet
        public ActionResult PetList(string custCode)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                DataModel.Master.pet objp = new DataModel.Master.pet();
                var dbresult = objp.getPetList(0,0,"", custCode,SessLogObj.objDb.BranchId,SessLogObj.objAgent.ID);
                return Json(new { aaData = dbresult }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
    }
}