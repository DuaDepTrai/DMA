using HRM1API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthwindAPIClient.Models;

namespace HRM1Client.Controllers
{
    public class OrganizationsController : Controller
    {
        // GET: OrganizationsController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            //restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Organizations";
            string result = restClient.RestRequestAll();

            List<Organization> usersList = JsonConvert.DeserializeObject<List<Organization>>(result);

            return View(usersList);
        }

        // GET: OrganizationsController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            //restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Organizations/" + id;
            string result = restClient.RestRequestAll();

            Organization org = JsonConvert.DeserializeObject<Organization>(result);

            return View(org);
        }

        // GET: OrganizationsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Organization obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                //restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Organizations";
                string result = restClient.RestPostObj(obj);
                if (result == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: OrganizationsController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            //restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Organizations/" + id;
            string result = restClient.RestRequestAll();

            Organization org = JsonConvert.DeserializeObject<Organization>(result);

            return View(org);
        }

        // POST: OrganizationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Organization obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                //restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Organizations";
                string result = restClient.RestPutObj(obj);
                if (result == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(obj);
                }
            }
            catch
            {
                return View(obj);
            }
        }

        // GET: OrganizationsController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            //restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Organizations/" + id;
            string result = restClient.RestRequestAll();

            Organization org = JsonConvert.DeserializeObject<Organization>(result);

            return View(org);
        }

        // POST: OrganizationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                RestClient restClient = new RestClient();
                //restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Organizations/" + id;
                string result = restClient.RestDeleteObj();
                if (result == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
