using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class AssignmentDto
    {
        public Guid Id { get; set; }
        public string AssignmentTitle { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
    }
}
