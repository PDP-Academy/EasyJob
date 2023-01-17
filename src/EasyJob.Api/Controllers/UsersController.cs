using EasyJob.Application.DataTransferObjects;
using EasyJob.Application.Models;
using EasyJob.Application.Services.Users;
using EasyJob.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyJob.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(
            IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<UserDto>> PostUserAsync(
            UserForCreationDto userForCreationDto)
        {
            var createdUser = await this.userService
                .CreateUserAsync(userForCreationDto);

            return Created("", createdUser);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUsers(
            [FromQuery] QueryParameter queryParameter)
        {
            var users = this.userService
                .RetrieveUsers(queryParameter);

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
