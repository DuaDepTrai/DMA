using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTerritoriesController : ControllerBase
    {
        private readonly MyDBContext db;
        public EmployeeTerritoriesController(MyDBContext dbContext)
        {
            db = dbContext;
        }
        // GET: api/<EmployeeTerritoriesController>
        [HttpGet]
        public IEnumerable<EmployeeTerritories> Get()
        {
            return db.EmployeeTerritories
                        .Include(e => e.Employees)
                        .Include(t => t.Territories)
                        .ToList();
        }

        // GET api/<EmployeeTerritoriesController>/5
        [HttpGet("{employeeId}/{territoryId}")]
        public EmployeeTerritories Get(int employeeId, string territoryId)
        {
            return db.EmployeeTerritories
                        .Include(e => e.Employees)
                        .Include(t => t.Territories)
                        .FirstOrDefault();
        }

        // POST api/<EmployeeTerritoriesController>
        [HttpPost]
        public int Post(EmployeeTerritories obj)
        {
            try
            {
                if (db.Employees.Find(obj.EmployeeID) == null)
                {
                    return -1;
                }
                if (db.Territories.Find(obj.TerritoryID) == null)
                {
                    return -2;
                }
                db.EmployeeTerritories.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // PUT api/<EmployeeTerritoriesController>/5
        [HttpPut]
        public int Put(EmployeeTerritories obj)
        {
            try
            {
                if (db.Employees.Find(obj.EmployeeID) == null)
                {
                    return -1;
                }
                if (db.Territories.Find(obj.TerritoryID) == null)
                {
                    return -2;
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

        // DELETE api/<EmployeeTerritoriesController>/5
        [HttpDelete("{id}")]
        public int Delete(int employeeId, string territoryId)
        {
            try
            {
                EmployeeTerritories empTerritory = db.EmployeeTerritories.Find(employeeId, territoryId);
                db.EmployeeTerritories.Remove(empTerritory);
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
