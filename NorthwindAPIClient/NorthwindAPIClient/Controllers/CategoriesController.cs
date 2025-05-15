using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthwindAPIClient.Models;

namespace NorthwindAPIClient.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: CategoriesController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Categories";
            string result = restClient.RestRequestAll();

            List<Categories> CatesList = JsonConvert.DeserializeObject<List<Categories>>(result);

            return View(CatesList);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Categories/" + id;
            string result = restClient.RestRequestAll();

            Categories cate = JsonConvert.DeserializeObject<Categories>(result);

            return View(cate);
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
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

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Categories/" + id;
            string result = restClient.RestRequestAll();

            Categories cate = JsonConvert.DeserializeObject<Categories>(result);

            return View(cate);
        }

        // POST: CategoriesController/Edit/5
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

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = "https://localhost:44374/";
            restClient.endPoint = "api/Categories/" + id;
            string result = restClient.RestRequestAll();

            Categories cate = JsonConvert.DeserializeObject<Categories>(result);

            return View(cate);
        }

        // POST: CategoriesController/Delete/5
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
