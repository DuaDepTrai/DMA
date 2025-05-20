using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDBContext db;
        public OrdersController(MyDBContext dbContext)
        {
            db = dbContext;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<Orders> Get()
        {
            return db.Orders.Include(c => c.Customers)
                            .Include(e => e.Employees)
                            .Include(s => s.Shippers)
                            .ToList();
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public Orders Get(int id)
        {
            return db.Orders.Include(c => c.Customers)
                            .Include(e => e.Employees)
                            .Include(s => s.Shippers)
                            .FirstOrDefault();
        }

        // POST api/<OrdersController>
        [HttpPost]
        public int Post(Orders obj)
        {
            try
            {
                if (obj.CustomerID != null && db.Suppliers.Find(obj.CustomerID) == null)
                {
                    return -1;
                }
                if (obj.EmployeeID != null && db.Categories.Find(obj.EmployeeID) == null)
                {
                    return -2;
                }
                if (obj.ShipVia != null && db.Shippers.Find(obj.ShipVia) == null)
                {
                    return -3;
                }
                db.Orders.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // PUT api/<OrdersController>/5
        [HttpPut]
        public int Put(Orders obj)
        {
            try
            {
                if (obj.CustomerID != null && db.Suppliers.Find(obj.CustomerID) == null)
                {
                    return -1;
                }
                if (obj.EmployeeID != null && db.Categories.Find(obj.EmployeeID) == null)
                {
                    return -2;
                }
                if (obj.ShipVia != null && db.Shippers.Find(obj.ShipVia) == null)
                {
                    return -3;
                }
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            try
            {
                Orders order = db.Orders.Find(id);
                db.Orders.Remove(order);
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
