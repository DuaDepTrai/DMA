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
using BankAPI;

namespace BankAPI.Controllers
{
    public class AccountsController : ApiController
    {
        private BankTransferEntities db = new BankTransferEntities();

        // GET: api/Accounts
        public IQueryable<Accounts> GetAccounts()
        {
            return db.Accounts.Include(a => a.Users);
        }

        // GET: api/Accounts/5
        [ResponseType(typeof(Accounts))]
        public IHttpActionResult GetAccounts(int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        // PUT: api/Accounts/5
        [ResponseType(typeof(void))]
        public int PutAccounts(Accounts accounts)
        {
            try
            {
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // POST: api/Accounts
        [ResponseType(typeof(Accounts))]
        public IHttpActionResult PostAccounts(Accounts accounts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accounts.Add(accounts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = accounts.AccountID }, accounts);
        }

        // DELETE: api/Accounts/5
        [ResponseType(typeof(Accounts))]
        public IHttpActionResult DeleteAccounts(int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return NotFound();
            }

            db.Accounts.Remove(accounts);
            db.SaveChanges();

            return Ok(accounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountsExists(int id)
        {
            return db.Accounts.Count(e => e.AccountID == id) > 0;
        }

        // GET: api/Accounts?userId={userId}
        [ResponseType(typeof(IQueryable<Accounts>))]
        public IHttpActionResult GetAccountsByUser(int? userId)
        {
            if (!userId.HasValue)
            {
                return BadRequest("userId is required.");
            }

            var accounts = db.Accounts
                .Include(a => a.Users)
                .Where(a => a.UserID == userId.Value);

            if (!accounts.Any())
            {
                return NotFound();
            }

            return Ok(accounts);
        }
    }
}