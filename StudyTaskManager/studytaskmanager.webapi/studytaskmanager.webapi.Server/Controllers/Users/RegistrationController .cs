using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.Users.Commands.CreateUser;
using StudyTaskManager.Application.Entity.Users.Queries;
using StudyTaskManager.Application.Entity.Users.Queries.GetUserById;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.WebAPI.Abstractions;
using StudyTaskManager.WebAPI.Contracts.Users;


namespace StudyTaskManager.WebAPI.Controllers.Users
{
    [Route("api/[controller]")]
    public sealed class RegistrationController : ApiController
    {
        public RegistrationController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(
            [FromBody] RegisterUserRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = new CreateUserCommand(
                request.Username, 
                request.Email, 
                request.Password, 
                request.PhoneNumber, 
                request.SystemRole);

            Result<Guid> result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = result.Value },
                result.Value);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(id);

            Result<UserResponse> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }
    }
}
