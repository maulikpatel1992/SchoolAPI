using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class CourseSectionMgt
    {
        [Column("SecCourseId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "StartDate is a required field.")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "EndDate is a required field.")]
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }


        [ForeignKey(nameof(CourseMgt))]
        public Guid CourseId { get; set; }
        public CourseMgt CourseMgt { get; set; }

    }
}
