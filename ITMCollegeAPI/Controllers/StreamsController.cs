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
    public class StreamsController : ControllerBase
    {
        private readonly ITMCollegeContext _context;

        public StreamsController(ITMCollegeContext context)
        {
            _context = context;
        }

        // GET: api/Streams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stream>>> GetStreams()
        {
            return await _context.Streams.OrderByDescending(i=>i.StreamId).ToListAsync();
        }

        // GET: api/Streams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stream>> GetStream(int id)
        {
            var stream = await _context.Streams.FindAsync(id);

            if (stream == null)
            {
                return NotFound();
            }

            return stream;
        }

        // PUT: api/Streams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStream(int id,[FromForm] Stream stream)
        {
            if (id != stream.StreamId)
            {
                return BadRequest();
            }

            _context.Entry(stream).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreamExists(id))
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

        // POST: api/Streams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stream>> PostStream([FromForm] Stream stream)
        {
            _context.Streams.Add(stream);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStream", new { id = stream.StreamId }, stream);
        }

        // DELETE: api/Streams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStream(int id)
        {
            var stream = await _context.Streams.FindAsync(id);
            if (stream == null)
            {
                return NotFound();
            }

            _context.Streams.Remove(stream);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StreamExists(int id)
        {
            return _context.Streams.Any(e => e.StreamId == id);
        }
    }
}
