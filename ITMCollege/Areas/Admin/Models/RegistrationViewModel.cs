using ITMCollege.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Areas.Admin.Models
{
    public class RegistrationViewModel
    {
        public long RegistrationId { get; set; }
        public string RegNum { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ResAddress { get; set; }
        public string PerAddress { get; set; }
        public Stream Stream { get; set; }
        public Field Field { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public SpeSubject SpeSubject { get; set; }
        public OpSubject OpSubject { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyAddress { get; set; }
        public string EmergencyPhone { get; set; }
    }
}
