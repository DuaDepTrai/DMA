using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing;

namespace WcfService
{
    /// <summary>
    /// Summary description for Northwind
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Northwind : System.Web.Services.WebService
    {
        string strcnn = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        //static int ID = 0;
        //static List<Region> regions = new List<Region>();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int Sum(int a, int b)
        {
            return a + b;
        }

        [WebMethod]
        public DataSet GetAllRegion()
        {
            SqlConnection conn = new SqlConnection(strcnn);
            string Sql = "SELECT * FROM Region";
            SqlDataAdapter da = new SqlDataAdapter(Sql, conn); 
            DataSet ds = new DataSet();

            da.Fill(ds, "Region");

            return ds;
        }

        [WebMethod]
        public DataSet GetRegionById(int id)
        {
            SqlConnection conn = new SqlConnection(strcnn);
            string Sql = "SELECT * FROM Region WHERE RegionID=" + id;
            SqlDataAdapter da = new SqlDataAdapter(Sql, conn);
            DataSet ds = new DataSet();

            da.Fill(ds, "Region");

            return ds;
        }

        [WebMethod]
        public int InsertRegion(int id, string RegionDescription)
        {
            SqlConnection conn = new SqlConnection(strcnn);
            string Sql = "INSERT INTO Region (RegionID, RegionDescription) VALUES (" + id + ",N'" + RegionDescription + "')";
            
            if (conn.State==ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand(Sql, conn);
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.ExecuteNonQuery();
                return id;
            }
            catch  (Exception)
            {
                return 0;
            }
        }

        [WebMethod]
        public int UpdateRegion(int id, string RegionDescription)
        {
            SqlConnection conn = new SqlConnection(strcnn);
            string Sql = "UPDATE Region SET RegionDescription=N'" + RegionDescription + "' WHERE RegionID=" + id;

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand(Sql, conn);
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.ExecuteNonQuery();
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        [WebMethod]
        public int DeleteRegion(int id)
        {
            SqlConnection conn = new SqlConnection(strcnn);
            string Sql = "DELETE FROM Region WHERE RegionID=" + id;

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand(Sql, conn);
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.ExecuteNonQuery();
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
