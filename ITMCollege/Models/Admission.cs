using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Models
{
    public class Admission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AdmissionId { get; set; }
        public string RegNum { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string MotherName { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string ResAddress { get; set; }
        [Required]
        public string PerAddress { get; set; }
        public int StreamId { get; set; }
        public int FieldId { get; set; }
        [Required]
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
        public byte Status { get; set; }
    }
}
