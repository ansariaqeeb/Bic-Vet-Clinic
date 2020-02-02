using Bic_Vet_Clinic.Filters;
using DataModel.LoginModel;
using DataModel.Master;
using DataModel.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Bic_Vet_Clinic.Controllers.WorkingCalendar
{
    [CustomSessionAttribute]
    public class WorkdayCalendarController : Controller
    {
        LoginSessionDetails SessLogObj = new LoginSessionDetails();
        [Route("WorkdayCalendar/Workday")]
        // GET: WorkdayCalendar
        public ActionResult Index()
        {
            return View();
        } 
        public ActionResult WorkingCalendarList(int ControlNo = 0, string searchtext = null, int PageNo = 1)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                DataModel.CommonModel.CommonLibrary objCommon = new DataModel.CommonModel.CommonLibrary(SessLogObj.objDb.DbConStr);

                string FILTERXML = "<FILTERXML CONTROLNO =\"" + ControlNo + "\" />";
                DataTable dt = objCommon.GetLookup(2, 0, SessLogObj.objAgent.ID, PageNo, XElement.Parse(FILTERXML));

                 
                List<workingCalendar> modelList = (dt != null && dt.Rows.Count > 0) ?
                    (from s in dt.AsEnumerable()
                     select new workingCalendar
                     {
                         MID = s.Field<int>("MID"),
                         FromDate = s.Field<DateTime>("FromDate"),
                         ToDate = s.Field<DateTime>("ToDate"), 
                         WorkingDays = s.Field<string>("WorkingDays"),
                         TotalRows = s.Field<int>("TotalRows"),
                         TotalPages = Convert.ToInt32(s.Field<decimal>("TotalPages")),
                         PageSize = s.Field<int>("PageSize")
                     }).ToList() : null;


                ViewBag.PageNo = PageNo;
                ViewBag.totalpages = modelList == null ? 0 : modelList.FirstOrDefault().TotalPages;
                return PartialView(modelList);
            }
            catch (Exception ex)
            {
                throw;
            } 
        }
        public ActionResult AddWorkCalendar()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult AddWorkCalendar(workingCalendar obj)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                workingCalendar objn = new workingCalendar(SessLogObj.objDb.DbConStr);
                obj.WFlag = obj.MID == 0 ? "I" : "E";
                Result rseXml = objn.Save(SessLogObj.objAgent.ID, obj);
                return Json(rseXml, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult EditWorkCalendar(int mid)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"]; 
                workingCalendar obj = new workingCalendar(SessLogObj.objDb.DbConStr);
                List<workingCalendar> model = obj.getWorkingCalendarList(1, mid, 0);
                obj = model != null ? model.FirstOrDefault():  null; 
                return PartialView(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JsonResult DeleteWorkCalendar(int MID)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                workingCalendar objn = new workingCalendar(SessLogObj.objDb.DbConStr);
                objn.MID = MID;
                objn.WFlag = "D";
                Result rseXml = objn.Save(SessLogObj.objAgent.ID, objn);
                return Json(rseXml, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}