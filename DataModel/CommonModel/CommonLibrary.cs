using DataAccess;
using DataModel.Master;
using Pastel.Evolution;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataModel.CommonModel
{
    public class CommonLibrary
    {
        XDocument Xdoc;
        DataTable dt;
        string dbConnectionString = ConfigurationManager.AppSettings["dbConnectionString"];
        string evolutionCommonDBConnectionString = ConfigurationManager.AppSettings["evolutionCommonDBConnectionString"];
        string serialNumber = ConfigurationManager.AppSettings["serialNumber"];
        string authCode = ConfigurationManager.AppSettings["authCode"];

        string _conStr;

        public string ConStr
        {
            get
            {
                return _conStr;
            }

            set
            {
                _conStr = value;
            }
        }
        public CommonLibrary(){}
        public CommonLibrary(string conStr)
        { ConStr = conStr;  }

        #region Methods
         
        public object FillCONTROLNO(int CONTROLENO, string DESC, int Flag, XElement LOGXML = null)
        { 
            Xdoc = DBXML.ST_SYSCONTROLENO_h(CONTROLENO, DESC, Flag, LOGXML);
            dt = SqlExev2.GetDT(Xdoc, ConStr);
            var dbResult = (from row in dt.AsEnumerable()
                            select new
                            {
                                id = row.Field<int>("idYear"),
                                STARTDATE = row.Field<DateTime>("STARTDATE"), 
                                text = row.Field<string>("cYearDescription")
                            }).ToList();
            return dbResult;
        }


        public DataTable GetLookup(int MENUID, int flag, int USERID, int PAGENO, XElement FILTERXML, XElement LOGXML = null)
        { 
            Xdoc = DBXML.ST_GetLookupData(MENUID, flag, USERID, PAGENO, FILTERXML, LOGXML);
            dt = SqlExev2.GetDT(Xdoc, ConStr); 
            return  dt;
        }

        public DataTable GetLookupExport(int MENUID, int flag, int USERID, int PAGENO, XElement FILTERXML, XElement LOGXML = null)
        {
            Xdoc = DBXML.ST_GetLookupData(MENUID, flag, USERID, PAGENO, FILTERXML, LOGXML);
            dt = SqlExev2.GetDT(Xdoc, ConStr);
            return dt;
        }
        
        public DataTable GetPriceListDt()
        { 
            DatabaseContext.Initialise(ConStr, evolutionCommonDBConnectionString, serialNumber, authCode);
            DataTable dt = PriceList.List("");
            return  dt;
        }

        public DataTable GetTaxRateDt()
        { 
            DatabaseContext.Initialise(ConStr, evolutionCommonDBConnectionString, serialNumber, authCode);
            DataTable dt = TaxRate.List("");
            return dt;
        }

        public dayType chkserviceDate(DateTime ServiceDate, XElement LOGXML = null)
        { 
            Xdoc = DBXML.ST_CHECKDATE_g(ServiceDate, 0, LOGXML);
            dt = SqlExev2.GetDT(Xdoc, ConStr);
            dayType dbresult = dt != null ? (from s in dt.AsEnumerable()
                                             select new dayType
                                             {
                                                 ISWEEKEND = s.Field<int>("ISWEEKEND"),
                                                 ISHOLIDAY = s.Field<int>("ISHOLIDAY"),
                                                 ISWORKING = s.Field<int>("ISWORKING")
                                             }).FirstOrDefault() : null;
            return dbresult;
        }

        public object fillPriceList(string dbConnectionString, string evolutionCommonDBConnectionString, string serialNumber, string authCode)
        {
            DatabaseContext.Initialise(dbConnectionString, evolutionCommonDBConnectionString, serialNumber, authCode);
            DataTable DT = PriceList.List("");

            object dbresult = DT != null ?
                (from s in DT.AsEnumerable()
                 select new 
                 {
                     id = s.Field<int>("IDPriceListName"),
                     text = s.Field<string>("cName")
                 }).ToList() : null;
            //return dbresult;
            return dbresult;
        }

        #endregion

    }
}
