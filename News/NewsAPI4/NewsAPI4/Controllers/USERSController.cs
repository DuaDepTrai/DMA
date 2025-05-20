using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NewsAPI4;

namespace NewsAPI4.Controllers
{
    public class USERSController : ApiController
    {
        private NEWSEntities db = new NEWSEntities();

        // GET: api/USERS
        public IQueryable<USERS> GetUSERS()
        {
            return db.USERS;
        }

        // GET: api/USERS/5
        [ResponseType(typeof(USERS))]
        public IHttpActionResult GetUSERS(int id)
        {
            USERS uSERS = db.USERS.Find(id);
            if (uSERS == null)
            {
                return NotFound();
            }

            return Ok(uSERS);
        }

        // PUT: api/USERS/5
        [ResponseType(typeof(void))]
        public int PutUSERS(USERS uSERS)
        {
            try
            {
                db.Entry(uSERS).State = EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // POST: api/USERS
        [ResponseType(typeof(USERS))]
        public IHttpActionResult PostUSERS(USERS uSERS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USERS.Add(uSERS);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = uSERS.UsersID }, uSERS);
        }

        // DELETE: api/USERS/5
        [ResponseType(typeof(USERS))]
        public IHttpActionResult DeleteUSERS(int id)
        {
            USERS uSERS = db.USERS.Find(id);
            if (uSERS == null)
            {
                return NotFound();
            }

            if (uSERS.UserName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return Content(HttpStatusCode.Forbidden, "Cannot remove user Admin.");
            }

            db.USERS.Remove(uSERS);
            db.SaveChanges();

            return Ok(uSERS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USERSExists(int id)
        {
            return db.USERS.Count(e => e.UsersID == id) > 0;
        }

        [Route("api/Login")]
        [HttpGet]
        public USERS CheckLogin(string userName, string password)
        {
            return db.USERS.Where(u => u.UserName == userName && u.Password == password)
                            .FirstOrDefault();
        }

        [HttpGet]
        [Route("api/CheckUsername")]
        public bool CheckUserNameExists(string userName)
        {
            return db.USERS.Any(u => u.UserName == userName);
        }

    }
}