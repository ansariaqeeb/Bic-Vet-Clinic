using DataAccess;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace DataModel.Master
{
    public class pet : Result.Result
    {
        #region propeties
        XDocument xdoc;
        DataTable dt;
        string flag;
        int id;
        string name;
        string code;
        string custCode;
        DateTime dob;
        string sex;
        string species;
        string breed;
        int branchId;
        string conStr;
        string registrationNo;
        bool isSterilized;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string CustCode { get => custCode; set => custCode = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        public string Sex { get => sex; set => sex = value; }
        public string Species { get => species; set => species = value; }
        public string Breed { get => breed; set => breed = value; }
        public int BranchId { get => branchId; set => branchId = value; }
        public string ConStr { get => conStr; set => conStr = value; }
        public string Code { get => code; set => code = value; }
        public string RegistrationNo { get => registrationNo; set => registrationNo = value; }
        public string Flag { get => flag; set => flag = value; }
        public bool IsSterilized { get => isSterilized; set => isSterilized = value; }
        #endregion
        #region Method
        public pet()
        { }
        public pet(string conStr)
        {
            ConStr = conStr;
        }
        //Method generating xml for  holidayCalendar_C sp and saving data to database
        public Result.Result Save(int userId, pet obj, XElement lOGXML = null)
        {
            try
            {
                xdoc = DBXML.petMaster_C(obj.Flag, obj.Id,obj.BranchId,obj.Code == null ? "" : obj.Code,obj.Name,obj.RegistrationNo,obj.Dob,obj.Sex,obj.Species,obj.Breed,obj.isSterilized,true, userId, lOGXML);
                return ReadBIErrors(Convert.ToString(SqlExe.GetXml(xdoc)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Method generating xml for petMaster sp and getting data from database
        public List<pet> getPetList(int flag, int id, string name, string custCode, int branchId, int userId, XElement LOGXML = null)
        {
            try
            {
                xdoc = DBXML.petMaster_g(flag,id,name,custCode, branchId, userId, LOGXML);
                dt = SqlExev2.GetDT(xdoc, ConStr);
                List<pet> model = dt != null ? dt.AsEnumerable().Select(row => new pet
                {
                    id = row.Field<int>("Id"),
                    code = row.Field<string>("code"), 
                    name = row.Field<string>("petName"),
                    registrationNo = row.Field<string>("registrationNo"),
                    dob = row.Field<DateTime>("dob"),
                    sex = row.Field<string>("sex"),
                    species = row.Field<string>("species"), 
                    breed = row.Field<string>("breed")
                }).ToList() : null;
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        #endregion
    }
}
