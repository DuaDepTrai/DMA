using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDBContext db;
        public UsersController(MyDBContext dbContext)
        {
            db = dbContext;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return db.Users.Include(u => u.Employees).ToList();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public Users Get(int id)
        {
            return db.Users.Find(id);
        }

        [Route("Login")]
        [HttpGet]
        public Users Login(string userName, string password)
        {
            return db.Users.Include(e => e.Employees)
                            .Where(u => u.UserName == userName && u.Password == password)
                            .FirstOrDefault();
        }

        // POST api/<UsersController>
        [HttpPost]
        public int Post(Users obj)
        {
            try
            {
                if (obj.EmployeeID != null && db.Employees.Find(obj.EmployeeID) == null)
                {
                    return -1;
                }
                db.Users.Add(obj);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public int Put(Users obj)
        {
            try
            {
                if (obj.EmployeeID != null && db.Employees.Find(obj.EmployeeID) == null)
                {
                    return -1;
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

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            try
            {
                Users user = db.Users.Find(id);
                db.Users.Remove(user);
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
