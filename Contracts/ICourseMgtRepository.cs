using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICourseMgtRepository
    {
        Task<IEnumerable<CourseMgt>> GetAllCoursesAsync(CourseMgtParameters courseMgtParameters, bool trackChanges);
        Task<CourseMgt> GetCourseAsync(Guid courseId, bool trackChanges);
        void CreateCourse(CourseMgt course);
        Task<IEnumerable<CourseMgt>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

        void DeleteCourse(CourseMgt course);
    }
}
