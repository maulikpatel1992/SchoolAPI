using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ISecAssignmentMgtRepository
    {
        IEnumerable<SecAssignmentMgt> GetAssignmentSections(Guid assignmentId, bool trackChanges);
        SecAssignmentMgt GetAssignmentSection(Guid assignmentId, Guid id, bool trackChanges);
        void CreateAssignmentSectionForAssignment(Guid assignmentId, SecAssignmentMgt assignmentSection);
        void DeleteAssignmentSection(SecAssignmentMgt assignmentSection);
    }
}
