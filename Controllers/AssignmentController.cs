using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAssignmentsForCourse(Guid courseId)
        {
            var course = await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsFromDb =await _repository.Assignment.GetAssignmentsAsync(courseId, trackChanges: false);

            var assignmentsDto = _mapper.Map<IEnumerable<AssignmentDto>>(assignmentsFromDb);

            return Ok(assignmentsDto);
        }

        

        [HttpGet("{id}", Name = "GetAssignmentForCourse"), Authorize]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetAssignmentForCourse(Guid courseId, Guid id)
        {
            var course = await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentDb =await _repository.Assignment.GetAssignmentAsync(courseId, id, trackChanges: false);
            if (assignmentDb == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var assignment = _mapper.Map<AssignmentDto>(assignmentDb);

            return Ok(assignment);
        }

        [HttpPost, Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateAssignmentForCourse(Guid courseId, [FromBody] AssignmentForCreationDto assignment)
        {
            if (assignment == null)
            {
                _logger.LogError("AssignmentForCreationDto object sent from client is null.");
                return BadRequest("AssignmentForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the AssignmentForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var course =await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentEntity = _mapper.Map<Assignment>(assignment);

            _repository.Assignment.CreateAssignmentForCourse(courseId, assignmentEntity);
            await _repository.SaveAsync();

            var assignmentToReturn = _mapper.Map<AssignmentDto>(assignmentEntity);

            return CreatedAtRoute("GetAssignmentForCourse", new { courseId, id = assignmentToReturn.Id }, assignmentToReturn);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteAssignmentForCourse(Guid courseId, Guid id)
        {
            var course = await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentForCourse = await _repository.Assignment.GetAssignmentAsync(courseId, id, trackChanges: false);
            if (assignmentForCourse == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Assignment.DeleteAssignment(assignmentForCourse);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}"), Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateAssignmentForCourse(Guid courseId, Guid id, [FromBody] AssignmentForUpdateDto assignment)
        {
            if (assignment == null)
            {
                _logger.LogError("AssignmentForUpdateDto object sent from client is null.");
                return BadRequest("AssignmentForUpdateDto object is null");
            }
            
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the AssignmentForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var course = await _repository.CourseMgt.GetCourseAsync(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentEntity = await _repository.Assignment.GetAssignmentAsync(courseId, id, trackChanges: true);
            if (assignmentEntity == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(assignment, assignmentEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}"), Authorize(Roles = "Teacher")]
        public async Task<IActionResult> PartiallyUpdateAssignmentForCourse(Guid courseId, Guid id, [FromBody] JsonPatchDocument<AssignmentForUpdateDto> patchDoc)
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

            var assignmentEntity =await _repository.Assignment.GetAssignmentAsync(courseId, id, trackChanges: true);
            if (assignmentEntity == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentToPatch = _mapper.Map<AssignmentForUpdateDto>(assignmentEntity);

            patchDoc.ApplyTo(assignmentToPatch, ModelState);
            TryValidateModel(assignmentToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }


            _mapper.Map(assignmentToPatch, assignmentEntity);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
