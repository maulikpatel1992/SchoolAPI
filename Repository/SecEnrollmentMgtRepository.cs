using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class SecEnrollmentMgtRepository : RepositoryBase<Employee>, ISecEnrollmentMgtRepository
    {
        public SecEnrollmentMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
