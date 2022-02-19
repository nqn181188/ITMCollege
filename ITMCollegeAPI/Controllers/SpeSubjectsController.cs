using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITMCollegeAPI.Models;
using Newtonsoft.Json;

namespace ITMCollegeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeSubjectsController : ControllerBase
    {
        private readonly ITMCollegeContext _context;

        public SpeSubjectsController(ITMCollegeContext context)
        {
            _context = context;
        }

        // GET: api/SpeSubjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeSubject>>> GetSpeSubjects()
        {
            var data = await _context.SpeSubjects.ToListAsync();
            return data;
        }

        // GET: api/SpeSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpeSubject>> GetSpeSubject(int id)
        {
            var speSubject = await _context.SpeSubjects.FindAsync(id);

            if (speSubject == null)
            {
                return NotFound(); ;
            }

            return Ok(speSubject);
        }

        // PUT: api/SpeSubjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpeSubject(int id, SpeSubject speSubject)
        {
            var subject = await _context.SpeSubjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            try
            {
                subject.SubjectName = speSubject.SubjectName;
                subject.FieldId = speSubject.FieldId;
                _context.Update(subject);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // POST: api/SpeSubjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpeSubject>> PostSpeSubject(SpeSubject speSubject)
        {
            _context.SpeSubjects.Add(speSubject);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/SpeSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeSubject(int id)
        {
            var speSubject = await _context.SpeSubjects.FindAsync(id);
            if (speSubject == null)
            {
                return NotFound();
            }

            _context.SpeSubjects.Remove(speSubject);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool SpeSubjectExists(int id)
        {
            return _context.SpeSubjects.Any(e => e.SubjectId == id);
        }
    }
}
