using StudyTaskManager.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.Groups.Commands.GroupCreate;
using StudyTaskManager.Application.Entity.Groups.Commands.GroupDelete;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Application.Entity.Groups.Queries.GroupGetById;
using StudyTaskManager.Application.Entity.Groups.Queries.GroupGetAll;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : ApiController
    {
        public GroupController(ISender sender) : base(sender) { }


        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] GroupCreateCommand request,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpGet("{groupId:guid}")]
        public async Task<IActionResult> GetUserById(
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var query = new GroupGetByIdQuery(groupId);

            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var query = new GroupGetAllQuery(null);

            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpDelete("{groupId:guid}")]
        public async Task<IActionResult> Delete(
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var command = new GroupDeleteCommand(groupId);

            Result response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }
    }
}
