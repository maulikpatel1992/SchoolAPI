
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICourseSectionMgtRepository 
    {
        IEnumerable<CourseSectionMgt> GetCoursesections(Guid courseId, bool trackChanges);
        CourseSectionMgt GetCoursesection(Guid courseId, Guid id, bool trackChanges);
        void CreateCourseSectionForCourse(Guid courseId, CourseSectionMgt coursesection);
        void DeleteCourseSection(CourseSectionMgt coursesection);
    }
}
