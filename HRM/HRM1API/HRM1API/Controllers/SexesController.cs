using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRM1API.Models;

namespace HRM1API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SexesController : ControllerBase
    {
        private readonly MyDBContext _context;

        public SexesController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/Sexes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sex>>> GetSex()
        {
            return await _context.Sex.ToListAsync();
        }

        // GET: api/Sexes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sex>> GetSex(int id)
        {
            var sex = await _context.Sex.FindAsync(id);

            if (sex == null)
            {
                return NotFound();
            }

            return sex;
        }

        // PUT: api/Sexes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSex(int id, Sex sex)
        {
            if (id != sex.SexID)
            {
                return BadRequest();
            }

            _context.Entry(sex).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SexExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sexes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sex>> PostSex(Sex sex)
        {
            _context.Sex.Add(sex);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSex", new { id = sex.SexID }, sex);
        }

        // DELETE: api/Sexes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSex(int id)
        {
            var sex = await _context.Sex.FindAsync(id);
            if (sex == null)
            {
                return NotFound();
            }

            _context.Sex.Remove(sex);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SexExists(int id)
        {
            return _context.Sex.Any(e => e.SexID == id);
        }
    }
}
