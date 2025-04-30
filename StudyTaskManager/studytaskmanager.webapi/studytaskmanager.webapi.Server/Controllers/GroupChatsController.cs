using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.GroupChats.Commands.GroupChatCreate;
using StudyTaskManager.Application.Entity.GroupChats.Commands.GroupChatDelete;
using StudyTaskManager.Application.Entity.GroupChats.Queries.GroupChatGetAll;
using StudyTaskManager.Application.Entity.GroupChats.Queries.GroupChatGetById;
using StudyTaskManager.Application.Entity.Groups.Commands.GroupCreate;
using StudyTaskManager.Application.Entity.Groups.Commands.GroupDelete;
using StudyTaskManager.Application.Entity.Groups.Queries.GroupGetAll;
using StudyTaskManager.Application.Entity.Groups.Queries.GroupGetById;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupChatsController : ApiController
    {
        public GroupChatsController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] GroupChatCreateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpGet("{groupChatId:guid}")]
        public async Task<IActionResult> Get(
            Guid groupChatId,
            CancellationToken cancellationToken)
        {
            var query = new GroupChatGetByIdQuery(groupChatId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var query = new GroupChatGetAllQuery(null);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpDelete("{groupId:guid}")]
        public async Task<IActionResult> Delete(
            Guid groupChatId,
            CancellationToken cancellationToken)
        {
            var command = new GroupChatDeleteCommand(groupChatId);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }
    }
}
