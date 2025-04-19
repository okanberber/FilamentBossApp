using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataModel
    {
        SqlConnection con; SqlCommand cmd;
        
        public DataModel()
        { 
            con = new SqlConnection(ConnectionStrings.ConStr);
            cmd = con.CreateCommand();
        }
        public bool KategoriEkle(string Cname,string Cdes, bool Cisactive)
        {          
          try
          {
              cmd.CommandText = "INSERT INTO Categories (CategoryName,Description,IsActive) VALUES(@name,@des,@isactive)";
              cmd.Parameters.Clear();
              cmd.Parameters.AddWithValue("@name",Cname);
              cmd.Parameters.AddWithValue("@des",Cdes);
              cmd.Parameters.AddWithValue("@isactive",Cisactive);
              con.Open();
              cmd.ExecuteNonQuery();
              return true;
          }
          catch
          {
              return false;
          }
          finally
          {
           con.Close();
          }                
        }
        public DataTable KategoriListele()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.CommandText = "SELECT ID, CategoryName, Description, IsActive FROM Categories";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch 
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
