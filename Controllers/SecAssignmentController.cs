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
    [Route("api/assignmentsforsec/{assignmentId}/assignmentsections")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SecAssignmentController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public SecAssignmentController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAssignmentSectionsForAssignment(Guid assignmentId)
        {
            var assignment = _repository.Assignment.GetAssignmentForSec(assignmentId, trackChanges: false);
            if (assignment == null)
            {
                _logger.LogInfo($"Assignment with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsectionsFromDb = _repository.SecAssignmentMgt.GetAssignmentSections(assignmentId, trackChanges: false);

            var assignmentsectionsDto = _mapper.Map<IEnumerable<SecAssignmentDto>>(assignmentsectionsFromDb);

            return Ok(assignmentsectionsDto);
        }

        [HttpGet("{id}", Name = "GetAssignmentsectionForAssignment")]
        public IActionResult GetAssignmentsectionForAssignment(Guid assignmentId, Guid id)
        {
            var assignment = _repository.Assignment.GetAssignmentForSec(assignmentId, trackChanges: false);
            if (assignment == null)
            {
                _logger.LogInfo($"Assignment with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsectionDb = _repository.SecAssignmentMgt.GetAssignmentSection(assignmentId, id, trackChanges: false);
            if (assignmentsectionDb == null)
            {
                _logger.LogInfo($"Assignment section with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsection = _mapper.Map<SecAssignmentDto>(assignmentsectionDb);

            return Ok(assignmentsection);
        }

        [HttpPost]
        public IActionResult CreateAssignmentSectionForAssignment(Guid assignmentId, [FromBody] SecAssignmentForCreationDto assignmentSection)
        {
            if (assignmentSection == null)
            {
                _logger.LogError("SecAssignmentForCreationDto object sent from client is null.");
                return BadRequest("SecAssignmentForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the SecAssignmentForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var assignment = _repository.Assignment.GetAssignmentForSec(assignmentId, trackChanges: false);
            if (assignment == null)
            {
                _logger.LogInfo($"Assignment with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsectionEntity = _mapper.Map<SecAssignmentMgt>(assignmentSection);

            _repository.SecAssignmentMgt.CreateAssignmentSectionForAssignment(assignmentId, assignmentsectionEntity);
            _repository.Save();

            var assignmensectionToReturn = _mapper.Map<SecAssignmentDto>(assignmentsectionEntity);

            return CreatedAtRoute("GetAssignmentsectionForAssignment", new { assignmentId, id = assignmensectionToReturn.Id }, assignmensectionToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAssignmentSectionForAssignment(Guid assignmentId, Guid id)
        {
            var assignment = _repository.Assignment.GetAssignmentForSec(assignmentId, trackChanges: false);
            if (assignment == null)
            {
                _logger.LogInfo($"Assignment with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsectionForAssignment = _repository.SecAssignmentMgt.GetAssignmentSection(assignmentId, id, trackChanges: false);
            if (assignmentsectionForAssignment == null)
            {
                _logger.LogInfo($"Assignment Section with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.SecAssignmentMgt.DeleteAssignmentSection(assignmentsectionForAssignment);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAssignmentSectionForAssignment(Guid assignmentId, Guid id, [FromBody] SecAssignmentForUpdateDto assignmentsection)
        {
            if (assignmentsection == null)
            {
                _logger.LogError("SecAssignmentForUpdateDto object sent from client is null.");
                return BadRequest("SecAssignmentForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the SecAssignmentForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var assignment = _repository.Assignment.GetAssignmentForSec(assignmentId, trackChanges: false);
            if (assignment == null)
            {
                _logger.LogInfo($"Assignment with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsectionEntity = _repository.SecAssignmentMgt.GetAssignmentSection(assignmentId, id, trackChanges: true);
            if (assignmentsectionEntity == null)
            {
                _logger.LogInfo($"Section with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(assignmentsection, assignmentsectionEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateAssignmentSectionForAssignment(Guid assignmentId, Guid id, [FromBody] JsonPatchDocument<SecAssignmentForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var assignment = _repository.Assignment.GetAssignmentForSec(assignmentId, trackChanges: false);
            if (assignment == null)
            {
                _logger.LogInfo($"Assignment with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsectionEntity = _repository.SecAssignmentMgt.GetAssignmentSection(assignmentId, id, trackChanges: true);
            if (assignmentsectionEntity == null)
            {
                _logger.LogInfo($"Assignmentsection with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsectionToPatch = _mapper.Map<SecAssignmentForUpdateDto>(assignmentsectionEntity);

            patchDoc.ApplyTo(assignmentsectionToPatch, ModelState);

            TryValidateModel(assignmentsectionToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(assignmentsectionToPatch, assignmentsectionEntity);

            _repository.Save();

            return NoContent();
        }
    }
}
