using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAssignmentRepository
    {
        IEnumerable<Assignment> GetAssignments(Guid courseId, bool trackChanges);
        Assignment GetAssignment(Guid courseId, Guid id, bool trackChanges);
        void CreateAssignmentForCourse(Guid courseId, Assignment assignment);
        void DeleteAssignment(Assignment assignment);

        // Interfaces for SectionAssignmentMgt 
        IEnumerable<Assignment> GetAllAssignmentsForSec(bool trackChanges);
        Assignment GetAssignmentForSec(Guid assignmentId, bool trackChanges);
        void CreateAssignmentForSec(Assignment assignment);

    }
}
