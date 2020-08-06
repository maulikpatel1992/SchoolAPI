using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAssignmentsAsync(Guid courseId, bool trackChanges);
        Task<Assignment> GetAssignmentAsync(Guid courseId, Guid id, bool trackChanges);
        void CreateAssignmentForCourse(Guid courseId, Assignment assignment);
        void DeleteAssignment(Assignment assignment);

        // Interfaces for SectionAssignmentMgt 
        Task<IEnumerable<Assignment>> GetAllAssignmentsForSecAsync(bool trackChanges);
        Task<Assignment> GetAssignmentForSecAsync(Guid assignmentId, bool trackChanges);
        void CreateAssignmentForSec(Assignment assignment);

    }
}
