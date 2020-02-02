using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DataModel.LoginModel;
using DataModel.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bic_Vet_Clinic.Controllers.Reports
{
    public class RenderReportController : Controller
    {
        LoginSessionDetails SessLogObj = new LoginSessionDetails();
        ReportDocument rd = new ReportDocument();
        // GET: RenderReport
        public ActionResult Index()
        {
            return View();
        }
        //[ValidateInput(false)]
        //public void RunReport(int MID, string reportName = "", bool PRINTMODE = false, int EXPORTTYPE = 1)
        //{
        //    try
        //    {

        //        SessLogObj = (LoginSessionDetails)HttpContext.Session["SessionInformation"];
        //        InvoiceMaster obj = new InvoiceMaster(SessLogObj.objDb.DbConStr);
        //        obj = obj.getInvoiceList(1, MID, 0).FirstOrDefault();


        //        if (obj != null)
        //        {
        //            DataTable invoice = new DataTable();
        //            DataTable InvcTrans = new DataTable();

        //            invoice.Columns.Add("INVCNUMB", typeof(string));
        //            invoice.Columns.Add("PROINVCNUMB", typeof(string));

        //            invoice.Columns.Add("INVCDATE", typeof(string));
        //            invoice.Columns.Add("VOYAGENO", typeof(string));
        //            invoice.Columns.Add("VESSEL", typeof(string));
        //            invoice.Columns.Add("CUSTOMER", typeof(string));
        //            invoice.Columns.Add("ETA", typeof(string));
        //            invoice.Columns.Add("ETD", typeof(string));
        //            invoice.Columns.Add("ADDRESS", typeof(string));
        //            invoice.Columns.Add("CURRENCY", typeof(string));

        //            invoice.Columns.Add("TOTALINCLUSIVE", typeof(string));
        //            invoice.Columns.Add("TOTALTAX", typeof(string));
        //            invoice.Columns.Add("TOTALEXCLUSIVE", typeof(string));
        //            invoice.Columns.Add("INVCNTITLE", typeof(string));



        //            InvcTrans.Columns.Add("SRNO", typeof(int));
        //            InvcTrans.Columns.Add("TRANSDATE", typeof(string));
        //            InvcTrans.Columns.Add("ISOT", typeof(string));
        //            InvcTrans.Columns.Add("SERVICE", typeof(string));
        //            InvcTrans.Columns.Add("QTY1", typeof(string));
        //            InvcTrans.Columns.Add("QTY2", typeof(string));
        //            InvcTrans.Columns.Add("UOM1", typeof(string));
        //            InvcTrans.Columns.Add("UOM2", typeof(string));
        //            InvcTrans.Columns.Add("RATE", typeof(string));
        //            InvcTrans.Columns.Add("TAXRATE", typeof(string));
        //            InvcTrans.Columns.Add("AMOUNT", typeof(string));
        //            InvcTrans.Columns.Add("TAXAMOUNT", typeof(string));
        //            InvcTrans.Columns.Add("REMARK", typeof(string));
        //            InvcTrans.Columns.Add("DAYTYPEDESC", typeof(string));

        //            if (obj.TransList != null && obj.TransList.Count > 0)
        //            {
        //                foreach (var i in obj.TransList)
        //                {
        //                    InvcTrans.Rows.Add(i.SRNO, i.TRANSDATE.ToString("dd-MM-yyyy"), i.ISOVERTIME, i.ITEMDESC, i.QTY1, i.QTY2, i.UOM1DESC, i.UOM2DESC, i.RATE, i.TAX, i.AMOUNT, i.TAXAMOUNT, i.REMARK, i.DayTypeDesc);
        //                }
        //            }


        //            string title = obj.ISRELEASE == true ? "INVOICE" : "PROFORMA INVOICE";

        //            invoice.Rows.Add(obj.INVCNUMB,
        //                obj.PERINVCNUMB,
        //                obj.INVCDATE.ToString("dd-MMM-yyyy"),
        //                obj.VOYAGENO,
        //                obj.VESSEL,
        //                obj.CUSTOMER,
        //                Convert.ToDateTime(obj.ETA).Date == Convert.ToDateTime("1900/01/01").Date ? "None" : Convert.ToString(obj.ETA.ToString("dd-MM-yyyy")),
        //                Convert.ToDateTime(obj.ETD).Date == Convert.ToDateTime("1900/01/01").Date ? "None" : Convert.ToString(obj.ETD.ToString("dd-MM-yyyy")),
        //                obj.ADDRESSDESC,
        //                obj.CURRENCY,
        //                obj.INCLUSIVE,
        //                obj.TOTALTAXAMOUNT,
        //                obj.TOTALEXCLUSIVE,
        //                title);

        //            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Reports\\" + reportName;
        //            rd.Load(strRptPath);

        //            rd.Database.Tables["InvoiceMast"].SetDataSource(invoice);
        //            rd.Database.Tables["InvoiceTrans"].SetDataSource(InvcTrans);


        //            if (EXPORTTYPE == 1)
        //            {
        //                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, PRINTMODE, reportName);
        //            }
        //            else if (EXPORTTYPE == 2)
        //            {
        //                rd.ExportToHttpResponse(ExportFormatType.Excel, System.Web.HttpContext.Current.Response, PRINTMODE, reportName);
        //            }
        //        }
                     
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.ToString());
        //    }
        //    finally
        //    {
        //        rd.Close();
        //        rd.Dispose();
        //    }
        //}
    }
}