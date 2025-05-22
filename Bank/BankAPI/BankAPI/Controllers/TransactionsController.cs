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
    public class TransactionsController : ApiController
    {
        private BankTransferEntities db = new BankTransferEntities();

        // GET: api/Transactions
        public IQueryable<Transactions> GetTransactions()
        {
            return db.Transactions;
        }

        // GET: api/Transactions/5
        [ResponseType(typeof(Transactions))]
        public IHttpActionResult GetTransactions(int id)
        {
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return NotFound();
            }

            return Ok(transactions);
        }

        // PUT: api/Transactions/5
        [ResponseType(typeof(void))]
        public int PutTransactions(Transactions transactions)
        {
            try
            {
                db.Entry(transactions).State = EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // POST: api/Transactions
        [ResponseType(typeof(Transactions))]
        public IHttpActionResult PostTransactions(Transactions transactions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra tài khoản gửi và nhận
            var requestAccount = db.Accounts.Find(transactions.RequestID);
            var receiverAccount = db.Accounts.Find(transactions.ReceiverID);

            if (requestAccount == null || receiverAccount == null)
            {
                return BadRequest("Invalid RequestID or ReceiverID.");
            }

            // Kiểm tra số dư
            if (requestAccount.TotalAmount < transactions.Amount)
            {
                return BadRequest("Insufficient balance in request account.");
            }

            // Cập nhật số dư
            requestAccount.TotalAmount -= transactions.Amount;
            receiverAccount.TotalAmount += transactions.Amount;

            // Set TransferTime
            transactions.TransferTime = DateTime.Now;

            // Lưu giao dịch
            db.Transactions.Add(transactions);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = transactions.TransactID }, transactions);
        }

        // DELETE: api/Transactions/5
        [ResponseType(typeof(Transactions))]
        public IHttpActionResult DeleteTransactions(int id)
        {
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(transactions);
            db.SaveChanges();

            return Ok(transactions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionsExists(int id)
        {
            return db.Transactions.Count(e => e.TransactID == id) > 0;
        }
    }
}