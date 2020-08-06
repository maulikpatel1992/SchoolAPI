using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public abstract class UserForManipulationDto
    {
        [Required(ErrorMessage = "Email Id is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MaxLength(10, ErrorMessage = "Maximum length for the Address is 10 characters.")]
        public string Password { get; set; }

        public string Status { get; set; }

        public string SystemRoleId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
