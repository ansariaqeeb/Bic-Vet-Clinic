using Pastel.Evolution;
using Pastel.Evolution.Common;

namespace DataModel.LoginModel
{
    public class LoginSessionDetails
    {
        //public int userId { get; set; }
        //public int compId { get; set; }
        //public string loginName { get; set; }
        //public int userType { get; set; }
        //public string userName { get; set; }
        //public string userTime { get; set; }
        //public DateTime time { get; set; }
        //public string Timezone { get; set; }
        //public string dbConnectionString { get; set; }
        //public string evolutionCommonDBConnectionString { get; set; }
        //public string serialNumber { get; set; }
        //public string authCode { get; set; }
        //public string itemCriteria { get; set; }
        //public string etaDateUsrDef { get; set; }
        //public string etdDateUsrDef { get; set; }
        //public string uom1 { get; set; }
        //public string uom2 { get; set; }
        //public string minCharge { get; set; }
        //public string invcNumb { get; set; }
        //public string perInvcNumb { get; set; }
        //public string jobCriteria { get; set; }
        //public string priceListCriteria { get; set; }
        //public string warehouseId { get; set; }
        //public string clientSettingsProviderServiceUri { get; set; }
        //public string AdminGroup { get; set; }
        //public string userGroup { get; set; } 

        public DataModel.Database.dbConfig objDb { get; set; }
        public Agent objAgent { get; set; }

        public LoginModels objLoginM { get; set; }
    }
}
