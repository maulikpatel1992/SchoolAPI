using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
   public  class SecAssignmentMgt
    {
        [Column("SectionAId")]
        public Guid Id { get; set; }

        public string SubmissionText { get; set; }

        public int Score { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey(nameof(Assignment))]
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
