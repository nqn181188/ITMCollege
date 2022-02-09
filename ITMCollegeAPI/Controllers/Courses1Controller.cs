using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITMCollegeAPI.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ITMCollegeAPI.Repository;

namespace ITMCollegeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Courses1Controller : ControllerBase
    {
        private readonly ITMCollegeContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        ICourseRepository _courseRepository;

        public Courses1Controller(ITMCollegeContext context, IWebHostEnvironment hostEnvironment, ICourseRepository courseRepository)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _courseRepository = courseRepository;
        }

        [HttpGet]
        [Route("GetFields")]
        public async Task<IActionResult> GetFields()
        {
            try
            {
                var fields = await _courseRepository.GetFields();
                if (fields == null)
                {
                    return NotFound();
                }
                return Ok(fields);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetStreams")]
        public async Task<IActionResult> GetStreams()
        {
            try
            {
                var streams = await _courseRepository.GetStreams();
                if (streams == null)
                {
                    return NotFound();
                }
                return Ok(streams);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            try
            {
                var courses = await _courseRepository.GetCourses();
                if (courses == null)
                {
                    return NotFound();
                }
                return Ok(courses);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var course = await _courseRepository.GetCourse(id);
                if (course == null)
                {
                    return NotFound();
                }
                return Ok(course);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/Courses/5
        // To protect from overCourseing attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, [FromForm] Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }
            _context.Entry(course).State = EntityState.Modified;

            try
            {
                course.Image = await SaveImage(course.ImageFile);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // Course: api/Courses
        // To protect from overCourseing attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse([FromForm]Course course)
        {
           
            

            if (ModelState.IsValid)
            {
                try
                {
                    course.Image = await SaveImage(course.ImageFile);
                    var courseId = await _courseRepository.AddCourse(course);
                    if (courseId > 0)
                    {
                        return Ok(courseId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using(var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}
