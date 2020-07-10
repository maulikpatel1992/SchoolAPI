using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace SchoolAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<SecEnrollmentMgt, EnrollmentDto>();
            CreateMap<CourseMgt, CourseDto>();
            CreateMap<User, UserDto>();
            CreateMap<CourseSectionMgt, CourseSectionDto>();
            CreateMap<Assignment, AssignmentDto>();
        }
    }
}
