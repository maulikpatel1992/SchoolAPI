using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.DataTransferObjects
{
    public class UserForCreationDto : UserForManipulationDto
    {
        

        public IEnumerable<EnrollmentForCreationDto> Enrollments { get; set; }
    }
}
