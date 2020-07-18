using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public abstract class EnrollmentForManipulationDto
    {
        [Required(ErrorMessage = "StartDate is a required field.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is a required field.")]

        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        public Guid CourseId { get; set; }
    }
}
