using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class AssignmentRepository : RepositoryBase<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Assignment> GetAssignments(Guid courseId, bool trackChanges) =>
             FindByCondition(e => e.CourseId.Equals(courseId), trackChanges)
             .OrderBy(e => e.Id);

        public Assignment GetAssignment(Guid courseId, Guid id, bool trackChanges) =>
           FindByCondition(e => e.CourseId.Equals(courseId) && e.Id.Equals(id), trackChanges)
           .SingleOrDefault();

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

        public IEnumerable<Assignment> GetAllAssignmentsForSec(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Id)
            .ToList();
        public Assignment GetAssignmentForSec(Guid assignmentId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(assignmentId), trackChanges)
            .SingleOrDefault();

        public void CreateAssignmentForSec(Assignment assignment) => Create(assignment);




    }
}
