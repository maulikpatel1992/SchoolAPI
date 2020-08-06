using AutoMapper;

using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<UserAuth> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IRepositoryManager _repository;
        public AuthenticationController(ILoggerManager logger, IMapper mapper, UserManager<UserAuth> userManager, IAuthenticationManager authManager, IRepositoryManager repository)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _repository = repository;
        }

        [HttpPost(Name = "RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var userauth = _mapper.Map<UserAuth>(userForRegistration);
            
            var result = await _userManager.CreateAsync(userauth, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            if (!userForRegistration.Roles.Any())
            {
                _logger.LogInfo("Roles doesn't exist in the registration DTO object, adding the default one.");
                await _userManager.AddToRoleAsync(userauth, "Student");
            }
            else if (userForRegistration.Roles.Last().ToLower().Equals("admin"))
            {
                _logger.LogError("User can not register using Admin role. Please register using correct role");
                return BadRequest("User can not register using Admin role. Please register using correct role");

            }
            else
            {
                await _userManager.AddToRolesAsync(userauth, userForRegistration.Roles);
            }

            UserController userController = new UserController(_repository, _logger, _mapper);
            UserForCreationDto user = new UserForCreationDto();
            user.Password = userForRegistration.Password;
            user.Email = userForRegistration.Email;
            user.SystemRoleId = userForRegistration.Roles.Last();
            user.UpdatedDate = new System.DateTime();
            user.CreatedDate = new System.DateTime();
            user.Status = "Active";

            var user_created = await userController.CreateUser(user);

            return user_created;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken() });
        }
    }
}