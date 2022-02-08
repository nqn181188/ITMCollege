using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollegeAPI.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int StreamId { get; set; }
        public int FieldId { get; set; }
        public string Image { get; set; }
        public string FieldName { get; set; }
        public string StreamName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
