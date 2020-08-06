using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet(Name = "GetCourses"), Authorize]
        public async Task<IActionResult> GetCourses([FromQuery] CourseMgtParameters courseMgtParameters)
        {
            try
            {
                var courses =await _repository.CourseMgt.GetAllCoursesAsync(courseMgtParameters, trackChanges: false);

                var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);

                return Ok(coursesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCourses)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "CourseById"), Authorize]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetCourse(Guid id)
        {
            var course =await _repository.CourseMgt.GetCourseAsync(id, trackChanges: false);
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

        [HttpPost(Name = "CreateCourse"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourse([FromBody] CourseForCreationDto course)
        {
            if (course == null)
            {
                _logger.LogError("CourseForCreationDto object sent from client is null.");
                return BadRequest("CourseForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CourseForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var courseEntity = _mapper.Map<CourseMgt>(course);

            _repository.CourseMgt.CreateCourse(courseEntity);
            await _repository.SaveAsync();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);

            return CreatedAtRoute("CourseById", new { id = courseToReturn.Id }, courseToReturn);
        }

        [HttpGet("collection/({ids})", Name = "CourseCollection"), Authorize]
        public async Task<IActionResult> GetCourseCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var courseEntities =await _repository.CourseMgt.GetByIdsAsync(ids, trackChanges: false);

            if (ids.Count() != courseEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var coursesToReturn = _mapper.Map<IEnumerable<CourseDto>>(courseEntities);
            return Ok(coursesToReturn);
        }

        [HttpPost("collection"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourseCollection([FromBody] IEnumerable<CourseForCreationDto> courseCollection)
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

           await _repository.SaveAsync();

            var courseCollectionToReturn = _mapper.Map<IEnumerable<CourseDto>>(courseEntities);
            var ids = string.Join(",", courseCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("CourseCollection", new { ids }, courseCollectionToReturn);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _repository.CourseMgt.GetCourseAsync(id, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.CourseMgt.DeleteCourse(course);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] CourseForUpdateDto course)
        {
            if (course == null)
            {
                _logger.LogError("CourseForUpdateDto object sent from client is null.");
                return BadRequest("CourseForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CourseForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var courseEntity = await _repository.CourseMgt.GetCourseAsync(id, trackChanges: true);
            if (courseEntity == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(course, courseEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}"), Authorize]
        public async Task<IActionResult> PartiallyUpdateCourse(Guid Id, [FromBody] JsonPatchDocument<CourseForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var course = await _repository.CourseMgt.GetCourseAsync(Id, trackChanges: true);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {Id} doesn't exist in the database.");
                return NotFound();
            }



            var CourseToPatch = _mapper.Map<CourseForUpdateDto>(course);

            patchDoc.ApplyTo(CourseToPatch, ModelState);

            TryValidateModel(CourseToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
        

            _mapper.Map(CourseToPatch, course);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
