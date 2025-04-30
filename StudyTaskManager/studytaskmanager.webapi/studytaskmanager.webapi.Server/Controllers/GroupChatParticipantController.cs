using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.WebAPI.Abstractions;
using StudyTaskManager.Application.Entity.GroupChatParticipants.Commands.GroupChatParticipantCreate;
using StudyTaskManager.Application.Entity.GroupChatParticipants.Commands.GroupChatParticipantDelete;
using StudyTaskManager.Application.Entity.GroupChatParticipants.Queries.GroupChatParticipantGetAll;
using StudyTaskManager.Application.Entity.GroupChatParticipants.Queries.GroupChatParticipantGetByUserAndGroupChat;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupChatParticipantController : ApiController
    {
        public GroupChatParticipantController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] GroupChatParticipantCreateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpGet("{userId:Guid}_{groupChatId:guid}")]
        public async Task<IActionResult> Get(
            Guid userId,
            Guid groupChatId,
            CancellationToken cancellationToken)
        {
            var query = new GroupChatParticipantGetByUserAndGroupChatQuery(userId, groupChatId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var query = new GroupChatParticipantGetAllQuery(null);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(
            [FromBody] GroupChatParticipantDeleteCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }
    }
}
