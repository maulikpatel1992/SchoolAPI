﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserForUpdateDto : UserForManipulationDto
    {
        

        public IEnumerable<EnrollmentForCreationDto> Enrollments { get; set; }
    }
}
