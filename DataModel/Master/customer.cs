using Pastel.Evolution;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace DataModel.Master
{
    public class customer
    {
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

        public customer(string conStr)
        { 
            ConStr = conStr;
        }
        public async Task<DataTable> GetJobDt()
        { 
            DatabaseContext.Initialise(ConStr, evolutionCommonDBConnectionString, serialNumber, authCode); 
            DataTable dt = Customer.List("");
            //return dt;
            return await Task.Factory.StartNew(() => dt);
        }
    }
}
