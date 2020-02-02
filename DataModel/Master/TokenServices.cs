using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using DataAccess;

namespace DataModel.Master
{
    public class TokenServices
    {
        ////Token services
        //public static string GenerateToken(int UserId, int IsDomain = 0, int ExpireON = 0, XElement LOGXML = null)
        //{
        //    TokenKeys obj = new TokenKeys();
        //    obj.USERID = UserId;
        //    obj.token = Guid.NewGuid().ToString();
        //    obj.issuedOn = DateTime.Now;
        //    if (IsDomain == 0)
        //    {
        //        obj.IsDomain = false;
        //    }
        //    else
        //    {
        //        obj.IsDomain = true;
        //    }
        //    obj.expiredOn = ExpireON > 0 ? DateTime.Now.AddMinutes(Convert.ToDouble(ExpireON)) : DateTime.Now.AddDays(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));

        //    CacheHelper.SaveTocache(obj.token, obj, obj.expiredOn);
        //    XDocument xdoc = DBXML.Token_c("I", UserId, obj.token, obj.issuedOn, obj.expiredOn, IsDomain, LOGXML);
        //    DataTable dt = SqlExe.Instance.GetDT(xdoc);
        //    return obj.token;
        //}

        ////Validate tokens if already created
        //public static bool ValidateToken(string Token)
        //{
        //    if (CacheHelper.IsIncache(Token) && CacheHelper.GetFromCache(Token).expiredOn > DateTime.Now)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        XDocument xdoc = DBXML.ValidateToken_c("V", Token);
        //        bool IsValid = Convert.ToBoolean(SqlExe.Instance.ExecuteScalarforInt(xdoc));

        //        try
        //        {
        //            if (IsValid)
        //            {
        //                TokenKeys obj = GetTokenDetails(Token);
        //                CacheHelper.SaveTocache(obj.token, obj, obj.expiredOn);
        //            }
        //        }
        //        catch { }
        //        return IsValid;
        //    }
        //}


        ////Calls to get token details
        //public static TokenKeys GetTokenDetails(string Token)
        //{
        //    XDocument xdoc = DBXML.GetTokenDetails_g(Token);
        //    DataTable dt = SqlExe.Instance.GetDT(xdoc);
        //    TokenKeys data = (from row in dt.AsEnumerable()
        //                      select new TokenKeys
        //                      {
        //                          USERID = row.Field<int>("UserId"),
        //                          token = Token,
        //                          issuedOn = row.Field<DateTime>("IssuedOn"),
        //                          expiredOn = row.Field<DateTime>("ExpiresOn"),
        //                          IsDomain = row.Field<bool>("ISDOMAIN")

        //                      }).FirstOrDefault();
        //    return data;
        //}
    }

    public class TokenKeys
    {
        public int USERID { get; set; }
        public string token { get; set; }
        public DateTime issuedOn { get; set; }
        public DateTime expiredOn { get; set; }
        public bool IsDomain { get; set; }
    }
}
