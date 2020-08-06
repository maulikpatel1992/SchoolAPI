﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class CourseForCreationDto : CourseForManipulationDto
    {
        
        public IEnumerable<CourseSectionForCreationDto> CourseSections { get; set; }
        public IEnumerable<AssignmentForCreationDto> Assignments { get; set; }
    }
}

