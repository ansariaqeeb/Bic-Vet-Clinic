using Bic_Vet_Clinic.Filters;
using DataModel.CommonModel;
using DataModel.LoginModel;
using DataModel.Master;
using DataModel.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Bic_Vet_Clinic.Controllers.HolidayCalendar
{
    [CustomSessionAttribute]
    public class HolidayCalendarController : Controller
    {
        LoginSessionDetails SessLogObj = new LoginSessionDetails();

        // GET: HolidayCalendar
        [Route("HolidayCalendar/Holidays")]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult HolidayCalendarList(int ControlNo = 0, string searchtext = null, int PageNo = 1)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                DataModel.CommonModel.CommonLibrary objCommon = new DataModel.CommonModel.CommonLibrary(SessLogObj.objDb.DbConStr);
                string FILTERXML = "<FILTERXML CONTROLNO=\"" + ControlNo + "\" SearchText=\"" + searchtext + "\" />";
                DataTable dt = objCommon.GetLookup(1, 0, SessLogObj.objAgent.ID, PageNo, XElement.Parse(FILTERXML), null);

                List<holidayCalendar> modelList = (dt != null && dt.Rows.Count > 0) ?
                    (from s in dt.AsEnumerable()
                     select new holidayCalendar
                     {
                         MID = s.Field<int>("MID"),
                         HolidayDate = s.Field<DateTime>("holidayDate"),
                         Description = s.Field<string>("Description"),
                         IsActive = s.Field<bool>("IsActive"),
                         Year = s.Field<int>("Year"),
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
        public ActionResult AddHoliday()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult AddHoliday(holidayCalendar obj)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                holidayCalendar objn = new holidayCalendar(SessLogObj.objDb.DbConStr);
                obj.HFlag = obj.MID == 0 ? "I" : "E";
                Result rseXml = objn.Save(SessLogObj.objAgent.ID, obj);
                return Json(rseXml, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult EditHoliday(int mid, int year, DateTime holidayDate, string description)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                holidayCalendar objn = new holidayCalendar("");
                objn.MID = mid;
                objn.Year = year;
                objn.HolidayDate = holidayDate;
                objn.Description = description;

                return PartialView(objn);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        } 

        public JsonResult DeleteHoliday(int MID)
        {
            try
            {
                SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
                holidayCalendar objn = new holidayCalendar(SessLogObj.objDb.DbConStr);
                objn.MID = MID;
                objn.HFlag = "D";
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