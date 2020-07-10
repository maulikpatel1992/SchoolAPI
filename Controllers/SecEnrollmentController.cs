using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolAPI.Controllers
{
    [Route("api/users/{UserId}/enrollments")]
    [ApiController]
    public class SecEnrollmentController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public SecEnrollmentController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEnrollmentsForUser(Guid userId)
        {
            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentsFromDb = _repository.SecEnrollmentMgt.GetEnrollments(userId, trackChanges: false);

            var enrollmentsDto = _mapper.Map<IEnumerable<EnrollmentDto>>(enrollmentsFromDb);

            return Ok(enrollmentsDto);
        }
       [HttpGet("{id}")]
        public IActionResult GetEnrollmentForUser(Guid userId, Guid id)
        {
            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentDb = _repository.SecEnrollmentMgt.GetEnrollment(userId, id, trackChanges: false);
            if (enrollmentDb == null)
            {
                _logger.LogInfo($"Enrollment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var enrollment = _mapper.Map<EnrollmentDto>(enrollmentDb);

            return Ok(enrollment);
        }
    }
}
