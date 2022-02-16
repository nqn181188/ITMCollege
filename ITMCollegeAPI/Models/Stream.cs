using System;
using System.Collections.Generic;

#nullable disable

namespace ITMCollegeAPI.Models
{
    public partial class Stream
    {
        public Stream()
        {
            Admissions = new HashSet<Admission>();
            Courses = new HashSet<Course>();
            Fields = new HashSet<Field>();
        }
        public int StreamId { get; set; }
        public string StreamName { get; set; }

        public virtual ICollection<Admission> Admissions { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
    }
}
