using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.WebAPI.Abstractions;
using StudyTaskManager.Application.Entity.BlockedUserInfos.Commands.BlockedUserInfoCreate;
using StudyTaskManager.Application.Entity.BlockedUserInfos.Commands.BlockedUserInfoDelete;
using StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoGetAll;
using StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoGetByUserId;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BlockedUserInfoController : ApiController
    {
        public BlockedUserInfoController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] BlockedUserInfoCreateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response) : HandleFailure(response);
        }

        //[Authorize]
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetByUserId(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var query = new BlockedUserInfoGetByUserIdQuery(userId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var query = new BlockedUserInfoGetAllQuery(null);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Delete(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var command = new BlockedUserInfoDeleteCommand(userId);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : HandleFailure(response);
        }
    }
}
