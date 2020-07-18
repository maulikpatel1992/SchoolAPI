using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
   public class CourseDto
    {
        public Guid Id { get; set; }
       
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        public string CourseTitle { get; set; }
        public string Description { get; set; }

    }
}
