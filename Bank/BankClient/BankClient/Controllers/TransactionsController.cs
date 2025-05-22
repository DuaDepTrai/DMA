using System.Buffers.Text;
using BankClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NewsAPI4Client.Models;
using Newtonsoft.Json;

namespace BankClient.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly MySettings mst = new MySettings();
        public TransactionsController(IOptions<MySettings> options)
        {
            mst = options.Value;
        }
        // GET: TransactionsController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Transactions";
            string result = restClient.RestRequestAll();

            List<Transactions> transList = JsonConvert.DeserializeObject<List<Transactions>>(result);

            return View(transList);
        }

        // GET: TransactionsController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Transactions/" + id;
            string result = restClient.RestRequestAll();

            Transactions trans = JsonConvert.DeserializeObject<Transactions>(result);

            return View(trans);
        }

        // GET: TransactionsController/Create
        public ActionResult Create()
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Users";
            string usersResult = restClient.RestRequestAll();
            var users = JsonConvert.DeserializeObject<List<Users>>(usersResult);
            ViewBag.Users = users;

            restClient.endPoint = "api/Accounts";
            string accountsResult = restClient.RestRequestAll();
            var accounts = JsonConvert.DeserializeObject<List<Accounts>>(accountsResult);
            ViewBag.Accounts = accounts;

            return View(new Transactions());
        }

        // POST: TransactionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transactions obj, int? UserID)
        {
            try
            {
                // Không cần gửi TransferTime, server sẽ tự set
                obj.TransferTime = null;

                // Kiểm tra UserID và RequestID
                if (!UserID.HasValue || obj.RequestID == 0)
                {
                    ModelState.AddModelError("", "Please select a valid user and account.");
                    // Lấy lại dữ liệu cho ViewBag
                    RestClient restClient = new RestClient();
                    restClient.BaseUrl = mst.BaseUrl;
                    restClient.endPoint = "api/Users";
                    string usersResult = restClient.RestRequestAll();
                    ViewBag.Users = JsonConvert.DeserializeObject<List<Users>>(usersResult);

                    restClient.endPoint = "api/Accounts";
                    string accountsResult = restClient.RestRequestAll();
                    ViewBag.Accounts = JsonConvert.DeserializeObject<List<Accounts>>(accountsResult);
                    return View(obj);
                }

                RestClient restClientPost = new RestClient();
                restClientPost.BaseUrl = mst.BaseUrl;
                restClientPost.endPoint = "api/Transactions";
                string result = restClientPost.RestPostObj(obj);

                if (result == "Success")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Lấy lại dữ liệu cho ViewBag
                    RestClient restClient = new RestClient();
                    restClient.BaseUrl = mst.BaseUrl;
                    restClient.endPoint = "api/Users";
                    string usersResult = restClient.RestRequestAll();
                    ViewBag.Users = JsonConvert.DeserializeObject<List<Users>>(usersResult);

                    restClient.endPoint = "api/Accounts";
                    string accountsResult = restClient.RestRequestAll();
                    ViewBag.Accounts = JsonConvert.DeserializeObject<List<Accounts>>(accountsResult);

                    ModelState.AddModelError("", "Failed to create transaction: " + result);
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                // Lấy lại dữ liệu cho ViewBag
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Users";
                string usersResult = restClient.RestRequestAll();
                ViewBag.Users = JsonConvert.DeserializeObject<List<Users>>(usersResult);

                restClient.endPoint = "api/Accounts";
                string accountsResult = restClient.RestRequestAll();
                ViewBag.Accounts = JsonConvert.DeserializeObject<List<Accounts>>(accountsResult);

                ModelState.AddModelError("", "Error: " + ex.Message);
                return View(obj);
            }
        }

        // GET: TransactionsController/GetAccountsByUser
        [HttpGet]
        public ActionResult GetAccountsByUser(int userId)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = $"api/Accounts?userId={userId}";
            string result = restClient.RestRequestAll();
            var accounts = JsonConvert.DeserializeObject<List<Accounts>>(result);
            return Json(accounts);
        }

        // GET: TransactionsController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Transactions/" + id;
            string result = restClient.RestRequestAll();

            Transactions trans = JsonConvert.DeserializeObject<Transactions>(result);

            return View(trans);
        }

        // POST: TransactionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transactions obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Transactions";
                string result = restClient.RestPutObj(obj);
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

        // GET: TransactionsController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Transactions/" + id;
            string result = restClient.RestRequestAll();

            Transactions trans = JsonConvert.DeserializeObject<Transactions>(result);

            return View(trans);
        }

        // POST: TransactionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Transactions";
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
