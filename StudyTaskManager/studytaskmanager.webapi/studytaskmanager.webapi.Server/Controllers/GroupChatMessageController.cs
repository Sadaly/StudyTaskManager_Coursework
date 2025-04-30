using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageCreate;
using StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageDelete;
using StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageUpdate;
using StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetAll;
using StudyTaskManager.WebAPI.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupChatMessageController : ApiController
    {
        public GroupChatMessageController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] GroupChatMessageCreateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var query = new GroupChatMessageGetAllQuery(null);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpDelete("{groupChatId:guid}_{ordinal:ulong}")]
        public async Task<IActionResult> Delete(
            Guid groupChatId,
            ulong ordinal,
            CancellationToken cancellationToken)
        {
            var command = new GroupChatMessageDeleteCommand(groupChatId, ordinal);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpPut("{groupChatId:guid}_{ordinal:ulong}")]
        public async Task<IActionResult> Update(
            Guid groupChatId,
            ulong ordinal,
            [FromBody] string newContent,
            CancellationToken cancellationToken)
        {
            var command = new GroupChatMessageUpdateCommand(groupChatId, ordinal, newContent);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }


    }
}
