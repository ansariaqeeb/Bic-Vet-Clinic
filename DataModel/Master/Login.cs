using DataAccess;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataModel
{
    public class Login
    {
        #region properties
        XDocument xdoc;
        int _id;
        string _userName;
        string _password;
        string _firstName;
        string _lastName;


        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
            }
        }


        #endregion
        #region Methods 
        //public async Task<userMaster> getLoginInfo(int flag, string userName, string password)
        //{
        //    try
        //    {
        //        xdoc = DBXML.UserAuthentication_g(flag, userName, password);
        //        DataTable dt = SqlExe.Instance.GetDT(xdoc);
        //        userMaster model = dt != null ? dt.AsEnumerable().Select(row =>
        //                     new userMaster
        //                     {
        //                         UserId = row.Field<int?>("USERID") ?? 0,
        //                         FName = row.Field<string>("FNAME") ?? "",
        //                         MName = row.Field<string>("MNAME") ?? "",
        //                         LName = row.Field<string>("LNAME") ?? "",
        //                         Email = row.Field<string>("Email") ?? "",
        //                         LoginId = row.Field<string>("LOGINID") ?? "",
        //                         Password = row.Field<string>("PASSWORD"),
        //                         UTypeId = row.Field<int?>("UTYPEID") ?? 0,
        //                     //UTYPEDESC = row.Field<string>("UTYPEDESC") == null ? "" : row.Field<string>("UTYPEDESC"),
        //                         MobileNo = row.Field<string>("MobileNo"),
        //                         Dob = row.Field<DateTime>("DOB"),
        //                         IsActive = row.Field<bool>("ISACTIVE")
        //                     }).FirstOrDefault() : null;

        //        //return model;
        //        return await Task.Factory.StartNew(() => model);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        
        //}

        #endregion
    }
}
