using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using StudyTaskManager.Application.Entity.Users.Commands.UserCreate;
using StudyTaskManager.Application.Entity.Users.Commands.UserLogin;
using StudyTaskManager.Application.Entity.Users.Queries.GetUserById;
using StudyTaskManager.Application.Entity.Users.Queries.TakeUsers;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.WebAPI.Abstractions;
using StudyTaskManager.WebAPI.Controllers.SupportData;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public sealed class UsersController : ApiController
    {
        public class UserFilter : IEntityFilter<User>
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

            if (tokenResult.IsSuccess)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, 
                    SameSite = SameSiteMode.Strict, 
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                };
                Response.Cookies.Delete("access_token");
                Response.Cookies.Append("access_token", tokenResult.Value, cookieOptions);
                string? userId = JwtHelper.GetClaim(tokenResult.Value, "sub");

                return Ok(new { message = userId });
            }

            return HandleFailure(tokenResult);
        }

		[HttpPost("Logout")]
		public IActionResult LogoutUser()
		{
			Response.Cookies.Delete("access_token");
            return Ok();
		}

		[Authorize(Roles = "User")]
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            var query = new UserGetByIdQuery(userId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [Authorize(Roles = "User")]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] UserFilter filter,
            CancellationToken cancellationToken)
        {
            var query = new UsersGetAllQuery(filter.ToPredicate());
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [Authorize(Roles = "User")]
        [HttpGet("Take")]
        public async Task<IActionResult> TakeUsers(
            [FromQuery] TakeData<UserFilter, User> take,
            CancellationToken cancellationToken)
        {
            var query = new UsersTakeQuery(take.StartIndex, take.Count, take.Filter?.ToPredicate());
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMe()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(new { userId, role });
        }

    }
}