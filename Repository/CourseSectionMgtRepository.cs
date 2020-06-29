using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CourseSectionMgtRepository : RepositoryBase<CourseMgtRepository>, ICourseSectionMgtRepository
    {
        public CourseSectionMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
