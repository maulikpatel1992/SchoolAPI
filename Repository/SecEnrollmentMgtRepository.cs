using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class SecEnrollmentMgtRepository : RepositoryBase<SecEnrollmentMgt>, ISecEnrollmentMgtRepository
    {
        public SecEnrollmentMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<SecEnrollmentMgt>> GetEnrollmentsAsync(Guid userId, SecEnrollmentParameters secEnrollmentParameters, bool trackChanges)
        {
            var secEnrollments = await FindByCondition(e => e.UserId.Equals(userId), trackChanges)
             .FilterSecEnrollments(secEnrollmentParameters.startDate, secEnrollmentParameters.endDate)
             .OrderBy(e => e.Id)
             .ToListAsync();

            return PagedList<SecEnrollmentMgt>
                .ToPagedList(secEnrollments, secEnrollmentParameters.PageNumber, 
                secEnrollmentParameters.PageSize);
        }

       public async Task<SecEnrollmentMgt> GetEnrollmentAsync(Guid userId, Guid id, bool trackChanges) =>
           await FindByCondition(e => e.UserId.Equals(userId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

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
