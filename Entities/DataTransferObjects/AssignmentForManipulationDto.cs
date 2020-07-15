using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public abstract class AssignmentForManipulationDto
    {
        [Required(ErrorMessage = "Assignment Title is a required field.")]
        [MaxLength(200, ErrorMessage = "Maximum length for the AssignmentTitle is 200 characters.")]
        public string AssignmentTitle { get; set; }
        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(500, ErrorMessage = "Maximum length for the Description is 500 characters.")]
        public string Description { get; set; }
        public Guid CourseId { get; set; }
    }
}
