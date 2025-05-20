using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NewsAPI4Client.Models;
using Newtonsoft.Json;

namespace NewsAPI4Client.Controllers
{
    public class NewsController : Controller
    {
        private readonly MySettings mst = new MySettings();
        public NewsController(IOptions<MySettings> options)
        {
            mst = options.Value;
        }
        // GET: NewsController
        public ActionResult Index(int? categoryId, string searchStr)
        {
            // Lấy danh sách Category
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories";
            string categoriesResult = restClient.RestRequestAll();
            List<Category> categories;
            try
            {
                categories = JsonConvert.DeserializeObject<List<Category>>(categoriesResult);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing categories: {ex.Message}");
                categories = new List<Category>();
                ModelState.AddModelError("", "Failed to load categories.");
            }
            ViewBag.Categories = categories;

            // Lưu searchStr để hiển thị lại trong form
            ViewBag.CurrentSearch = searchStr;

            // Lấy danh sách News
            List<News> newsList = new List<News>();
            restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;

            try
            {
                if (!string.IsNullOrEmpty(searchStr))
                {
                    if (searchStr.Length < 3)
                    {
                        ModelState.AddModelError("searchStr", "Search string must be at least 3 characters.");
                    }
                    else
                    {
                        restClient.endPoint = $"api/News/Search?strsearch={Uri.EscapeDataString(searchStr)}";
                    }
                }
                else if (categoryId.HasValue)
                {
                    restClient.endPoint = $"api/News/GetByCategory?CategoryID={categoryId.Value}";
                }
                else
                {
                    restClient.endPoint = "api/News";
                }

                string newsResult = restClient.RestRequestAll();
                Console.WriteLine($"News API response: {newsResult}");
                newsList = JsonConvert.DeserializeObject<List<News>>(newsResult) ?? new List<News>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing news: {ex.Message}");
                ModelState.AddModelError("", "Failed to load news.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error: {ex.Message}");
                ModelState.AddModelError("", "Failed to connect to news API.");
            }

            return View(newsList);
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/News/" + id;
            string result = restClient.RestRequestAll();

            News news = JsonConvert.DeserializeObject<News>(result);

            return View(news);
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/News";
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

        // GET: NewsController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/News/" + id;
            string result = restClient.RestRequestAll();

            News news = JsonConvert.DeserializeObject<News>(result);

            return View(news);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/News";
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

        // GET: NewsController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/News/" + id;
            string result = restClient.RestRequestAll();

            News news = JsonConvert.DeserializeObject<News>(result);

            return View(news);
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/News/" + id;
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
