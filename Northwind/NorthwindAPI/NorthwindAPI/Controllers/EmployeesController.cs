using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly MyDBContext db;
        public EmployeesController(MyDBContext dbContext)
        {
            db = dbContext;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public IEnumerable<Employees> Get()
        {
            return db.Employees.ToList();
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public Employees Get(int id)
        {
            return db.Employees.Find(id);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public int Post(Employees obj)
        {
            try
            {
                if (obj == null)
                {
                    return -2;
                }
                if (obj.ReportsTo != null && db.Employees.Find(obj.ReportsTo) == null) 
                {
                    return -1;
                }
                obj.EmployeeID = 0;

                db.Employees.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception) 
            {
                return 0;
            }
        }

        // PUT api/<EmployeesController>/5
        [HttpPut]
        public int Put(Employees obj)
        {
            try
            {
                if (obj.ReportsTo != null && db.Employees.Find(obj.ReportsTo) == null)
                {
                    return -1;
                }
                db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            try
            {
                Employees emp = db.Employees.Find(id) ;
                db.Employees.Remove(emp);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
