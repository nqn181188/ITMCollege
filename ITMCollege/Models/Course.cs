using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string CourseName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Stream")]
        public int StreamId { get; set; }
        [Required]
        [Display(Name = "Field")]
        public int FieldId { get; set; }
        public string Image { get; set; }

        //[NotMapped]
        //public IFormFile ImageFile { get; set; }
        public virtual Field Field { get; set; }
        public virtual Stream Stream { get; set; }
    }
}
