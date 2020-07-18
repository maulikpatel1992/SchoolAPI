using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class SecAssignmentMgtRepository : RepositoryBase<SecAssignmentMgt>, ISecAssignmentMgtRepository
    {
        public SecAssignmentMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

            public IEnumerable<SecAssignmentMgt> GetAssignmentSections(Guid assignmentId, bool trackChanges) => 
                FindByCondition(e => e.AssignmentId.Equals(assignmentId), trackChanges)
                .OrderBy(e => e.Id);

        public SecAssignmentMgt GetAssignmentSection(Guid assignmentId, Guid id, bool trackChanges) =>
           FindByCondition(e => e.AssignmentId.Equals(assignmentId) && e.Id.Equals(id), trackChanges)
           .SingleOrDefault();

        public void CreateAssignmentSectionForAssignment(Guid assignmentId, SecAssignmentMgt assignmentSection)
        {
            assignmentSection.AssignmentId = assignmentId;
            Create(assignmentSection);
        }

        public void DeleteAssignmentSection(SecAssignmentMgt assignmentSection)
        {
            Delete(assignmentSection);
        }
    }
}
