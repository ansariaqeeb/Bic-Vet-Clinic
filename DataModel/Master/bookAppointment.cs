using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace DataModel.Master
{
    public class bookAppointment: Result.Result
    {
        #region Properties
        XDocument xdoc;
        DataTable dt;
        int id;
        string flag;
        string bookingNo;
        string custCode;
        string custName;
        string docCode;
        string docName;
        string petCode;
        DateTime bookDateTime;
        string bookingDate;
        string bookingTime; 
        string bookingStatus;
        bool isActive;
        string conStr;
        int branchId;

        public int Id { get => id; set => id = value; }
        public string BookingNo { get => bookingNo; set => bookingNo = value; }
        public string CustCode { get => custCode; set => custCode = value; }
        public string DocCode { get => docCode; set => docCode = value; }
        public string PetCode { get => petCode; set => petCode = value; }
        public DateTime BookDateTime { get => bookDateTime; set => bookDateTime = value; }
        public string BookingDate { get => bookingDate; set => bookingDate = value; }
        public string BookingTime { get => bookingTime; set => bookingTime = value; }
        public string BookingStatus { get => bookingStatus; set => bookingStatus = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string ConStr { get => conStr; set => conStr = value; }
        public string Flag { get => flag; set => flag = value; }
        public int BranchId { get => branchId; set => branchId = value; }
        public string CustName { get => custName; set => custName = value; }
        public string DocName { get => docName; set => docName = value; }
        #endregion
        #region Methods
        public bookAppointment()
        { }
        public bookAppointment(string conStr)
        {
            ConStr = conStr;
        }

        //Method generating xml for  bookAppointment_C sp and saving data to database
        public Result.Result Save(int userId, bookAppointment obj, XElement lOGXML = null)
        {
            try
            {
                xdoc = DBXML.bookAppointment_C(obj.Flag, obj.Id, obj.BranchId, obj.BookingNo == null ? "" : obj.BookingNo,obj.CustCode,obj.DocCode,obj.PetCode,obj.BookDateTime,obj.BookingStatus, true, userId, lOGXML);
                return ReadBIErrors(Convert.ToString(SqlExe.GetXml(xdoc)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Method generating xml for petMaster sp and getting data from database
        public List<bookAppointment> getBookingList(int flag, int id, string bookingNo, string docCode,string custCode, int branchId, int userId, XElement LOGXML = null)
        {
            try
            {
                xdoc = DBXML.bookAppointment_g(flag, id, bookingNo,docCode,custCode, branchId, userId, LOGXML);
                dt = SqlExev2.GetDT(xdoc, ConStr);
                List<bookAppointment> model = dt != null ? dt.AsEnumerable().Select(row => new bookAppointment
                {
                    Id = row.Field<int>("Id"),
                    BookingNo = row.Field<string>("bookingNo"),
                    CustCode = row.Field<string>("custCode"),
                    CustName = row.Field<string>("custName"),
                    DocCode = row.Field<string>("docCode"),
                    DocName = row.Field<string>("docName"),
                    PetCode = row.Field<string>("petCode"),
                    BookDateTime = row.Field<DateTime>("bookDateTime"),
                    BookingStatus = row.Field<string>("bookingStatus"),
                    IsActive = row.Field<bool>("isActive")
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
