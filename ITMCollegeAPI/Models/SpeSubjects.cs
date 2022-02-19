using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollegeAPI.Models
{
    public class SpeSubjects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int FieldId { get; set; }
        public virtual Stream Stream { get; set; }
        public virtual Field Field { get; set; }
    }
}
