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
    public class NEWSController : ApiController
    {
        private NEWSEntities db = new NEWSEntities();

        // GET: api/NEWS
        public IHttpActionResult GetNEWS()
        {
            try
            {
                return Ok(db.NEWS.ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/NEWS/5
        [ResponseType(typeof(NEWS))]
        public IHttpActionResult GetNEWS(int id)
        {
            try
            {
                NEWS nEWS = db.NEWS.Find(id);
                if (nEWS == null)
                {
                    return NotFound();
                }
                return Ok(nEWS);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/NEWS/5
        [ResponseType(typeof(void))]
        public int PutNEWS(NEWS nEWS)
        {
            try
            {
                if (nEWS.CategoryID != null && db.Category.Find(nEWS.CategoryID) == null)
                {
                    return -1;
                }
                if (nEWS.UsersID != null && db.USERS.Find(nEWS.UsersID) == null)
                {
                    return -2;
                }
                db.Entry(nEWS).State = EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // POST: api/NEWS
        [ResponseType(typeof(NEWS))]
        public IHttpActionResult PostNEWS(NEWS nEWS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NEWS.Add(nEWS);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nEWS.ID }, nEWS);
        }

        // DELETE: api/NEWS/5
        [ResponseType(typeof(NEWS))]
        public IHttpActionResult DeleteNEWS(int id)
        {
            NEWS nEWS = db.NEWS.Find(id);
            if (nEWS == null)
            {
                return NotFound();
            }

            db.NEWS.Remove(nEWS);
            db.SaveChanges();

            return Ok(nEWS);
        }

        // GET: api/NEWS/Search?strsearch=...
        [Route("api/NEWS/Search")]
        [HttpGet]
        [ResponseType(typeof(List<NEWS>))]
        public IHttpActionResult Search(string strsearch)
        {
            try
            {
                if (string.IsNullOrEmpty(strsearch))
                {
                    return BadRequest("Search string cannot be empty.");
                }

                var news = db.NEWS
            .ToList() // Đưa dữ liệu vào bộ nhớ
            .Where(n => (n.tieu_de != null && n.tieu_de.IndexOf(strsearch, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (n.tom_tat != null && n.tom_tat.IndexOf(strsearch, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (n.noi_dung != null && n.noi_dung.IndexOf(strsearch, StringComparison.OrdinalIgnoreCase) >= 0))
            .ToList();
                return Ok(news);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/NEWS/GetByCategory?CategoryID=...
        [Route("api/NEWS/GetByCategory")]
        [HttpGet]
        [ResponseType(typeof(List<NEWS>))]
        public IHttpActionResult GetNewByCategoryID(int CategoryID)
        {
            try
            {
                var news = db.NEWS
                    .Where(n => n.CategoryID == CategoryID)
                    .ToList();
                if (news == null || !news.Any())
                {
                    return NotFound();
                }
                return Ok(news);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NEWSExists(int id)
        {
            return db.NEWS.Count(e => e.ID == id) > 0;
        }
    }
}