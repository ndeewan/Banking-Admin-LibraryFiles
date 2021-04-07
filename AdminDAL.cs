using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLib
{
    public class AdminDAL
    {
        
        public List<Admin> ShowAllCustomers()
        {
            List<Admin> custlist = new List<Admin>();

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("[dbo].[sp_ShowAllCustomers]", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Admin cust = new Admin();
                cust.Account_No = Convert.ToInt64(dr["Account_No"]);
                cust.Account_Name = dr["Account_Name"].ToString();
                cust.Mobile_No = Convert.ToInt64(dr["Mobile_No"]);
                cust.Email = dr["Email"].ToString();
                cust.Address= dr["Address"].ToString();
                cust.Bank_Branch = dr["Bank_Branch"].ToString();
                cust.IFSC = dr["IFSC"].ToString();

                custlist.Add(cust);
            }
            cn.Close();

            return custlist;
        }


        public Admin ShowCustomerByAccNo(long accno,Admin adm)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);

            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[fn_ShowCustomerByAccNo] ("+accno+")", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            //Admin adm = new Admin();
            adm.Account_No = accno;
            adm.Account_Name = dr["Account_Name"].ToString();
            adm.Mobile_No = Convert.ToInt64(dr["Mobile_No"]);
            adm.Email = dr["Email"].ToString();
            adm.Address = dr["Address"].ToString();
            adm.Bank_Branch = dr["Bank_Branch"].ToString();
            adm.IFSC = dr["IFSC"].ToString();
            cn.Close();

            return adm;
        }

        public void EditCustomer(long accno, Admin adm)
        {
            

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);

            SqlCommand cmd = new SqlCommand("sp_UpdateDetails", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@accno", adm.Account_No);
            cmd.Parameters.AddWithValue("@accname",adm.Account_Name);
            cmd.Parameters.AddWithValue("@mobno", adm.Mobile_No);
            cmd.Parameters.AddWithValue("@email", adm.Email);
            cmd.Parameters.AddWithValue("@addr", adm.Address);
            cmd.Parameters.AddWithValue("@bbranch", adm.Bank_Branch);
            cmd.Parameters.AddWithValue("@ifsc", adm.IFSC);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public List<Transaction> ShowTransactiondetails()
        {
            List<Transaction> transList = new List<Transaction>();
            
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[fn_ShowTransactionDetails] ()", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                Transaction txn = new Transaction();
                txn.Sender_Acc = Convert.ToInt64(dr["Sender's Account No"]);
                txn.Receiver_Acc = Convert.ToInt64(dr["Receiver's Account No"]);
                txn.Rec_IFSC = dr["Receiver's Bank IFSC"].ToString();
                txn.Amount = Convert.ToDouble(dr["Amount"]);
                txn.Transaction_time = Convert.ToDateTime(dr["Transaction time"]);
                txn.Account_Balance = Convert.ToDouble(dr["Account Balance"]);

                transList.Add(txn);
            }
            cn.Close();
            return transList;
        }

        public Transaction ShowTransactionDetailsByAccNo(long accno)
        {
            Transaction txn = new Transaction();
            
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[fn_ShowTransactionDetailsByAccNo] (" + accno + ")", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            txn.Sender_Acc = Convert.ToInt64(dr["Sender's Account No"]);
            txn.Receiver_Acc = Convert.ToInt64(dr["Receiver's Account No"]);
            txn.Rec_IFSC = dr["Receiver's Bank IFSC"].ToString();
            txn.Amount = Convert.ToDouble(dr["Amount"]);
            txn.Transaction_time = Convert.ToDateTime(dr["Transaction Time"]);
            txn.Account_Balance = Convert.ToDouble(dr["Account Balance"]);

            cn.Close();

            return txn;
        }

    }
}
