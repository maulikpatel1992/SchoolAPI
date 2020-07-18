using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICourseMgtRepository
    {
        IEnumerable<CourseMgt> GetAllCourses(bool trackChanges);
        CourseMgt GetCourse(Guid courseId, bool trackChanges);
        void CreateCourse(CourseMgt course);
        IEnumerable<CourseMgt> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCourse(CourseMgt course);
    }
}
