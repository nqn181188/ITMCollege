using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int StreamId { get; set; }
        public int FieldId { get; set; }
        public string Image { get; set; }

        //[NotMapped]
        //public IFormFile ImageFile { get; set; }
        public virtual Field Field { get; set; }
        public virtual Stream Stream { get; set; }
    }
}
