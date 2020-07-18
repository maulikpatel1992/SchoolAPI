using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ISecEnrollmentMgtRepository
    {

        IEnumerable<SecEnrollmentMgt> GetEnrollments(Guid userId, bool trackChanges);
        SecEnrollmentMgt GetEnrollment(Guid userId, Guid id, bool trackChanges);
        void CreateEnrollmentForUser(Guid userId, SecEnrollmentMgt enrollment);
        void DeleteEnrollment(SecEnrollmentMgt enrollment);
    }
}
