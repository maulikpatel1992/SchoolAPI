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
    [Route("api/courses/{courseId}/assignments")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AssignmentController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAssignmentsForCourse(Guid courseId)
        {
            var course = _repository.CourseMgt.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsFromDb = _repository.Assignment.GetAssignments(courseId, trackChanges: false);

            var assignmentsDto = _mapper.Map<IEnumerable<AssignmentDto>>(assignmentsFromDb);

            return Ok(assignmentsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetAssignmentForCourse(Guid courseId, Guid id)
        {
            var course = _repository.CourseMgt.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentDb = _repository.Assignment.GetAssignment(courseId, id, trackChanges: false);
            if (assignmentDb == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var assignment = _mapper.Map<AssignmentDto>(assignmentDb);

            return Ok(assignment);
        }
    }
}
