using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class CourseMgt
    {
        [Column("CourseId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "CourseTitle is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the CourseTitle is 60 characters.")]
        public string CourseTitle { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(200, ErrorMessage = "Maximum length for the Description is 200 characters.")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ICollection<SecEnrollmentMgt> SecEnrollmentMgt { get; set; }
        public ICollection<CourseSectionMgt> CourseSectionMgt { get; set; }
        public ICollection<Assignment> Assignment { get; set; }
       

    }
}
