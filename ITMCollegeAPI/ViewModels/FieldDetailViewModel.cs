using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollegeAPI.ViewModels
{
    public class FieldDetailViewModel
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public int StreamId { get; set; }
        public string StreamName { get; set; }
    }
}
