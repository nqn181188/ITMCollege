using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITMCollegeAPI.Models;

namespace ITMCollegeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpSubjectsController : ControllerBase
    {
        private readonly ITMCollegeContext _context;

        public OpSubjectsController(ITMCollegeContext context)
        {
            _context = context;
        }

        // GET: api/OpSubjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpSubject>>> GetOpSubjects()
        {
            return await _context.OpSubjects.ToListAsync();
        }

        // GET: api/OpSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OpSubject>> GetOpSubject(int id)
        {
            var opSubject = await _context.OpSubjects.FindAsync(id);

            if (opSubject == null)
            {
                return NotFound();
            }

            return opSubject;
        }

        // PUT: api/OpSubjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpSubject(int id, OpSubject opSubject)
        {
            var subject = await _context.OpSubjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            try
            {
                subject = opSubject;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // POST: api/OpSubjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OpSubject>> PostOpSubject(OpSubject opSubject)
        {
            _context.OpSubjects.Add(opSubject);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/OpSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpSubject(int id)
        {
            var opSubject = await _context.OpSubjects.FindAsync(id);
            if (opSubject == null)
            {
                return NotFound();
            }

            _context.OpSubjects.Remove(opSubject);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool OpSubjectExists(int id)
        {
            return _context.OpSubjects.Any(e => e.SubjectId == id);
        }
    }
}
