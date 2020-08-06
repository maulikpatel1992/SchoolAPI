using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class CourseForUpdateDto : CourseForManipulationDto
    {
        
        public IEnumerable<CourseSectionForUpdateDto> CourseSections { get; set; }
        public IEnumerable<AssignmentForUpdateDto> Assignments { get; set; }
    }
}
