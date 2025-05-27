using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ShoppingWcfService
{
    /// <summary>
    /// Summary description for Shopping
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Shopping : System.Web.Services.WebService
    {
        string strcnn = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public DataSet GetAllCategories()
        {
            SqlConnection conn = new SqlConnection(strcnn);
            string Sql = "SELECT * FROM Categories";
            SqlDataAdapter da = new SqlDataAdapter(Sql, conn);
            DataSet ds = new DataSet();

            da.Fill(ds, "Categories");

            return ds;
        }


        [WebMethod]
        public DataSet GetProducts(int? CategoryID)
        {
            SqlConnection conn = new SqlConnection(strcnn);

            if (CategoryID != null)
            {
                string Sql = "SELECT * FROM Products WHERE CategoryID="+CategoryID;
                SqlDataAdapter da = new SqlDataAdapter(Sql, conn);
                DataSet ds = new DataSet();

                da.Fill(ds, "Products");

                return ds;
            }
            else
            {
                string Sql = "SELECT * FROM Products";
                SqlDataAdapter da = new SqlDataAdapter(Sql, conn);
                DataSet ds = new DataSet();

                da.Fill(ds, "Products");

                return ds;
            }
        }

        [WebMethod]
        public DataSet Search(int? CategoryID, string Property, string Value)
        {
            SqlConnection conn = new SqlConnection(strcnn);

            if (CategoryID != null)
            {
                string Sql = "SELECT * FROM Products WHERE CategoryID = " + CategoryID + " AND Details.exist('//" + Property + "[text()=\"" + Value + "\"]') = 1";
                SqlDataAdapter da = new SqlDataAdapter(Sql, conn);
                DataSet ds = new DataSet();

                da.Fill(ds, "Products");

                return ds;
            }
            else
            {
                string Sql = "SELECT * FROM Products WHERE Details.exist('//" + Property + "[text()=\"" + Value + "\"]') = 1";
                SqlDataAdapter da = new SqlDataAdapter(Sql, conn);
                DataSet ds = new DataSet();

                da.Fill(ds, "Products");

                return ds;
            }
        }
    }
}
