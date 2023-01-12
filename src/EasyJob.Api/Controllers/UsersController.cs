using EasyJob.Application.DataTransferObjects;
using EasyJob.Application.Services.Users;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EasyJob.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IValidator<UserForCreationDto> userForCreationDtoValidator;

        public UsersController(
            IUserService userService,
            IValidator<UserForCreationDto> userForCreationDtoValidator)
        {
            this.userService = userService;
            this.userForCreationDtoValidator = userForCreationDtoValidator;
        }

        [HttpPost]
        public async ValueTask<ActionResult<UserDto>> PostUserAsync(
            UserForCreationDto userForCreationDto)
        {
            var result = await this.userForCreationDtoValidator
                .ValidateAsync(userForCreationDto);

            if(!result.IsValid)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.ErrorCode, error.ErrorMessage);
                }
                
                return BadRequest(ModelState);
            }

            var createdUser = await this.userService
                .CreateUserAsync(userForCreationDto);

            return Created("", createdUser);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = this.userService
                .RetrieveUsers();

            return Ok(users);
        }

        [HttpGet("{userId:guid}")]
        public async ValueTask<ActionResult<UserDto>> GetUserByIdAsync(
            Guid userId)
        {
            var user = await this.userService
                .RetrieveUserByIdAsync(userId);

            return Ok(user);
        }

        [HttpPut]
        public async ValueTask<ActionResult<UserDto>> PutUserAsync(
            UserForModificationDto userForModificationDto)
        {
            var modifiedUser = await this.userService
                .ModifyUserAsync(userForModificationDto);

            return Ok(modifiedUser);
        }

        [HttpDelete("{userId:guid}")]
        public async ValueTask<ActionResult<UserDto>> DeleteUserAsync(
            Guid userId)
        {
            var removed = await this.userService
                .RemoveUserAsync(userId);

            return Ok(removed);
        }
    }
}
