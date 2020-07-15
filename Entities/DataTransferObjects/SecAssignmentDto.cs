using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class SecAssignmentDto
    {
       
        public string SubmissionText { get; set; }

        
        public int Score { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

       
        public Guid AssignmentId { get; set; }
       
    }
}
