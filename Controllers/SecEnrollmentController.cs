using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
       [HttpGet("{id}", Name = "GetEnrollmentForUser")]
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

        [HttpPost]
        public IActionResult CreateEnrollmentForUser(Guid userId, [FromBody] EnrollmentForCreationDto enrollment)
        {
            if (enrollment == null)
            {
                _logger.LogError("EnrollmentForCreationDto object sent from client is null.");
                return BadRequest("EnrollmentForCreationDto object is null");
            }

            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentEntity = _mapper.Map<SecEnrollmentMgt>(enrollment);

            _repository.SecEnrollmentMgt.CreateEnrollmentForUser(userId, enrollmentEntity);
            _repository.Save();

            var enrollmentToReturn = _mapper.Map<EnrollmentDto>(enrollmentEntity);

            return CreatedAtRoute("GetEnrollmentForUser", new { userId, id = enrollmentToReturn.Id }, enrollmentToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEnrollmentForUser(Guid userId, Guid id)
        {
            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentForUser = _repository.SecEnrollmentMgt.GetEnrollment(userId, id, trackChanges: false);
            if (enrollmentForUser == null)
            {
                _logger.LogInfo($"Enrollment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.SecEnrollmentMgt.DeleteEnrollment(enrollmentForUser);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEnrollmentForUser(Guid userId, Guid id, [FromBody] EnrollmentForUpdateDto enrollment)
        {
            if (enrollment == null)
            {
                _logger.LogError("EnrollmentForUpdateDto object sent from client is null.");
                return BadRequest("EnrollmentForUpdateDto object is null");
            }

            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentEntity = _repository.SecEnrollmentMgt.GetEnrollment(userId, id, trackChanges: true);
            if (enrollmentEntity == null)
            {
                _logger.LogInfo($"Enrollment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(enrollment, enrollmentEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateEnrollmentForUser(Guid userId, Guid id, [FromBody] JsonPatchDocument<EnrollmentForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentEntity = _repository.SecEnrollmentMgt.GetEnrollment(userId, id, trackChanges: true);
            if (enrollmentEntity == null)
            {
                _logger.LogInfo($"Enrollment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentToPatch = _mapper.Map<EnrollmentForUpdateDto>(enrollmentEntity);

            patchDoc.ApplyTo(enrollmentToPatch);

            _mapper.Map(enrollmentToPatch, enrollmentEntity);

            _repository.Save();

            return NoContent();
        }
    }
}
