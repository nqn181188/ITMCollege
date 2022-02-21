using ITMCollege.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Admin.Models
{
    public class SpeSubjectViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }
        [Required]
        public string SubjectName { get; set; }
        public int FieldId { get; set; }
        public virtual Stream Stream { get; set; }
        public virtual Field Field { get; set; }
    }
}
