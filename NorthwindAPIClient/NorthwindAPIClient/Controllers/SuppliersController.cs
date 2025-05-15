using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthwindAPIClient.Models;

namespace NorthwindAPIClient.Controllers
{
    public class SuppliersController : Controller
    {
        // GET: SuppliersController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Suppliers";
            string result = restClient.RestRequestAll();

            List<Suppliers> SuppliersList = JsonConvert.DeserializeObject<List<Suppliers>>(result);

            return View(SuppliersList);
        }

        // GET: SuppliersController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Suppliers/"+id;
            string result = restClient.RestRequestAll();

            Suppliers supp = JsonConvert.DeserializeObject<Suppliers>(result);

            return View(supp);
        }

        // GET: SuppliersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuppliersController/Create
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

        // GET: SuppliersController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Suppliers/" + id;
            string result = restClient.RestRequestAll();

            Suppliers supp = JsonConvert.DeserializeObject<Suppliers>(result);

            return View(supp);
        }

        // POST: SuppliersController/Edit/5
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

        // GET: SuppliersController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Suppliers/" + id;
            string result = restClient.RestRequestAll();

            Suppliers supp = JsonConvert.DeserializeObject<Suppliers>(result);

            return View(supp);
        }

        // POST: SuppliersController/Delete/5
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
