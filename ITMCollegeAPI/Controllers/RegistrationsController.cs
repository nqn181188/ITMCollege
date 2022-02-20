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
    public class RegistrationsController : ControllerBase
    {
        private readonly ITMCollegeContext _context;

        public RegistrationsController(ITMCollegeContext context)
        {
            _context = context;
        }

        // GET: api/Registrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
            return await _context.Registrations.ToListAsync();
        }

        // GET: api/Registrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetRegistration(long id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var registration = await _context.Registrations.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        // PUT: api/Registrations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPost]
        public async Task<ActionResult<Registration>> PostRegistration(Registration registration)
        {
            try
            {
                _context.Registrations.Add(registration);
                await _context.SaveChangesAsync();
                return Ok(registration);
            }
            catch
            {
                return BadRequest();
            }
        }
        private bool RegistrationExists(long id)
        {
            return _context.Registrations.Any(e => e.RegistrationId == id);
        }
    }
}
