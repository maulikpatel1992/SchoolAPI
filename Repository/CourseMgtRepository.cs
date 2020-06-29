using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CourseMgtRepository : RepositoryBase<CourseMgtRepository>, ICourseMgtRepository
    {
        public CourseMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
