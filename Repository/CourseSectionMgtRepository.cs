using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class CourseSectionMgtRepository : RepositoryBase<CourseSectionMgt>, ICourseSectionMgtRepository
    {
        public CourseSectionMgtRepository (RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<CourseSectionMgt> GetCoursesections(Guid courseId, bool trackChanges) => 
            FindByCondition(e => e.CourseId.Equals(courseId), trackChanges)
            .OrderBy(e => e.Id);

        public CourseSectionMgt GetCoursesection(Guid courseId, Guid id, bool trackChanges) =>
           FindByCondition(e => e.CourseId.Equals(courseId) && e.Id.Equals(id), trackChanges)
           .SingleOrDefault();
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
