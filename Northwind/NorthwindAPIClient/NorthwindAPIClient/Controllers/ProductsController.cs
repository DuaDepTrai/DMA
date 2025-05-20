using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NorthwindAPIClient.Models;

namespace NorthwindAPIClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MySettings mst = new MySettings();
        public ProductsController(IOptions<MySettings> options)
        {
            mst = options.Value;
        }
        // GET: ProductsController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Products";
            string result = restClient.RestRequestAll();

            List<Products> ProductsList = JsonConvert.DeserializeObject<List<Products>>(result);

            return View(ProductsList);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Products/" + id;
            string result = restClient.RestRequestAll();

            Products prod = JsonConvert.DeserializeObject<Products>(result);

            return View(prod);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Products";
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

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Products/" + id;
            string result = restClient.RestRequestAll();

            Products prod = JsonConvert.DeserializeObject<Products>(result);

            return View(prod);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Products";
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

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Products/" + id;
            string result = restClient.RestRequestAll();

            Products prod = JsonConvert.DeserializeObject<Products>(result);

            return View(prod);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Products/" + id;
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
