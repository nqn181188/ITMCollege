﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        [Display(Name ="Name")]
        public string FalcultyName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        public string Degree { get; set; }
        public int DepId { get; set; }
        public string Image { get; set; }

        //[NotMapped]
        //public IFormFile ImageFile { get; set; }
        public virtual Department Dep { get; set; }
    }
}
