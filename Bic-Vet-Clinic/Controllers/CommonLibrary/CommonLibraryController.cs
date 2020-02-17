using Bic_Vet_Clinic.Filters;
using DataModel.LoginModel;
using System;
using System.Data;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Bic_Vet_Clinic.Controllers.CommonLibrary
{
    [CustomSessionAttribute]
    public class CommonLibraryController : Controller
    {
        LoginSessionDetails SessLogObj = new LoginSessionDetails();
        
        // GET: CommonLibrary
        public ActionResult Index()
        {
            return View();
        }

        //Action to fill itemtype select2
        public JsonResult _FillControlNo(int CONTROLENO, string DESCRIPTION)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                DataModel.CommonModel.CommonLibrary objCommon = new DataModel.CommonModel.CommonLibrary(SessLogObj.objDb.DbConStr);
                var data = objCommon.FillCONTROLNO(CONTROLENO, DESCRIPTION==null?"":DESCRIPTION, SessLogObj.objAgent.ID);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            { 
                throw;
            }
           
        }

        //Action to fill itemtype select2
        public JsonResult _PriceList(int id, string description)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                DataModel.CommonModel.CommonLibrary objCommon = new DataModel.CommonModel.CommonLibrary(SessLogObj.objDb.DbConStr);
                var data = objCommon.fillPriceList(SessLogObj.objDb.DbConStr, SessLogObj.objDb.DbCommonConStr, SessLogObj.objDb.SerialNumber, SessLogObj.objDb.AuthCode);
                 
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}