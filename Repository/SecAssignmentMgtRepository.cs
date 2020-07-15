using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class SecAssignmentMgtRepository : RepositoryBase<SecAssignmentMgtRepository>, ISecAssignmentMgtRepository
    {
        public SecAssignmentMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
