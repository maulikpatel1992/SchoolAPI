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
    public class SecAssignmentMgtRepository : RepositoryBase<SecAssignmentMgt>, ISecAssignmentMgtRepository
    {
        public SecAssignmentMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

            public async Task<IEnumerable<SecAssignmentMgt>> GetAssignmentSectionsAsync(Guid assignmentId, bool trackChanges) => 
                await FindByCondition(e => e.AssignmentId.Equals(assignmentId), trackChanges)
                .OrderBy(e => e.Id)
                .ToListAsync();

        public async Task<SecAssignmentMgt> GetAssignmentSectionAsync(Guid assignmentId, Guid id, bool trackChanges) =>
           await FindByCondition(e => e.AssignmentId.Equals(assignmentId) && e.Id.Equals(id), trackChanges)
           .SingleOrDefaultAsync();

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
