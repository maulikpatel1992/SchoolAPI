using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class SecEnrollmentMgt
    {
        [Column("SecEnrolId")]
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(CourseSectionMgt))]
        public Guid SecCourseId { get; set; }
        public CourseSectionMgt CourseSectionMgt { get; set; }

    }
}
