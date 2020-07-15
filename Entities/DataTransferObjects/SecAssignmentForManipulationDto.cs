using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public abstract class SecAssignmentForManipulationDto
    {
        [Required(ErrorMessage = "SubmissionText is a required field.")]
        [MaxLength(200, ErrorMessage = "Maximum length for the SubmissionText is 1000 characters.")]
        public string SubmissionText { get; set; }

        [Range(0, 100, ErrorMessage = "Score is required and it must be between 0 to 100")]
        public int Score { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }


        public Guid AssignmentId { get; set; }
    }
}
