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
    public class AdmissionsController : ControllerBase
    {
        private readonly ITMCollegeContext _context;

        public AdmissionsController(ITMCollegeContext context)
        {
            _context = context;
        }

        // GET: api/Admissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admission>>> GetAdmissions()
        {
            return await _context.Admissions.ToListAsync();
        }

        // GET: api/Admissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admission>> GetAdmission(long id)
        {
            var admission = await _context.Admissions.FindAsync(id);

            if (admission == null)
            {
                return NotFound();
            }

            return admission;
        }

        // PUT: api/Admissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmission(long id, Admission admission)
        {
            if (id != admission.AdmissionId)
            {
                return BadRequest();
            }

            _context.Entry(admission).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdmissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        // POST: api/Admissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admission>> PostAdmission(Admission admission)
        {
            _context.Admissions.Add(admission);
            await _context.SaveChangesAsync();
            return Ok(admission);
        }
        // DELETE: api/Admissions/5
        private bool AdmissionExists(long id)
        {
            return _context.Admissions.Any(e => e.AdmissionId == id);
        }
        [HttpGet("CheckRegNumber/{regnum}")]
        public bool CheckRegNumberExits(string regnum)
        {
            var admission = _context.Admissions.Where(a => a.RegNum.Equals(regnum)).FirstOrDefault();
            if (admission == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
