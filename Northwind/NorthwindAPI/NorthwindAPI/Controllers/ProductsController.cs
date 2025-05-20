using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDBContext db;
        public ProductsController(MyDBContext dbContext)
        {
            db = dbContext;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Products> Get()
        {
            return db.Products.Include(s => s.Suppliers).Include(c=>c.Categories).ToList();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public Products Get(int id)
        {
            return db.Products.Include(s => s.Suppliers)
                                .Include(c => c.Categories)
                                .FirstOrDefault();
        }

        // POST api/<ProductsController>
        [HttpPost]
        public int Post(Products obj)
        {
            try
            {
                if (obj.SupplierID != null && db.Suppliers.Find(obj.SupplierID) == null)
                {
                    return -1;
                }
                if (obj.CategoryID != null && db.Categories.Find(obj.CategoryID) == null)
                {
                    return -2;
                }
                db.Products.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception) 
            {
                return 0;
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut]
        public int Put(Products obj)
        {
            try
            {
                if (obj.SupplierID != null && db.Suppliers.Find(obj.SupplierID) == null)
                {
                    return -1;
                }
                if (obj.CategoryID != null && db.Categories.Find(obj.CategoryID) == null)
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

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            try
            {
                Products product = db.Products.Find(id);
                db.Products.Remove(product);
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
