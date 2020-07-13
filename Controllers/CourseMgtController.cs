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
using SchoolAPI.ModelBinders;

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

        [HttpGet("{id}", Name = "CourseById")]
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

        [HttpPost]
        public IActionResult CreateCourse([FromBody] CourseForCreationDto course)
        {
            if (course == null)
            {
                _logger.LogError("CourseForCreationDto object sent from client is null.");
                return BadRequest("CourseForCreationDto object is null");
            }

            var courseEntity = _mapper.Map<CourseMgt>(course);

            _repository.CourseMgt.CreateCourse(courseEntity);
            _repository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);

            return CreatedAtRoute("CourseById", new { id = courseToReturn.Id }, courseToReturn);
        }

        [HttpGet("collection/({ids})", Name = "CourseCollection")]
        public IActionResult GetCourseCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var courseEntities = _repository.CourseMgt.GetByIds(ids, trackChanges: false);

            if (ids.Count() != courseEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var coursesToReturn = _mapper.Map<IEnumerable<CourseDto>>(courseEntities);
            return Ok(coursesToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateCourseCollection([FromBody] IEnumerable<CourseForCreationDto> courseCollection)
        {
            if (courseCollection == null)
            {
                _logger.LogError("Course collection sent from client is null.");
                return BadRequest("Course collection is null");
            }

            var courseEntities = _mapper.Map<IEnumerable<CourseMgt>>(courseCollection);
            foreach (var course in courseEntities)
            {
                _repository.CourseMgt.CreateCourse(course);
            }

            _repository.Save();

            var courseCollectionToReturn = _mapper.Map<IEnumerable<CourseDto>>(courseEntities);
            var ids = string.Join(",", courseCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("CourseCollection", new { ids }, courseCollectionToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(Guid id)
        {
            var course = _repository.CourseMgt.GetCourse(id, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.CourseMgt.DeleteCourse(course);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourse(Guid id, [FromBody] CourseForUpdateDto course)
        {
            if (course == null)
            {
                _logger.LogError("CourseForUpdateDto object sent from client is null.");
                return BadRequest("CourseForUpdateDto object is null");
            }

            var courseEntity = _repository.CourseMgt.GetCourse(id, trackChanges: true);
            if (courseEntity == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(course, courseEntity);
            _repository.Save();

            return NoContent();
        }
    }
}
