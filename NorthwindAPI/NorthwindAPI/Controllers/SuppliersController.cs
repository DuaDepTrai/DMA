using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly MyDBContext db;
        public SuppliersController(MyDBContext dbContext)
        {
            db = dbContext;
        }

        // GET: api/<SuppliersController>
        [HttpGet]
        public IEnumerable<Suppliers> Get()
        {
            return db.Suppliers;
        }

        // GET api/<SuppliersController>/5
        [HttpGet("{id}")]
        public Suppliers Get(int id)
        {
            return db.Suppliers.Find(id);
        }

        // POST api/<SuppliersController>
        [HttpPost]
        public int Post(Suppliers obj)
        {
            try
            {
                db.Suppliers.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // PUT api/<SuppliersController>/5
        [HttpPut]
        public int Put(Suppliers obj)
        {
            try
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            try
            {
                Suppliers sup = db.Suppliers.Find(id);
                db.Suppliers.Remove(sup);
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
