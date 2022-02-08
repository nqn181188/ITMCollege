using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Models
{
    public class Department
    {
        public Department()
        {
            Faculties = new HashSet<Faculty>();
        }

        public int DepId { get; set; }
        [Required]
        public string DepName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
