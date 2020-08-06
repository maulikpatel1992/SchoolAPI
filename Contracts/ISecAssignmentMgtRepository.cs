using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISecAssignmentMgtRepository
    {
        Task<IEnumerable<SecAssignmentMgt>> GetAssignmentSectionsAsync(Guid assignmentId, bool trackChanges);
        Task<SecAssignmentMgt> GetAssignmentSectionAsync(Guid assignmentId, Guid id, bool trackChanges);
        void CreateAssignmentSectionForAssignment(Guid assignmentId, SecAssignmentMgt assignmentSection);
        void DeleteAssignmentSection(SecAssignmentMgt assignmentSection);
    }
}
