using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Models
{
    public class Facility
    {
        public int FacilityId { get; set; }
        [Required]
        public string FacilityName { get; set; }
        [Required]
        public bool IsActive { get; set; }
     
        public string Image { get; set; }
    }
}
