using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MVVMCheckboxAlIDataGrid.ViewModel.DAL
{
   public static class DBUtil
   {
      public static DataTable GetProduct()
      {
         DataSet ds = new DataSet();
         string query = "Select * from Products;";

         try
         {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["products"].ConnectionString.ToString()))
            {
               SqlCommand cmd = new SqlCommand(query, conn);
               conn.Open();
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               da.Fill(ds);
               conn.Close();
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }

         return ds.Tables[0];
      }

      public static int UpdateProductDiscontinue(bool value, int productID)
      {
         int result = 0;
         string query = String.Format("Update Products set discontinue = '{0}' where productID = '{1}' ;", value, productID);

         try
         {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["products"].ConnectionString.ToString()))
            {
               SqlCommand cmd = new SqlCommand(query, conn);
               conn.Open();
               result = cmd.ExecuteNonQuery();
               conn.Close();
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }

         return result;
      }
   }
}
