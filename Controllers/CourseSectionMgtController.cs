using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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

        [HttpGet("{id}", Name="GetCoursesectionForCourse")]
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

        [HttpPost]
        public IActionResult CreateCourseSectionForCourse(Guid courseId, [FromBody] CourseSectionForCreationDto coursesection)
        {
            if (coursesection == null)
            {
                _logger.LogError("CourseSectionForCreationDto object sent from client is null.");
                return BadRequest("CourseSectionForCreationDto object is null");
            }

            var course = _repository.CourseMgt.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionEntity = _mapper.Map<CourseSectionMgt>(coursesection);

            _repository.CourseSectionMgt.CreateCourseSectionForCourse(courseId, coursesectionEntity);
            _repository.Save();

            var coursesectionToReturn = _mapper.Map<EnrollmentDto>(coursesectionEntity);

            return CreatedAtRoute("GetCoursesectionForCourse", new { courseId, id = coursesectionToReturn.Id }, coursesectionToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourseSectionForCourse(Guid courseId, Guid id)
        {
            var course = _repository.CourseMgt.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionForCourse = _repository.CourseSectionMgt.GetCoursesection(courseId, id, trackChanges: false);
            if (coursesectionForCourse == null)
            {
                _logger.LogInfo($"CourseSection with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.CourseSectionMgt.DeleteCourseSection(coursesectionForCourse);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourseSectionForCourse(Guid courseId, Guid id, [FromBody] CourseSectionForUpdateDto coursesection)
        {
            if (coursesection == null)
            {
                _logger.LogError("CourseSectionForUpdateDto object sent from client is null.");
                return BadRequest("CourseSectionForUpdateDto object is null");
            }

            var course = _repository.CourseMgt.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionEntity = _repository.CourseSectionMgt.GetCoursesection(courseId, id, trackChanges: true);
            if (coursesectionEntity == null)
            {
                _logger.LogInfo($"Enrollment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(coursesection, coursesectionEntity);
            _repository.Save();

            return NoContent();
        }
    }
}
