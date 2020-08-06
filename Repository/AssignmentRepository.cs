using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class AssignmentRepository : RepositoryBase<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsAsync(Guid courseId, bool trackChanges) =>
             await FindByCondition(e => e.CourseId.Equals(courseId), trackChanges)
             .OrderBy(e => e.Id)
            .ToListAsync();

        public async Task<Assignment> GetAssignmentAsync(Guid courseId, Guid id, bool trackChanges) =>
           await FindByCondition(e => e.CourseId.Equals(courseId) && e.Id.Equals(id), trackChanges)
           .SingleOrDefaultAsync();

        public void CreateAssignmentForCourse(Guid courseId, Assignment assignment)
        {
            assignment.CourseId = courseId;
            Create(assignment);
        }
        public void DeleteAssignment(Assignment assignment)
        {
            Delete(assignment);
        }

        // Interfaces for SectionAssignmentMgt 

        public async Task<IEnumerable<Assignment>> GetAllAssignmentsForSecAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Id)
            .ToListAsync();
        public async Task<Assignment> GetAssignmentForSecAsync(Guid assignmentId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(assignmentId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateAssignmentForSec(Assignment assignment) => Create(assignment);




    }
}
