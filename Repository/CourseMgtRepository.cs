using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Extensions;

namespace Repository
{

    public class CourseMgtRepository : RepositoryBase<CourseMgt>, ICourseMgtRepository
    {
        public CourseMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<CourseMgt>> GetAllCoursesAsync(CourseMgtParameters courseMgtParameters, bool trackChanges) =>          
            await FindAll(trackChanges)
            .Search(courseMgtParameters.SearchTerm)
            .Sort(courseMgtParameters.OrderBy)
            //.OrderBy(c => c.Id)          
            .ToListAsync();

        public async Task<CourseMgt> GetCourseAsync(Guid courseId, bool trackChanges) => 
            await FindByCondition(c => c.Id.Equals(courseId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateCourse(CourseMgt course) => Create(course);
        public async Task<IEnumerable<CourseMgt>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => 
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();
        public void DeleteCourse(CourseMgt course) 
        { 
            Delete(course); 
        }
    }
}
