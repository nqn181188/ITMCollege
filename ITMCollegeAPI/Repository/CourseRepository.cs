using ITMCollegeAPI.Models;
using ITMCollegeAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollegeAPI.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ITMCollegeContext _context;

        public CourseRepository(ITMCollegeContext context)
        {
            _context = context;
        }

        public async Task<int> AddCourse(Course course)
        {
            if (_context != null)
            {
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
                return course.CourseId;
            }
            return 0;
        }

        public async Task<int> DeleteCourse(int? id)
        {
            int result = 0;
            if (_context != null)
            {
                var delCourse = await _context.Courses.FindAsync(id);
                if (delCourse != null)
                {
                    _context.Courses.Remove(delCourse);
                    result = await _context.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<CourseViewModel> GetCourse(int? courseId)
        {
            if (_context != null)
            {
                return await (from c in _context.Courses
                              from f in _context.Fields
                              from s in _context.Streams
                              where c.CourseId == courseId

                              select new CourseViewModel
                              {
                                  CourseId = c.CourseId,
                                  Description = c.Description,
                                  CourseName = c.CourseName,
                                  Image = c.Image,
                                  FieldId = f.FieldId,
                                  FieldName = f.FieldName,
                                  StreamId = s.StreamId,
                                  StreamName = s.StreamName
                              }).SingleOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<CourseViewModel>> GetCourses()
        {
            if (_context != null)
            {
                return await (from c in _context.Courses
                              from f in _context.Fields
                              from s in _context.Streams
                              where c.StreamId == s.StreamId &&  c.FieldId == f.FieldId

                              select new CourseViewModel
                              {
                                 CourseId = c.CourseId,
                                 Description = c.Description,
                                 CourseName = c.CourseName,
                                 Image = c.Image,
                                 FieldId = f.FieldId,
                                 FieldName = f.FieldName,
                                 StreamId = s.StreamId,
                                 StreamName = s.StreamName
                              }).ToListAsync();
            }
            return null;
        }

        public  async Task<List<Field>> GetFields()
        {
            if (_context != null)
            {
                return await _context.Fields.ToListAsync();
            }
            return null;
        }

        public  async Task<List<Stream>> GetStreams()
        {
            if (_context != null)
            {
                return await _context.Streams.ToListAsync();
            }
            return null;
        }

        public async Task UpdateCourse(Course course)
        {
            if (_context != null)
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}
