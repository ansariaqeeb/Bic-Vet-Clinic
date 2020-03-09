using DataAccess;
using Pastel.Evolution;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataModel.Transaction
{
    public class JobCardPosting
    {
        //string dbConnectionString = ConfigurationManager.AppSettings["dbConnectionString"];
        //string evolutionCommonDBConnectionString = ConfigurationManager.AppSettings["evolutionCommonDBConnectionString"];
        //string serialNumber = ConfigurationManager.AppSettings["serialNumber"];
        //string authCode = ConfigurationManager.AppSettings["authCode"];
        //int WarehouseId =Convert.ToInt32(ConfigurationManager.AppSettings["WarehouseId"]);
        #region Properties
        XDocument xdoc;
        DataTable dt;
        string _conStr;
        string _VOYAGENO;
        int _ITEMID;
        decimal _TOTALVALUE;
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

        public string VOYAGENO
        {
            get
            {
                return _VOYAGENO;
            }

            set
            {
                _VOYAGENO = value;
            }
        }

        public int ITEMID
        {
            get
            {
                return _ITEMID;
            }

            set
            {
                _ITEMID = value;
            }
        }

        public decimal TOTALVALUE
        {
            get
            {
                return _TOTALVALUE;
            }

            set
            {
                _TOTALVALUE = value;
            }
        }

        #endregion
        #region Methods

        public JobCardPosting()
        { }

        public JobCardPosting(string conStr)
        { 
            ConStr = conStr;
        }
         
        #endregion
    }
}
