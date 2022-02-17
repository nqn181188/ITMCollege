using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollegeAPI.Models
{
    public class Admissions
    {
        public long AdmissionId { get; set; }
        public string RegNum { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ResAddress { get; set; }
        public string PerAddress { get; set; }
        public int StreamId { get; set; }
        public int FieldId { get; set; }
        public string Email { get; set; }
        public string Sport { get; set; }
        public string ExUniversity { get; set; }
        public string ExEnrollNum { get; set; }
        public string ExCenter { get; set; }
        public string ExStream { get; set; }
        public string ExField { get; set; }
        public decimal? ExMarks { get; set; }
        public string ExOutOfDate { get; set; }
        public string ExClass { get; set; }
        public virtual Field Field { get; set; }
        public virtual Stream Stream { get; set; }
        public byte Status { get; set; }
    }
}
