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
        public int JobPosting(int MID,int WarehouseId,string dbConnectionString,string evolutionCommonDBConnectionString,string serialNumber,string authCode,int USERID,XElement LOGXML=null)
        {
            try
            {
                xdoc = DBXML.ST_INVOICETRANSDTLPOSTING_g(MID, USERID, LOGXML);
                DataTable dt = SqlExev2.GetDT(xdoc, ConStr);
                List<JobCardPosting> dbresult = dt != null ? (from s in dt.AsEnumerable()
                                                            select new JobCardPosting
                                                            {
                                                                VOYAGENO = s.Field<string>("VOYAGENO"), 
                                                                ITEMID = s.Field<int>("ITEMID"),
                                                                TOTALVALUE = s.Field<decimal>("AMOUNT") 
                                                            }).ToList() : null;

                DatabaseContext.Initialise(dbConnectionString, evolutionCommonDBConnectionString, serialNumber, authCode);
                JobCard jc = new JobCard(dbresult.FirstOrDefault().VOYAGENO);

                foreach (var item in dbresult)
                {
                    JobDetail jd = new JobDetail();
                    jd.TransactionCode =new JobTransactionCode(JobDetail.TransactionSource.Inventory, "ST");
                    jd.Status = JobCard.JobStatus.Active;
                    jd.InventoryItem = new InventoryItem(item.ITEMID);
                    jd.Warehouse = new Warehouse(WarehouseId);
                    jd.Quantity = 1;
                    jd.UnitSellingPrice =Convert.ToDouble(item.TOTALVALUE);
                    jc.Detail.Add(jd);
                }

                jc.Save();

                //return jc.ID;
                //return dbresult;
                return jc.ID;

            }
            catch (Exception ex)
            {
                return  0;
            }


        }
        #endregion
    }
}
