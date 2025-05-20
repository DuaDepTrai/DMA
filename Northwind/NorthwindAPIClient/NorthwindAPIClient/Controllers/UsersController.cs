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
            restClient.endPoint = "api/Users/" + id;
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
            restClient.endPoint = "api/Users/" + id;
            string result = restClient.RestRequestAll();

            Users user = JsonConvert.DeserializeObject<Users>(result);

            return View(user);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Users";
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

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Users/" + id;
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
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Users/" + id;
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

        // GET: Users/Login
        [HttpGet]
        public ActionResult Login()
        {
            Users user = new Users();

            // Kiểm tra cookie để điền UserName
            if (Request.Cookies["UserName"] != null)
            {
                user.UserName = Request.Cookies["UserName"];
            }

            return View(user);
        }

        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user, bool Remember)
        {
            // Log dữ liệu nhận được
            Console.WriteLine($"Received: UserName={user.UserName}, Password={user.Password}, Remember={Remember}");

            // Log ModelState để debug
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine("ModelState errors: " + string.Join(", ", errors));
            }

            if (true) // Bỏ ModelState.IsValid
            {
                // Gọi API Login
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = $"api/Users/Login?userName={Uri.EscapeDataString(user.UserName)}&password={Uri.EscapeDataString(user.Password)}";
                string result = restClient.RestRequestAll();
                Console.WriteLine($"API response: {result}");

                // Kiểm tra kết quả API
                Users matchedUser = JsonConvert.DeserializeObject<Users>(result);
                if (matchedUser != null)
                {
                    // Ghi cookie nếu chọn Remember
                    if (Remember)
                    {
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(30),
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict
                        };
                        Response.Cookies.Append("UserName", user.UserName, cookieOptions);
                    }

                    // Lưu session
                    HttpContext.Session.SetInt32("UserID", matchedUser.UserID);
                    HttpContext.Session.SetString("UserName", matchedUser.UserName);
                    Console.WriteLine($"Session set: UserName={matchedUser.UserName}");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(user);
        }
    }
}
