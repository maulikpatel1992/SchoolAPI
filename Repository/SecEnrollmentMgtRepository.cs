using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class SecEnrollmentMgtRepository : RepositoryBase<SecEnrollmentMgt>, ISecEnrollmentMgtRepository
    {
        public SecEnrollmentMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<SecEnrollmentMgt> GetEnrollments(Guid userId, bool trackChanges) =>
            FindByCondition(e => e.UserId.Equals(userId), trackChanges)
            .OrderBy(e => e.Id);

       public SecEnrollmentMgt GetEnrollment(Guid userId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.UserId.Equals(userId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void CreateEnrollmentForUser(Guid userId, SecEnrollmentMgt enrollment) 
        {
            enrollment.UserId = userId; 
            Create(enrollment); 
        }

        public void DeleteEnrollment(SecEnrollmentMgt enrollment) 
        { 
            Delete(enrollment); 
        }
    }
}
