using ITMCollegeAPI.Models;
using ITMCollegeAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollegeAPI.Repository
{
    public interface ICourseRepository
    {
        Task<List<Field>> GetFields();
        Task<List<Stream>> GetStreams();
        Task<List<CourseViewModel>> GetCourses();
        Task<CourseViewModel> GetCourse(int? courseId);
        Task<int> AddCourse(Course course);
        Task<int> DeleteCourse(int? id);
        Task UpdateCourse(Course course);
    }
}
