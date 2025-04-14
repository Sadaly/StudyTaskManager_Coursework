using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.Users.Queries;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.WebAPI.Abstractions;
using StudyTaskManager.Application.Entity.SystemRoles.Queries.SystemRoleGetById;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Application.Entity.Users.Queries.GetUserById;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleCreate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleDelete;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleUpdatePrivileges;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleUpdateTitle;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SystemRoleController : ApiController
    {
        public SystemRoleController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] SystemRoleCreateCommand request,
            CancellationToken cancellationToken)
        {
            var command = new SystemRoleCreateCommand(
                request.Name,
                request.CanViewPeoplesGroups,
                request.CanChangeSystemRoles,
                request.CanBlockUsers,
                request.CanDeleteChats);

            Result<Guid> response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpDelete("{systemRoleId:guid}")]
        public async Task<IActionResult> Delete(
            Guid systemRoleId,
            CancellationToken cancellationToken)
        {
            var command = new SystemRoleDeleteCommand(systemRoleId);

            Result response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }


        //[Authorize]
        [HttpGet("{systemRoleId:guid}")]
        public async Task<IActionResult> GetUserById(Guid systemRoleId, CancellationToken cancellationToken)
        {
            var query = new SystemRoleGetByIdQuery(systemRoleId);

            Result<SystemRole> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        public sealed record SystemRoleUpdatePrivilegesCommandRequest(
            bool CanViewPeoplesGroups,
            bool CanChangeSystemRoles,
            bool CanBlockUsers,
            bool CanDeleteChats);

        //[Authorize]
        [HttpPut("{systemRoleId:guid}/privileges")]
        public async Task<IActionResult> UpdatePrivileges(
            Guid systemRoleId,
            [FromBody] SystemRoleUpdatePrivilegesCommandRequest request,
            CancellationToken cancellationToken)
        {
            var command = new SystemRoleUpdatePrivilegesCommand(
                systemRoleId,
                request.CanViewPeoplesGroups,
                request.CanChangeSystemRoles,
                request.CanBlockUsers,
                request.CanDeleteChats);

            Result response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpPut("{systemRoleId:guid}/title")]
        public async Task<IActionResult> UpdateTitle(
            Guid systemRoleId,
            string NewTitle,
            CancellationToken cancellationToken)
        {
            var command = new SystemRoleUpdateTitleCommand(
                systemRoleId,
                NewTitle);

            Result response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }
    }
}