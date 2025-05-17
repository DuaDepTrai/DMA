using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NorthwindAPIClient.Models;

namespace NorthwindAPIClient.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MySettings mst = new MySettings();
        public EmployeesController(IOptions<MySettings> options)
        {
            mst = options.Value;
        }
        // GET: EmployeesController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Employees";
            string result = restClient.RestRequestAll();

            List<Employees> empList = JsonConvert.DeserializeObject<List<Employees>>(result);

            return View(empList);
        }

        public ActionResult ShowImage(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Employees/" + id;
            string result = restClient.RestRequestAll();

            Employees emp = JsonConvert.DeserializeObject<Employees>(result);

            byte[] imageData = emp.Photo is null ? null : emp.Photo.Skip(78).ToArray();
            if (emp != null && emp.Photo != null)
            {
                return File(imageData, "image/jpeg");
            }
            return null;
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Employees/" + id;
            string result = restClient.RestRequestAll();

            Employees emp = JsonConvert.DeserializeObject<Employees>(result);

            return View(emp);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employees obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Employees";
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

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Employees/" + id;
            string result = restClient.RestRequestAll();

            Employees emp = JsonConvert.DeserializeObject<Employees>(result);

            return View(emp);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employees obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Employees";
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

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Employees/" + id;
            string result = restClient.RestRequestAll();

            Employees emp = JsonConvert.DeserializeObject<Employees>(result);

            return View(emp);
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employees obj)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Employees/" + id;
                string result = restClient.RestDeleteObj(obj);
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
    }
}
