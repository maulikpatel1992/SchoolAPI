using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{

    public class CourseMgtRepository : RepositoryBase<CourseMgt>, ICourseMgtRepository
    {
        public CourseMgtRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<CourseMgt> GetAllCourses(bool trackChanges) =>          
            FindAll(trackChanges)          
            .OrderBy(c => c.Id)          
            .ToList();

        public CourseMgt GetCourse(Guid courseId, bool trackChanges) => 
            FindByCondition(c => c.Id.Equals(courseId), trackChanges)
            .SingleOrDefault();

        public void CreateCourse(CourseMgt course) => Create(course);
        public IEnumerable<CourseMgt> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => 
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();
        public void DeleteCourse(CourseMgt course) 
        { 
            Delete(course); 
        }
    }
}
