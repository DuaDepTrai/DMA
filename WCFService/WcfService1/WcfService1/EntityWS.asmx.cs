using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WcfService1
{
    /// <summary>
    /// Summary description for EntityWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EntityWS : System.Web.Services.WebService
    {
        //string strcnn = ConfigurationManager.ConnectionStrings["NorthwindEntities"].ConnectionString;
        private NorthwindEntities db = new NorthwindEntities();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Categories> GetCategories() 
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            return db.Categories.ToList();
        }
    }
}
