using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using SchoolAPI.ModelBinders;
using Microsoft.AspNetCore.JsonPatch;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;

namespace SchoolAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
 
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public UserController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary> 
        /// Gets the list of all users
        /// </summary>
        /// <returns>The companies list</returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _repository.User.GetAllUsersAsync(trackChanges: false);

                var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetUsers)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name ="UserById"), Authorize]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _repository.User.GetUserAsync(id, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var UserDto = _mapper.Map<UserDto>(user);
                return Ok(UserDto);
            }
        }

       [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto user)
        {
            if (user == null)
            {
                _logger.LogError("UserForCreationDto object sent from client is null.");
                return BadRequest("UserForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the UserForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var userEntity = _mapper.Map<User>(user);

            _repository.User.CreateUser(userEntity);
            await _repository.SaveAsync();

            var userToReturn = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("UserById", new { id = userToReturn.Id }, userToReturn);
        }

        [HttpGet("collection/({ids})", Name = "UserCollection"), Authorize]
        public async Task<IActionResult> GetUserCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var userEntities = await _repository.User.GetByIdsAsync(ids, trackChanges: false);

            if (ids.Count() != userEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(userEntities);
            return Ok(usersToReturn);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateUserCollection([FromBody] IEnumerable<UserForCreationDto> userCollection)
        {
            if (userCollection == null)
            {
                _logger.LogError("User collection sent from client is null.");
                return BadRequest("User collection is null");
            }

            var userEntities = _mapper.Map<IEnumerable<User>>(userCollection);
            foreach (var user in userEntities)
            {
                _repository.User.CreateUser(user);
            }

            await _repository.SaveAsync();

            var userCollectionToReturn = _mapper.Map<IEnumerable<UserDto>>(userEntities);
            var ids = string.Join(",", userCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("UserCollection", new { ids }, userCollectionToReturn);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _repository.User.GetUserAsync(id, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.User.DeleteUser(user);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserForUpdateDto user)
        {
            if (user == null)
            {
                _logger.LogError("UserForUpdateDto object sent from client is null.");
                return BadRequest("UserForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the UserForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var userEntity = await _repository.User.GetUserAsync(id, trackChanges: true);
            if (userEntity == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(user, userEntity);
            await _repository.SaveAsync();


            return NoContent();
        }

        [HttpPatch("{id}"), Authorize]
        public async Task<IActionResult> PartiallyUpdateUser(Guid Id, [FromBody] JsonPatchDocument<UserForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var user = await _repository.User.GetUserAsync(Id, trackChanges: true);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {Id} doesn't exist in the database.");
                return NotFound();
            }

            

            var UserToPatch = _mapper.Map<UserForUpdateDto>(user);

            patchDoc.ApplyTo(UserToPatch, ModelState);
            TryValidateModel(UserToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(UserToPatch, user);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
