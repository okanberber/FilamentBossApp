using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
            try
            {
                cmd.CommandText = "SELECT * FROM Categories";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("kategori No");
                dt.Columns.Add("Kategori Isim");
                dt.Columns.Add("Açıklama");
                dt.Columns.Add("Aktif");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string description = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    bool active = reader.GetBoolean(3);
                    dt.Rows.Add(id, name, description,active);
                }
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
        public bool KategoriDuzenle(string cn,string des, bool ac,string id)
        {
            try
            {
                cmd.CommandText = "UPDATE Categories SET CategoryName=@cn,Description=@des,IsActive=@ac WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cn", cn);
                cmd.Parameters.AddWithValue("@des", des);
                cmd.Parameters.AddWithValue("@ac", ac);
                cmd.Parameters.AddWithValue("@id", id);
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
        public bool KategoriSil(int id)
        {
            try
            {
                cmd.CommandText = "DELETE Categories WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
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
                con.Close() ;
            }
        }
        public List<Categories> KategoriDoldur()
        {
            List<Categories> list = new List<Categories>();
            try
            {
                cmd.CommandText = "SELECT ID,CategoryName,Description,IsActive FROM Categories";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Categories c = new Categories();
                    c.ID = reader.GetInt32(0);
                    c.CategoryName = reader.GetString(1);
                    c.Description = reader.GetString(2);
                    c.IsActive = reader.GetBoolean(3);
                    list.Add(c);
                }
                return list;
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
        public Categories KategoriDoldur(string id)
        {
            Categories c = new Categories();
            try
            {
                cmd.CommandText = "SELECT ID,CategoryName,Description,IsActive FROM Categories WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    c.ID = reader.GetInt32(0);
                    c.CategoryName = reader.GetString(1);
                    c.Description = reader.GetString(2);
                    c.IsActive = reader.GetBoolean(3);
                }
                return c;
            }
            catch
            {
                return null ;
            }
            finally
            {
                con.Close( );
            }
        }
        public Products UrunDoldur(int id)
        {
            Products p = new Products();
            try
            {
                cmd.CommandText = "SELECT P.ID,P.CategoryID,C.CategoryName,P.ProductName,P.Piece,P.Price,P.Diameter,P.Color,P.Description,P.IsActive FROM Products AS P JOIN Categories AS C ON P.CategoryID = C.ID WHERE P.ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    p.ID = reader.GetInt32(0);
                    p.CategoryID = reader.GetInt32(1);
                    p.CategoryName = reader.GetString (2);
                    p.ProductName = reader.GetString(3);
                    p.Piece=reader.GetInt32(4);
                    p.Price = reader.GetDecimal(5);
                    p.Diameter=reader.GetString(6);
                    p.Color=reader.GetString(7);
                    p.Description = reader.GetString(8);
                    p.IsActive = reader.GetBoolean(9);
                }
                return p;
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
        public List<Products> UrunDoldur(string X)
        {
            int indirim = 0;
            if (X == "BRONZ")
            {
                indirim = 2;
            }
            
            if (X == "SİLVER")
            {
                indirim = 5;
            }
            if(X == "GOLD")
            {
                indirim = 10;
            }
            
            List<Products> liste = new List<Products>();
            
            try
            {
                cmd.CommandText = "SELECT P.ID,P.CategoryID,C.CategoryName,C.Description,P.ProductName,P.Piece,P.Price,P.Diameter,P.Color,P.Description,P.IsActive FROM Products AS P JOIN Categories AS C ON P.CategoryID = C.ID";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Products p = new Products();
                    p.ID = reader.GetInt32(0);
                    p.CategoryID = reader.GetInt32(1);
                    p.CategoryName = reader.GetString(2);
                    p.CategoryDescription = reader.GetString(3);
                    p.ProductName = reader.GetString(4);
                    p.Piece = reader.GetInt32(5);
                    p.Price = reader.GetDecimal(6)-(reader.GetDecimal(6)*indirim)/100;
                    p.Diameter = reader.GetString(7);
                    p.Color = reader.GetString(8);
                    p.Description = reader.GetString(9);
                    p.IsActive = reader.GetBoolean(10);
                    liste.Add(p);
                }
                return liste;
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
        public bool UrunEkle(int Cid,string Pname,int Ppie,decimal Ppri,string Pdia,string Pcol, string Pdes, bool Pisactive)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Products (CategoryID,ProductName,Piece,Price,Diameter,Color,Description,IsActive) VALUES(@cid,@name,@pie,@pri,@dia,@col,@des,@isactive)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cid",Cid);
                cmd.Parameters.AddWithValue("@name", Pname);
                cmd.Parameters.AddWithValue("@pie",Ppie);
                cmd.Parameters.AddWithValue("@pri", Ppri);
                cmd.Parameters.AddWithValue("@dia",Pdia);
                cmd.Parameters.AddWithValue("@col",Pcol);
                cmd.Parameters.AddWithValue("@des", Pdes);
                cmd.Parameters.AddWithValue("@isactive", Pisactive);
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
        public DataTable UrunListele()
        {
            try
            {
                cmd.CommandText = "SELECT P.ID,P.CategoryID,C.CategoryName,P.ProductName,P.Piece,P.Price,P.Diameter,P.Color,P.Description,P.IsActive FROM Products AS P JOIN Categories AS C ON P.CategoryID = C.ID";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Ürün No");
                dt.Columns.Add("Kategori No");
                dt.Columns.Add("Kategori Adı");
                dt.Columns.Add("Ürün Adı");
                dt.Columns.Add("Ürün Adedi");
                dt.Columns.Add("Ürün Fiyatı");
                dt.Columns.Add("Ürün Çapı");
                dt.Columns.Add("Ürün Rengi");
                dt.Columns.Add("Açıklama");
                dt.Columns.Add("Aktif");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int cid = reader.GetInt32(1);
                    string cname = reader.GetString(2);
                    string name = reader.GetString(3);
                    int pie = reader.GetInt32(4);
                    decimal pri = reader.GetDecimal(5);
                    string dia = reader.GetString(6);
                    string col = reader.GetString(7);
                    string description = reader.IsDBNull(8) ? "" : reader.GetString(8);
                    bool active = reader.GetBoolean(9);
                    dt.Rows.Add(id,cid,cname,name,pie,pri,dia,col,description,active);
                }
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
        public bool UrunDuzenle(int Pid,int Cid,string Pname,int Pie,decimal Pri,string Dia,string Pcol,string Des,bool IsActive)
        {
            try
            {
                cmd.CommandText = "UPDATE Products SET CategoryID=@cid,ProductName=@pname,Piece=@pie,Price=@pri,Diameter=@dia,Color=@col,Description=@des,IsActive=@ac WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cid", Cid);
                cmd.Parameters.AddWithValue("@pname",Pname);
                cmd.Parameters.AddWithValue("@pie", Pie);
                cmd.Parameters.AddWithValue("@pri",Pri);
                cmd.Parameters.AddWithValue("@dia", Dia);
                cmd.Parameters.AddWithValue("@col",Pcol);
                cmd.Parameters.AddWithValue("@des", Des);
                cmd.Parameters.AddWithValue("@ac", IsActive);
                cmd.Parameters.AddWithValue("@id", Pid);
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
        public bool UrunSil(int id)
        {
            try
            {
                cmd.CommandText = "DELETE Products WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
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
        public bool XMLOlusturBronz()
        {
            DataModel dm = new DataModel();
            List<Products> productList = dm.UrunDoldur("BRONZ");
            try
            {
                using (StreamWriter sw = new StreamWriter("Bronz.xml"))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Products>));
                    serializer.Serialize(sw, productList);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XMLOlusturSilver()
        {
            DataModel dm = new DataModel();
            List<Products> productList = dm.UrunDoldur("SİLVER");
            try
            {
                using (StreamWriter sw = new StreamWriter("Silver.xml"))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Products>));
                    serializer.Serialize(sw, productList);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XMLOlusturGold()
        {
            DataModel dm = new DataModel();
            List<Products> productList = dm.UrunDoldur("GOLD");
            try
            {
                using (StreamWriter sw = new StreamWriter("Gold.xml"))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Products>));
                    serializer.Serialize(sw, productList);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
