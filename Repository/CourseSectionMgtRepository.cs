using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CourseSectionMgtRepository : RepositoryBase<CourseSectionMgt>, ICourseSectionMgtRepository
    {
        public CourseSectionMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<CourseSectionMgt>> GetCoursesectionsAsync(Guid courseId, CourseSectionMgtParameters courseSectionMgtParameters, bool trackChanges)
        { 
            var courseSectionMgts = await FindByCondition(e => e.CourseId.Equals(courseId), trackChanges)
            .OrderBy(e => e.Id)
            .ToListAsync();

            return PagedList<CourseSectionMgt>.ToPagedList(courseSectionMgts, courseSectionMgtParameters.PageNumber, courseSectionMgtParameters.PageSize);
        }
        public async Task<CourseSectionMgt> GetCoursesectionAsync(Guid courseId, Guid id, bool trackChanges) =>
           await FindByCondition(e => e.CourseId.Equals(courseId) && e.Id.Equals(id), trackChanges)
           .SingleOrDefaultAsync();
        public void CreateCourseSectionForCourse(Guid courseId, CourseSectionMgt coursesection) 
        {
            coursesection.CourseId = courseId; 
            Create(coursesection); 
        }

        public void DeleteCourseSection(CourseSectionMgt coursesection)
        {
            Delete(coursesection);
        }
    }
}
