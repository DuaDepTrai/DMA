using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NorthwindAPIClient.Models;

namespace NorthwindAPIClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly MySettings mst = new MySettings();
        public UsersController(IOptions<MySettings> options)
        {
            mst = options.Value;
        }
        // GET: UsersController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Users";
            string result = restClient.RestRequestAll();

            List<Users> usersList = JsonConvert.DeserializeObject<List<Users>>(result);

            return View(usersList);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Users";
            string result = restClient.RestRequestAll();

            Users user = JsonConvert.DeserializeObject<Users>(result);

            return View(user);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Users";
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

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Users";
            string result = restClient.RestRequestAll();

            Users user = JsonConvert.DeserializeObject<Users>(result);

            return View(user);
        }

        // POST: UsersController/Edit/5
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

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Users";
            string result = restClient.RestRequestAll();

            Users user = JsonConvert.DeserializeObject<Users>(result);

            return View(user);
        }

        // POST: UsersController/Delete/5
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
