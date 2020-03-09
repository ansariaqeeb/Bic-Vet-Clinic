using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataModel.Master
{
    public class treatmentService : Result.Result
    {
        #region Properties
        XDocument xdoc;
        DataTable dt;
        string flag;
        int rTid;
        int rMid;
        int serviceItemId;
        string serviceItem;
        string serviceItemDesc;
        string uomDesc;
        decimal qty;
        decimal rate;
        decimal tax;
        string taxCode;
        decimal amount;
        decimal taxAmount;
        decimal exclusiveAmount;
        decimal inclusiveAmount;
        string remark;
        bool isActive;
        string conStr;
        int branchId;

        public string Flag1 { get => flag; set => flag = value; }
        public int RTid { get => rTid; set => rTid = value; }
        public int RMid { get => rMid; set => rMid = value; }
        public int ServiceItemId { get => serviceItemId; set => serviceItemId = value; }
        public string ServiceItem { get => serviceItem; set => serviceItem = value; }
        public string ServiceItemDesc { get => serviceItemDesc; set => serviceItemDesc = value; }
        public string UomDesc { get => uomDesc; set => uomDesc = value; }
        public decimal Qty { get => qty; set => qty = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public decimal Tax { get => tax; set => tax = value; }
        public string TaxCode { get => taxCode; set => taxCode = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public decimal TaxAmount { get => taxAmount; set => taxAmount = value; }
        public decimal ExclusiveAmount { get => exclusiveAmount; set => exclusiveAmount = value; }
        public decimal InclusiveAmount { get => inclusiveAmount; set => inclusiveAmount = value; }
        public string Remark { get => remark; set => remark = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string ConStr { get => conStr; set => conStr = value; }
        public int BranchId { get => branchId; set => branchId = value; }
        #endregion
        #region Method
        public treatmentService()
        { }
        public treatmentService(string conStr)
        {
            ConStr = conStr;
        }

        //Method generating xml for  bookAppointment_C sp and saving data to database
        public Result.Result Save(int userId, treatmentService obj, XElement lOGXML = null)
        {
            try
            {
                xdoc = DBXML.treatmentService_C(obj.Flag, obj.RTid, obj.RMid,obj.ServiceItemId,obj.ServiceItem, obj.ServiceItemDesc,obj.UomDesc,obj.Qty,obj.Rate,obj.Tax, 
                    obj.TaxCode,obj.Amount, obj.TaxAmount,obj.ExclusiveAmount,obj.InclusiveAmount,obj.Remark,obj.IsActive,obj.BranchId, userId, lOGXML);
                return ReadBIErrors(Convert.ToString(SqlExe.GetXml(xdoc)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Method generating xml for petMaster sp and getting data from database
        public List<bookAppointment> getTreatmentList(int flag, int rTid,int rMid,int serviceItemId,string serviceItem,string serviceItemDesc,int branchId, int userId, XElement LOGXML = null)
        {
            try
            {
                xdoc = DBXML.treatmentService_g(flag, rTid, rMid, serviceItemId, serviceItem, serviceItemDesc, branchId, userId, LOGXML);
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
