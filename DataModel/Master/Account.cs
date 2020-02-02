using DataAccess;
using Pastel.Evolution;
using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataModel.Master
{
    public class Account
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

        public Account(string conStr)
        {

            ConStr = conStr; 
        }
        public async Task<object> validateAget(string LoginId,string Password, int IsDomain =0 , int ExpireOn = 0) 
        {
            try
            {
                //CompanyMaster compObj = new CompanyMaster();
                //string CompConectionString = await compObj.getComapnyConnectionstr(CompId); 

                //DatabaseContext.CreateCommonDBConnection(evolutionCommonDBConnectionString);
                //DatabaseContext.SetLicense(serialNumber, authCode);
                //DatabaseContext.CreateConnection(dbConnectionString);

                DatabaseContext.Initialise(dbConnectionString, evolutionCommonDBConnectionString, serialNumber, authCode);
                bool valid = Agent.Authenticate(LoginId, Password);
                if (valid)
                {
                    //DatabaseContext.Initialise(dbConnectionString, evolutionCommonDBConnectionString, serialNumber, authCode);
                    var agent= Agent.GetByName(LoginId);
                    //var agent = new Agent();
                    //return agent;
                    return await Task.Factory.StartNew(() => agent);
                }
                else
                {
                    var agent = new Agent();
                    //return agent;
                    return await Task.Factory.StartNew(() => agent);
                }

            }
            catch (Exception ex)
            { 
                throw ex;
            }
           
           
        }
    }
}
