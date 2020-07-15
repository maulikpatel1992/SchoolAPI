using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public abstract class CourseForManipulationDto
    {
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        [Required(ErrorMessage = "Course Title is a required field.")]
        public string CourseTitle { get; set; }

        [Required(ErrorMessage = "Description  is a required field.")]
        public string Description { get; set; }
    }
}
