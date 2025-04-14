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

            Result response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var command = new SystemRoleDeleteCommand(id);

            Result response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }


        //[Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var query = new SystemRoleGetByIdQuery(id);

            Result<SystemRole> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpPut("privileges/{systemRoleId:guid}")]
        public async Task<IActionResult> UpdatePrivileges(
            Guid systemRoleId,
            [FromBody] SystemRoleUpdatePrivilegesCommand request,
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
        [HttpPut("title/{systemRoleId:guid}")]
        public async Task<IActionResult> UpdateTitle(
            Guid systemRoleId,
            [FromBody] SystemRoleUpdateTitleCommand request,
            CancellationToken cancellationToken)
        {
            var command = new SystemRoleUpdateTitleCommand(
                systemRoleId,
                request.NewTitle);

            Result response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }
    }
}