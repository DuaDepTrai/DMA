using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NorthwindAPIClient.Models;

namespace NorthwindAPIClient.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly MySettings mst = new MySettings();
        public CategoriesController(IOptions<MySettings> options)
        {
            mst = options.Value;
        }
        // GET: CategoriesController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            //restClient.BaseUrl = "https://localhost:44374/";
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories";
            string result = restClient.RestRequestAll();

            List<Categories> CatesList = JsonConvert.DeserializeObject<List<Categories>>(result);

            return View(CatesList);
        }

        public ActionResult ShowImage(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories/" + id;
            string result = restClient.RestRequestAll();

            Categories cate = JsonConvert.DeserializeObject<Categories>(result);

            byte[] imageData = cate.Picture is null ? null : cate.Picture.Skip(78).ToArray();
            if (cate != null && cate.Picture != null) 
            {
                return File(imageData, "image/jpeg");
            }
            return null;
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
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
        public ActionResult Create(Categories obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Categories";
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

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories/" + id;
            string result = restClient.RestRequestAll();

            Categories cate = JsonConvert.DeserializeObject<Categories>(result);

            return View(cate);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Categories";
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

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
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
