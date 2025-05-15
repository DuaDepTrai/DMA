using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthwindAPIClient.Models;

namespace NorthwindAPIClient.Controllers
{
    public class ShippersController : Controller
    {
        // GET: ShippersController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Shippers";
            string result = restClient.RestRequestAll();

            List<Shippers> ShippersList = JsonConvert.DeserializeObject<List<Shippers>>(result);

            return View(ShippersList);
        }

        // GET: ShippersController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Shippers/" + id;
            string result = restClient.RestRequestAll();

            Shippers ship = JsonConvert.DeserializeObject<Shippers>(result);

            return View(ship);
        }

        // GET: ShippersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShippersController/Create
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

        // GET: ShippersController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Shippers/" + id;
            string result = restClient.RestRequestAll();

            Shippers ship = JsonConvert.DeserializeObject<Shippers>(result);

            return View(ship);
        }

        // POST: ShippersController/Edit/5
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

        // GET: ShippersController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Shippers/" + id;
            string result = restClient.RestRequestAll();

            Shippers ship = JsonConvert.DeserializeObject<Shippers>(result);

            return View(ship);
        }

        // POST: ShippersController/Delete/5
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
