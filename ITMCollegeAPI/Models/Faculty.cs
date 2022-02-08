using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITMCollegeAPI.Models
{
    public partial class Faculty
    {
        public int FacultyId { get; set; }
        public string FalcultyName { get; set; }
        public DateTime Dob { get; set; }
        public string Degree { get; set; }
        public int DepId { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual Department Dep { get; set; }
    }
}
