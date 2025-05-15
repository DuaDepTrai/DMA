using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindAPIClient.Models;
using Newtonsoft.Json;

namespace NorthwindAPIClient.Controllers
{
    public class RegionController : Controller
    {
        // GET: RegionController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Region";
            string result = restClient.RestRequestAll();

            List<Region> RegionList = JsonConvert.DeserializeObject<List<Region>>(result);

            return View(RegionList);
        }

        // GET: RegionController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Region/"+id;
            string result = restClient.RestRequestAll();

            Region region = JsonConvert.DeserializeObject<Region>(result);

            return View(region);
        }

        // GET: RegionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegionController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Region/" + id;
            string result = restClient.RestRequestAll();

            Region region = JsonConvert.DeserializeObject<Region>(result);

            return View(region);
        }

        // POST: RegionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegionController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Region/" + id;
            string result = restClient.RestRequestAll();

            Region region = JsonConvert.DeserializeObject<Region>(result);

            return View(region);
        }

        // POST: RegionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
