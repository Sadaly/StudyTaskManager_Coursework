using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatAddMessage;
using StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatCreate;
using StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatDelete;
using StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatGetById;
using StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatsGetAll;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class PersonalChatController : ApiController
    {
        public PersonalChatController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] PersonalChatCreateCommand request,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpGet("{perconalChatId:guid}")]
        public async Task<IActionResult> GetUserById(
            Guid perconalChatId,
            CancellationToken cancellationToken)
        {
            var query = new PersonalChatGetByIdQuery(perconalChatId);

            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpGet("Chats/{userId:guid}")]
        public async Task<IActionResult> GetUserByUser(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var query = new PersonalChatsGetAllQuery(pc => pc.UsersID.Contains(userId));

            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> GetUserByUser(
            [FromBody] PersonalChatAddMessageCommand query,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }


        //[Authorize]
        [HttpDelete("{personalChatId:guid}")]
        public async Task<IActionResult> Delete(
            Guid personalChatId,
            CancellationToken cancellationToken)
        {
            var command = new PersonalChatDeleteCommand(personalChatId);

            Result response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }
    }
}
