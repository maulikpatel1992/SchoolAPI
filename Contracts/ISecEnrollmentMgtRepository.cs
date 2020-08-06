using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISecEnrollmentMgtRepository
    {

        Task<PagedList<SecEnrollmentMgt>> GetEnrollmentsAsync(Guid userId, SecEnrollmentParameters secEnrollmentParameters, bool trackChanges);
        Task<SecEnrollmentMgt> GetEnrollmentAsync(Guid userId, Guid id, bool trackChanges);
        void CreateEnrollmentForUser(Guid userId, SecEnrollmentMgt enrollment);
        void DeleteEnrollment(SecEnrollmentMgt enrollment);
    }
}
