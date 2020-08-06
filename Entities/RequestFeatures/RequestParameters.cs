using System;
using System.Collections.Generic;
using System.Text;


namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50; 
        public int PageNumber { get; set; } = 1; 
        private int _pageSize = 10; 
        public int PageSize 
        { 
            get 
            { 
                return _pageSize; 
            } 
            set 
            { 
                _pageSize = (value > maxPageSize) ? maxPageSize : value; 
            } 
        }

        public string OrderBy { get; set; }
    }

    public class SecEnrollmentParameters : RequestParameters 
    {
        public DateTime startDate { get; set; } = new DateTime(1200, 01, 01);
        public DateTime endDate { get; set; } = new DateTime(9999, 12, 31);
    }

    public class CourseSectionMgtParameters : RequestParameters
    {
    }

    public class CourseMgtParameters : RequestParameters
    {
        public CourseMgtParameters() 
        { 
            OrderBy = "courseTitle"; 
        }
        public string SearchTerm { get; set; }
    }

    
}
