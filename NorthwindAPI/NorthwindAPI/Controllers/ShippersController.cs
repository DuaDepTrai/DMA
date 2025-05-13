using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly MyDBContext db;
        public ShippersController(MyDBContext dbContext)
        {
            db = dbContext;
        }

        // GET: api/<ShippersController>
        [HttpGet]
        public IEnumerable<Shippers> Get()
        {
            return db.Shippers;
        }

        // GET api/<ShippersController>/5
        [HttpGet("{id}")]
        public Shippers Get(int id)
        {
            return db.Shippers.Find(id);
        }

        // POST api/<ShippersController>
        [HttpPost]
        public int Post(Shippers obj)
        {
            try
            {
                db.Shippers.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception) 
            {
                return -1;
            }
        }

        // PUT api/<ShippersController>/5
        [HttpPut]
        public int Put(Shippers obj)
        {
            try
            {
                db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch (Exception) 
            {
                return -1;
            }
        }

        // DELETE api/<ShippersController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            try
            {
                Shippers shipper = db.Shippers.Find(id);
                db.Shippers.Remove(shipper);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
