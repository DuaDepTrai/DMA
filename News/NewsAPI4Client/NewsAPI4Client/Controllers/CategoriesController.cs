using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NewsAPI4Client.Models;
using Newtonsoft.Json;

namespace NewsAPI4Client.Controllers
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
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories";
            string result = restClient.RestRequestAll();

            List<Category> CateList = JsonConvert.DeserializeObject<List<Category>>(result);

            return View(CateList);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories/" + id;
            string result = restClient.RestRequestAll();

            Category cate = JsonConvert.DeserializeObject<Category>(result);

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
        public ActionResult Create(Category obj)
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

            Category cate = JsonConvert.DeserializeObject<Category>(result);

            return View(cate);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category obj)
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

            Category cate = JsonConvert.DeserializeObject<Category>(result);

            return View(cate);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Categories/" + id;
                string result = restClient.RestDeleteObj();
                if (result == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = result; // Giả sử result chứa thông điệp lỗi từ API
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error occurred while deleting category: " + ex.Message;
                return View();
            }
        }
    }
}
