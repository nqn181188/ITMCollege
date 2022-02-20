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
        [Required]
        public string Image { get; set; }
        [Required]
        public int? SpeSubjectId { get; set; }
        public int? OpSubjectId { get; set; }
        [Required]
        public string EmergencyName { get; set; }
        [Required]
        public string EmergencyAddress { get; set; }
        [Required]
        public string EmergencyPhone { get; set; }
    }
}
