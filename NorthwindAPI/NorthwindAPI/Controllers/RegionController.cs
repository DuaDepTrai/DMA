using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly MyDBContext _dbContext;
        public RegionController(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<Region>
        [HttpGet]
        public IEnumerable<Region> Get()
        {

            return _dbContext.Region;
        }

        // GET api/<Region>/5
        [HttpGet("{id}")]
        public Region Get(int id)
        {
            return _dbContext.Region.Find(id);
        }

        // POST api/<Region>
        //[Route("api/[RegionTS]")]
        //[HttpPost]
        //public int Post(string RegionDescription)
        //{
        //    try
        //    {
        //        Region region = new Region();
        //        region.RegionDescription = RegionDescription;
        //        region.RegionID = _dbContext.Region.Max(r => r.RegionID) + 1;
        //        _dbContext.Region.Add(region);
        //        _dbContext.SaveChanges();
        //        return 1;
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //}

        // POST api/<Region>
        [HttpPost]
        public int Post(Region obj)
        {
            try
            {
                obj.RegionID = _dbContext.Region.Max(r => r.RegionID) + 1;
                _dbContext.Region.Add(obj);
                _dbContext.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // PUT api/<Region>/5
        [HttpPut]
        public int Put(Region obj)
        {
            try
            {
                _dbContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                return obj.RegionID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // DELETE api/<Region>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            try
            {
                Region region = _dbContext.Region.Find(id);
                _dbContext.Region.Remove(region);
                _dbContext.SaveChanges();
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
