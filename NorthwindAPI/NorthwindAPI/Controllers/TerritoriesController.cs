using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoriesController : ControllerBase
    {
        private readonly MyDBContext db;
        public TerritoriesController(MyDBContext dbContext)
        {
            db = dbContext;
        }

        // GET: api/<TerritoriesController>
        [HttpGet]
        public IEnumerable<Territories> Get()
        {
            return db.Territories;
        }

        // GET api/<TerritoriesController>/5
        [HttpGet("{id}")]
        public Territories Get(string id)
        {
            return db.Territories.Find(id);
        }

        // POST api/<TerritoriesController>
        [HttpPost]
        public int Post(Territories obj)
        {
            try
            {
                if (db.Region.Find(obj.RegionID) == null) 
                {
                    return 0;
                }
                db.Territories.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        // PUT api/<TerritoriesController>/5
        [HttpPut]
        public int Put(Territories obj)
        {
            try
            {
                if (db.Region.Find(obj.RegionID) == null)
                {
                    return 0;
                }
                db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch (Exception) 
            {
                return -1;
            }
        }

        // DELETE api/<TerritoriesController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            try
            {
                Territories ter = db.Territories.Find(id);
                db.Territories.Remove(ter);
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
