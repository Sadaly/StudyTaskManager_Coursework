using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageCreate;
using StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageDelete;
using StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageUpdate;
using StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetAll;
using StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Commands.GroupChatParticipantLastReadCreate;
using StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Commands.GroupChatParticipantLastReadUpdate;
using StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Queries.GroupChatParticipantLastReadGet;
using StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Queries.GroupChatParticipantLastReadGetAll;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupChatParticipantLastReadController : ApiController
    {
        public GroupChatParticipantLastReadController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] GroupChatParticipantLastReadCreateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response) : HandleFailure(response);
        }

        //[Authorize]
        [HttpGet("{userId:guid}_{groupChatId:guid}")]
        public async Task<IActionResult> Get(
            Guid userId,
            Guid groupChatId,
            CancellationToken cancellationToken)
        {
            var query = new GroupChatParticipantLastReadGetQuery(userId, groupChatId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var query = new GroupChatParticipantLastReadGetAllQuery(null);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpPut("{userId:guid}_{groupChatId:guid}")]
        public async Task<IActionResult> Update(
            Guid userId,
            Guid groupChatId,
            [FromBody] ulong newLastReadId,
            CancellationToken cancellationToken)
        {
            var command = new GroupChatParticipantLastReadUpdateCommand(userId, groupChatId, newLastReadId);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : HandleFailure(response);
        }

    }
}
