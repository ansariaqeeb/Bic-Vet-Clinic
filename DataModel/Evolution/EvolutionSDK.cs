using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pastel.Evolution;

namespace DataModel.Evolution
{
    public class EvolutionSDK
    {
        public EvolutionSDK(string dbConnection,string evolutionCommonConnection,string serialNumber, string authCode)
        {
            try
            {
               DatabaseContext.Initialise(dbConnection, evolutionCommonConnection, serialNumber, authCode);
            }
            catch
            {
                throw new Exception("Error While Processing");
            }
        }

        public List<Branch> branchList(string criteria = "1=1")
        {
            try
            {
                DataTable branchList = Branch.List(criteria);
                branchList.DefaultView.Sort = "cBranchCode";
                branchList = branchList.DefaultView.ToTable();
                List<Branch> lst = branchList != null && branchList.Rows.Count > 0 ? 
                    branchList.AsEnumerable().Select(s => new Branch(s.Field<string>("cBranchCode"))).ToList() : null;
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

    }
}
