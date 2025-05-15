using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthwindAPIClient.Models;

namespace NorthwindAPIClient.Controllers
{
    public class TerritoriesController : Controller
    {
        // GET: TerritoriesController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Territories";
            string result = restClient.RestRequestAll();

            List<Territories> TersList = JsonConvert.DeserializeObject<List<Territories>>(result);

            return View(TersList);
        }

        // GET: TerritoriesController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Territories/" + id;
            string result = restClient.RestRequestAll();

            Territories ter = JsonConvert.DeserializeObject<Territories>(result);

            return View(ter);
        }

        // GET: TerritoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TerritoriesController/Create
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

        // GET: TerritoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Territories/" + id;
            string result = restClient.RestRequestAll();

            Territories ter = JsonConvert.DeserializeObject<Territories>(result);

            return View(ter);
        }

        // POST: TerritoriesController/Edit/5
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

        // GET: TerritoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Territories/" + id;
            string result = restClient.RestRequestAll();

            Territories ter = JsonConvert.DeserializeObject<Territories>(result);

            return View(ter);
        }

        // POST: TerritoriesController/Delete/5
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
