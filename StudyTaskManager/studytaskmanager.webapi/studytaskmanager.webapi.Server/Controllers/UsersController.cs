using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.Users.Commands.UserCreate;
using StudyTaskManager.Application.Entity.Users.Commands.UserLogin;
using StudyTaskManager.Application.Entity.Users.Queries;
using StudyTaskManager.Application.Entity.Users.Queries.GetUserById;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.WebAPI.Abstractions;
using StudyTaskManager.WebAPI.Contracts.Users;


namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public sealed class UsersController : ApiController
    {
        public UsersController(ISender sender) : base(sender) { }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUserRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = new UserCreateCommand(
                request.Username,
                request.Email,
                request.Password,
                request.PhoneNumber,
                request.SystemRole);

            Result<Guid> result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser(
            [FromBody] UserLoginRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = new UserLoginCommand(
                request.Email, request.Password);

            Result<string> tokenResult = await Sender.Send(command, cancellationToken);

            return tokenResult.IsSuccess ? Ok(tokenResult.Value) : HandleFailure(tokenResult);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var query = new UserGetByIdQuery(id);

            Result<UserResponse> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var query = new GetAllUsersQuery();

            Result<List<User>> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }
    }
}
