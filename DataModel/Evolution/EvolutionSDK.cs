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
        public EvolutionSDK(string dbConnection, string evolutionCommonConnection, string serialNumber, string authCode)
        {
            try
            {
                DatabaseContext.Initialise(dbConnection, evolutionCommonConnection, serialNumber, authCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EvolutionSDK(string dbConnection, string evolutionCommonConnection, string serialNumber, string authCode, int branchId)
        {
            try
            {
                DatabaseContext.Initialise(dbConnection, evolutionCommonConnection, serialNumber, authCode);
                DatabaseContext.SetBranchContext(branchId);
            }
            catch
            {
                throw new Exception("Error While Processing");
            }
        }
        public EvolutionSDK(string dbConnection, string evolutionCommonConnection, string serialNumber, string authCode, int branchId,Agent agent)
        {
            try
            {
                DatabaseContext.Initialise(dbConnection, evolutionCommonConnection, serialNumber, authCode);
                DatabaseContext.SetBranchContext(branchId);
                DatabaseContext.CurrentAgent = agent;  
            }
            catch
            {
                throw new Exception("Error While Processing");
            }
        }


        public Agent validateAgent(string loginId, string password)
        {
            try
            {
                bool valid = Agent.Authenticate(loginId, password);
                Agent agent = new Agent();
                if (valid)
                {
                    agent = Agent.GetByName(loginId);
                    DatabaseContext.CurrentAgent = agent; //constant
                    return agent;
                }
                else
                {
                    return agent;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int agentGroupValidate(int agentId, string groupName)
        {
            try
            {
                return AgentGroup.Find("idAgentGroups IN (SELECT iGroupID FROM  dbo.[_rtblAgentGroupMembers] WHERE iAgentID = " + Convert.ToString(agentId) + ") AND cGroupName='" + groupName + "'");
            }
            catch (Exception ex)
            {
                throw ex;
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

        public List<Customer> customerList(string criteria = "1=1")
        {
            try
            {
                DataTable customerDt = Customer.List(criteria);
                customerDt.DefaultView.Sort = "Name";
                customerDt = customerDt.DefaultView.ToTable();
                List<Customer> lst = customerDt != null && customerDt.Rows.Count > 0 ? 
                (from s in customerDt.AsEnumerable()
                 select new Customer
                 {
                     Code = s.Field<string>("Account"),
                     Description = s.Field<string>("Title") + ". " + s.Field<string>("Name"),
                     Telephone = s.Field<string>("Telephone"),
                 }).ToList() : null; 
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        public Customer getCustomer(string code)
        {
            try
            {
                Customer objCust = new Customer(code);
                return objCust;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Customer addCustomer(Customer obj)
        {
            try
            {
                obj.Save();
                return obj; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Customer editCustomer(Customer obj)
        {
            try
            {
                Customer newobj = new Customer(obj.Code);
                newobj.Description = obj.Description;
                newobj.IDNumber = obj.IDNumber;
                newobj.Telephone = obj.Telephone;
                newobj.Addressee = obj.Addressee;

                newobj.Save();
                return newobj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
