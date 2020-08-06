
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICourseSectionMgtRepository 
    {
        Task<PagedList<CourseSectionMgt>> GetCoursesectionsAsync(Guid courseId, CourseSectionMgtParameters courseSectionMgtParameters, bool trackChanges);
        Task<CourseSectionMgt> GetCoursesectionAsync(Guid courseId, Guid id, bool trackChanges);
        void CreateCourseSectionForCourse(Guid courseId, CourseSectionMgt coursesection);
        void DeleteCourseSection(CourseSectionMgt coursesection);
    }
}
