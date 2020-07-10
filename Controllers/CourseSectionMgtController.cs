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
    [Route("api/courses/{courseId}/coursesections")]
    [ApiController]
    public class CourseSectionMgtController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CourseSectionMgtController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCoursesectionsForCourse(Guid courseId)
        {
            var course = _repository.CourseMgt.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionsFromDb = _repository.CourseSectionMgt.GetCoursesections(courseId, trackChanges: false);

            var coursesectionsDto = _mapper.Map<IEnumerable<CourseSectionDto>>(coursesectionsFromDb);

            return Ok(coursesectionsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetCoursesectionForCourse(Guid courseId, Guid id)
        {
            var course = _repository.CourseMgt.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionDb = _repository.CourseSectionMgt.GetCoursesection(courseId, id, trackChanges: false);
            if (coursesectionDb == null)
            {
                _logger.LogInfo($"Coursesection with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var coursesection = _mapper.Map<CourseSectionDto>(coursesectionDb);

            return Ok(coursesection);
        }
    }
}
