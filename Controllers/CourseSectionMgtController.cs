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
using Newtonsoft.Json;

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

        [HttpGet, Authorize]
        public async Task<IActionResult> GetCoursesectionsForCourse(Guid courseId, [FromQuery] CourseSectionMgtParameters courseSectionMgtParameters)
        {
            var course = await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionsFromDb = await _repository.CourseSectionMgt.GetCoursesectionsAsync(courseId, courseSectionMgtParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(coursesectionsFromDb.MetaData));

            var coursesectionsDto = _mapper.Map<IEnumerable<CourseSectionDto>>(coursesectionsFromDb);

            return Ok(coursesectionsDto);
        }

        [HttpGet("{id}", Name="GetCoursesectionForCourse"), Authorize]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetCoursesectionForCourse(Guid courseId, Guid id)
        {
            var course =await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionDb =await _repository.CourseSectionMgt.GetCoursesectionAsync(courseId, id, trackChanges: false);
            if (coursesectionDb == null)
            {
                _logger.LogInfo($"Coursesection with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var coursesection = _mapper.Map<CourseSectionDto>(coursesectionDb);

            return Ok(coursesection);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourseSectionForCourse(Guid courseId, [FromBody] CourseSectionForCreationDto coursesection)
        {
            if (coursesection == null)
            {
                _logger.LogError("CourseSectionForCreationDto object sent from client is null.");
                return BadRequest("CourseSectionForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CourseSectionForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var course =await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionEntity = _mapper.Map<CourseSectionMgt>(coursesection);

            _repository.CourseSectionMgt.CreateCourseSectionForCourse(courseId, coursesectionEntity);
            await _repository.SaveAsync();

            var coursesectionToReturn = _mapper.Map<CourseSectionDto>(coursesectionEntity);

            return CreatedAtRoute("GetCoursesectionForCourse", new { courseId, id = coursesectionToReturn.Id }, coursesectionToReturn);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourseSectionForCourse(Guid courseId, Guid id)
        {
            var course = await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionForCourse =await _repository.CourseSectionMgt.GetCoursesectionAsync(courseId, id, trackChanges: false);
            if (coursesectionForCourse == null)
            {
                _logger.LogInfo($"CourseSection with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.CourseSectionMgt.DeleteCourseSection(coursesectionForCourse);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCourseSectionForCourse(Guid courseId, Guid id, [FromBody] CourseSectionForUpdateDto coursesection)
        {
            if (coursesection == null)
            {
                _logger.LogError("CourseSectionForUpdateDto object sent from client is null.");
                return BadRequest("CourseSectionForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CourseSectionForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var course = await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionEntity = await _repository.CourseSectionMgt.GetCoursesectionAsync(courseId, id, trackChanges: true);
            if (coursesectionEntity == null)
            {
                _logger.LogInfo($"Enrollment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(coursesection, coursesectionEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> PartiallyUpdateCourseSectionForCourse(Guid courseId, Guid id, [FromBody] JsonPatchDocument<CourseSectionForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var course =await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionEntity = await _repository.CourseSectionMgt.GetCoursesectionAsync(courseId, id, trackChanges: true);
            if (coursesectionEntity == null)
            {
                _logger.LogInfo($"coursesection with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var coursesectionToPatch = _mapper.Map<CourseSectionForUpdateDto>(coursesectionEntity);

            patchDoc.ApplyTo(coursesectionToPatch, ModelState);

            TryValidateModel(coursesectionToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(coursesectionToPatch, coursesectionEntity);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
