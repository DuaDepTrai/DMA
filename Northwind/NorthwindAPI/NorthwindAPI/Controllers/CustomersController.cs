using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MyDBContext db;
        public CustomersController(MyDBContext dbContext)
        {
            db = dbContext;
        }

        // GET: api/<Customers>
        [HttpGet]
        public IEnumerable<Customers> Get()
        {
            return db.Customers;
        }

        // GET api/<Customers>/5
        [HttpGet("{id}")]
        public Customers Get(string id)
        {
            return db.Customers.Find(id);
        }

        // POST api/<Customers>
        [HttpPost]
        public int Post(Customers obj)
        {
            try
            {
                db.Customers.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception) 
            {
                return 0;
            }
        }

        // PUT api/<Customers>/5
        [HttpPut]
        public int Put(Customers obj)
        {
            try
            {
                db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch (Exception) 
            {
                return 0;
            }
        }

        // DELETE api/<Customers>/5
        [HttpDelete("{id}")]
        public int Delete(string id)
        {
            try
            {
                Customers cus = db.Customers.Find(id);
                db.Customers.Remove(cus);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
