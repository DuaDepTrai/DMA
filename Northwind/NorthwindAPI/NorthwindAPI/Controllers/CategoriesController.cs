using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDBContext db;
        public CategoriesController (MyDBContext dbContext)
        {
            db = dbContext;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<Categories> Get()
        {
            return db.Categories;
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public Categories Get(int id)
        {
            return db.Categories.Find(id);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public int Post(Categories obj)
        {
            try
            {
                if (obj == null || string.IsNullOrEmpty(obj.CategoryName))
                {
                    return -1;
                }
                // Đảm bảo CategoryID không được set (vì là IDENTITY)
                obj.CategoryID = 0;

                db.Categories.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // PUT api/<CategoriesController>/5
        [HttpPut]
        public int Put(Categories obj)
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

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            try
            {
                Categories cat = db.Categories.Find(id);
                db.Categories.Remove(cat);
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
