using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly MyDBContext db;
        public OrderDetailsController(MyDBContext dbContext)
        {
            db = dbContext;
        }
        // GET: api/<OrderDetailsController>
        [HttpGet]
        public IEnumerable<OrderDetails> Get()
        {
            return db.OrderDetails
                        .Include(o => o.Orders)
                        .Include(p => p.Products)
                        .ToList(); ;
        }

        // GET api/<OrderDetailsController>/5
        [HttpGet("{orderID}/{productID}")]
        public OrderDetails Get(int orderID, string productID)
        {
            return db.OrderDetails
                        .Include(e => e.Orders)
                        .Include(t => t.Products)
                        .FirstOrDefault();
        }

        // POST api/<OrderDetailsController>
        [HttpPost]
        public int Post(OrderDetails obj)
        {
            try
            {
                if (db.Orders.Find(obj.OrderID) == null)
                {
                    return -1;
                }
                if (db.Products.Find(obj.ProductID) == null)
                {
                    return -2;
                }
                db.OrderDetails.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // PUT api/<OrderDetailsController>/5
        [HttpPut]
        public int Put(OrderDetails obj)
        {
            try
            {
                if (db.Orders.Find(obj.OrderID) == null)
                {
                    return -1;
                }
                if (db.Products.Find(obj.ProductID) == null)
                {
                    return -2;
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

        // DELETE api/<OrderDetailsController>/5
        [HttpDelete("{id}")]
        public int Delete(int orderID, string productID)
        {
            try
            {
                OrderDetails orderDetail = db.OrderDetails.Find(orderID, productID);
                db.OrderDetails.Remove(orderDetail);
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
