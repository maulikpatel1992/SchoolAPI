using Entities.Models;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class RepositoryEmployeeExtensions
    { 
        public static IQueryable<SecEnrollmentMgt> FilterSecEnrollments(this IQueryable<SecEnrollmentMgt> secEnrollmentMgts, 
            DateTime startDate, DateTime endDate) => secEnrollmentMgts.Where(e => (e.StartDate >= startDate && e.EndDate <= endDate)); 
        public static IQueryable<CourseMgt> Search(this IQueryable<CourseMgt> courseMgt, string searchTerm) 
        { 
            if (string.IsNullOrWhiteSpace(searchTerm)) 
                return courseMgt; 
            var lowerCaseTerm = searchTerm.Trim().ToLower(); 
            return courseMgt.Where(e => e.CourseTitle.ToLower().Contains(lowerCaseTerm)); 
        }

        public static IQueryable<CourseMgt> Sort(this IQueryable<CourseMgt> courseMgt, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return courseMgt.OrderBy(e => e.CourseTitle);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<CourseMgt>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return courseMgt.OrderBy(e => e.CourseTitle);

            return courseMgt.OrderBy(orderQuery);
        }
    }
}
