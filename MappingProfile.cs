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
            CreateMap<SecAssignmentMgt, SecAssignmentDto>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<EnrollmentForCreationDto, SecEnrollmentMgt>();
            CreateMap<CourseForCreationDto, CourseMgt>();
            CreateMap<EnrollmentForUpdateDto, SecEnrollmentMgt>().ReverseMap();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<CourseSectionForUpdateDto, CourseSectionMgt>().ReverseMap(); 
            CreateMap<AssignmentForUpdateDto, Assignment>().ReverseMap();
            CreateMap<SecAssignmentForUpdateDto, SecAssignmentMgt>().ReverseMap();
            CreateMap<CourseForUpdateDto, CourseMgt>();
        }
    }
}
