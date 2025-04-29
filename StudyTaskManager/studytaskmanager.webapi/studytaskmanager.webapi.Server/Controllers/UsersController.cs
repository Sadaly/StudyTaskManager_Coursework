using System.Linq.Expressions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.Users.Commands.UserCreate;
using StudyTaskManager.Application.Entity.Users.Commands.UserLogin;
using StudyTaskManager.Application.Entity.Users.Queries.GetUserById;
using StudyTaskManager.Application.Entity.Users.Queries.TakeUsers;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public sealed class UsersController : ApiController
    {
        public class UserFilter
        {
            public string? Username { get; set; }
            public string? Email { get; set; }
            public bool? IsEmailVerified { get; set; }
            public bool? IsPhoneNumberVerified { get; set; }
            public string? PhoneNumber { get; set; }
            public DateTime? RegistrationDateFrom { get; set; }
            public DateTime? RegistrationDateTo { get; set; }
            public Guid? SystemRoleId { get; set; }

            public Expression<Func<User, bool>> ToPredicate()
            {
                return user =>
                    (string.IsNullOrEmpty(Username) || user.Username.Value.Contains(Username)) &&
                    (string.IsNullOrEmpty(Email) || user.Email.Value.Contains(Email)) &&
                    (!IsEmailVerified.HasValue || user.IsEmailVerifed == IsEmailVerified.Value) &&
                    (!IsPhoneNumberVerified.HasValue || user.IsPhoneNumberVerifed == IsPhoneNumberVerified.Value) &&
                    (string.IsNullOrEmpty(PhoneNumber) || (user.PhoneNumber != null && user.PhoneNumber.Value.Contains(PhoneNumber))) &&
                    (!RegistrationDateFrom.HasValue || user.RegistrationDate >= RegistrationDateFrom.Value) &&
                    (!RegistrationDateTo.HasValue || user.RegistrationDate <= RegistrationDateTo.Value) &&
                    (!SystemRoleId.HasValue || user.SystemRoleId == SystemRoleId);
            }
        }

        public sealed record TakeUsersData(
            int StartIndex,
            int Count,
            UserFilter UserFilter);

        public UsersController(ISender sender) : base(sender) { }

        [HttpPost("Registration")]
        public async Task<IActionResult> CreateUser(
            [FromBody] UserCreateCommand command,
            CancellationToken cancellationToken)
        {
            Result<Guid> result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(
            [FromBody] UserLoginCommand command,
            CancellationToken cancellationToken)
        {
            Result<string> tokenResult = await Sender.Send(command, cancellationToken);

            return tokenResult.IsSuccess ? Ok(tokenResult.Value) : HandleFailure(tokenResult);
        }

        [Authorize]
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            var query = new UserGetByIdQuery(userId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] UserFilter userFilter,
            CancellationToken cancellationToken)
        {
            var query = new UsersGetAllQuery(userFilter.ToPredicate());
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [HttpGet("Take")]
        public async Task<IActionResult> TakeUsers(
            [FromQuery] TakeUsersData data,
            CancellationToken cancellationToken)
        {
            var query = new UsersTakeQuery(data.StartIndex, data.Count, data.UserFilter.ToPredicate());
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }
    }
}