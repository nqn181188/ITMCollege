using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITMCollege.Models
{
    public partial class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RegistrationId { get; set; }
        public string RegNum { get; set; }
        public string Image { get; set; }
        public int? SpeSubjectId { get; set; }
        public int? OpSubjectId { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyAddress { get; set; }
        public string EmergencyPhone { get; set; }
    }
}
