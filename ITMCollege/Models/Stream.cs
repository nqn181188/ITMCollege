using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Models
{
    public class Stream
    {
        public int StreamId { get; set; }
        [Required]
        [Display(Name = "Stream")]
        public string StreamName { get; set; }

        
    }
}
