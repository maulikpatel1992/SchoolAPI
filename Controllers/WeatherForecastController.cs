using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public WeatherForecastController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            // _repository.User.AnyMethodFromUserRepository();
            // _repository.SecEnrollmentMgt.AnyMethodFromSecEnrollmentMgtRepository();
            //_repository.Assignment.AnyMethodFromAssignmentRepository();
            //_repository.CourseMgt.AnyMethodFromCourseMgtRepository();
            //_repository.CourseSectionMgt.AnyMethodFromCourseSectionMgtRepository();
            //_repository.SectionAssignmentMgt.AnyMethodFromSectionAssignmentMgtRepository();
            //_repository.SectionEnrollmentMgt.AnyMethodFromSectionEnrollmentMgtRepository();

            return new string[] { "value1", "value2" };
        }
    }
}
