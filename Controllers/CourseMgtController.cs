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
    [Route("api/courses")]
    [ApiController]
    public class CourseMgtController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CourseMgtController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            try
            {
                var courses = _repository.CourseMgt.GetAllCourses(trackChanges: false);

                var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);

                return Ok(coursesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCourses)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCourse(Guid id)
        {
            var course = _repository.CourseMgt.GetCourse(id, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var courseDto = _mapper.Map<CourseDto>(course);
                return Ok(courseDto);
            }
        }
    }
}
