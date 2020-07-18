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
    [Route("api/assignmentsforsec")]
    [ApiController]
    public class AssignmentForSecController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AssignmentForSecController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        protected IActionResult GetAllAssignmentsForSec()
        {
            try
            {
                var assignments = _repository.Assignment.GetAllAssignmentsForSec(trackChanges: false);

                var assignmentsDto = _mapper.Map<IEnumerable<AssignmentDto>>(assignments);

                return Ok(assignmentsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllAssignmentsForSec)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetAssignmentForSecById")]
        public IActionResult GetAssignmentForSec(Guid id)
        {
            var assignment = _repository.Assignment.GetAssignmentForSec(id, trackChanges: false);
            if (assignment == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            else
            {
                var assignmentDto = _mapper.Map<AssignmentDto>(assignment);
                return Ok(assignmentDto);
            }
        }

        [HttpPost]
        public IActionResult CreateAssignment([FromBody] AssignmentForCreationDto assignment)
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

            var assignmentEntity = _mapper.Map<Assignment>(assignment);

            _repository.Assignment.CreateAssignmentForSec(assignmentEntity);
            _repository.Save();

            var assignmentToReturn = _mapper.Map<AssignmentDto>(assignmentEntity);

            return CreatedAtRoute("GetAssignmentForSecById", new { id = assignmentToReturn.Id }, assignmentToReturn);
        }
    }
}
