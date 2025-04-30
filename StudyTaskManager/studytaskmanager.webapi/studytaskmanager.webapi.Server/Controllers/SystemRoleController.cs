using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudyTaskManager.WebAPI.Abstractions;
using StudyTaskManager.Application.Entity.SystemRoles.Queries.SystemRoleGetById;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleCreate;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleUpdatePrivileges;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleUpdateTitle;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleDelete;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SystemRoleController : ApiController
    {
        public sealed record SystemRoleUpdatePrivilegesCommandData(
            bool CanViewPeoplesGroups,
            bool CanChangeSystemRoles,
            bool CanBlockUsers,
            bool CanDeleteChats);

        public SystemRoleController(ISender sender) : base(sender) { }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] SystemRoleCreateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

        [Authorize]
        [HttpDelete("{systemRoleId:guid}")]
        public async Task<IActionResult> Delete(
            Guid systemRoleId,
            CancellationToken cancellationToken)
        {
            var command = new SystemRoleDeleteCommand(systemRoleId);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }


        [Authorize]
        [HttpGet("{systemRoleId:guid}")]
        public async Task<IActionResult> GetUserById(
            Guid systemRoleId,
            CancellationToken cancellationToken)
        {
            var query = new SystemRoleGetByIdQuery(systemRoleId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [Authorize]
        [HttpPut("{systemRoleId:guid}/Privileges")]
        public async Task<IActionResult> UpdatePrivileges(
            Guid systemRoleId,
            [FromBody] SystemRoleUpdatePrivilegesCommandData data,
            CancellationToken cancellationToken)
        {
            var command = new SystemRoleUpdatePrivilegesCommand(
                systemRoleId,
                data.CanViewPeoplesGroups,
                data.CanChangeSystemRoles,
                data.CanBlockUsers,
                data.CanDeleteChats);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }

        [Authorize]
        [HttpPut("{systemRoleId:guid}/Title")]
        public async Task<IActionResult> UpdateTitle(
            Guid systemRoleId,
            [FromBody] string newTitle,
            CancellationToken cancellationToken)
        {
            var command = new SystemRoleUpdateTitleCommand(systemRoleId, newTitle);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }
    }
}