using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Assignment
    {
        [Column("AssignmentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "AssignmentTitle is a required field.")]
        [MaxLength(200, ErrorMessage = "Maximum length for the AssignmentTitle is 200 characters.")]
        public string AssignmentTitle { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(500, ErrorMessage = "Maximum length for the Description is 500 characters.")]
        public string Description { get; set; }

        [ForeignKey(nameof(CourseMgt))]
        public Guid CourseId { get; set; }
        public CourseMgt CourseMgt { get; set; }
        public ICollection<SecAssignmentMgt> SecAssignmentMgt { get; set; }
    }
}
