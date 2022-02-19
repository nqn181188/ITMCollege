using ITMCollege.Areas.Admin.Models;
using ITMCollege.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.ViewModel
{
    public class RegistrationViewModel
    {
        public int RegistrationId { get; set; }
        public string RegNum { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ResAddress { get; set; }
        public string PerAddress { get; set; }
        public Stream Stream { get; set; }
        public Field Field { get; set; }
        public string Email { get; set; }
        public byte Status { get; set; }
        public string Image { get; set; }
        public int? SpeSubjectId { get; set; }
        public int? OpSubjectId { get; set; }   
        public SpeSubject SpeSubject { get; set; }
        public OpSubject OpSubject { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyAddress { get; set; }
        public string EmergencyPhone { get; set; }
    }
}
