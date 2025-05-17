using StudyTaskManager.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.Groups.Commands.GroupCreate;
using StudyTaskManager.Application.Entity.Groups.Commands.GroupDelete;
using StudyTaskManager.Application.Entity.Groups.Queries.GroupGetById;
using StudyTaskManager.Application.Entity.Groups.Queries.GroupGetAll;
using StudyTaskManager.Application.Entity.Groups.Queries.GroupTake;
using StudyTaskManager.WebAPI.Controllers.SupportData;
using System.Linq.Expressions;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : ApiController
    {
        public GroupController(ISender sender) : base(sender) { }

		public class GroupFilter : IEntityFilter<Group>
		{
			public string? Title { get; set; }
			public string? Description { get; set; }
			public Guid? DefaultRoleId { get; set; }

			public Expression<Func<Group, bool>> ToPredicate()
			{
				return group =>
					(string.IsNullOrEmpty(Title) || group.Title.Value.Contains(Title)) &&
					(string.IsNullOrEmpty(Description) || (group.Description != null && group.Description.Value.Contains(Description))) &&
					(!DefaultRoleId.HasValue || group.DefaultRoleId == DefaultRoleId.Value);
			}
		}
		//[Authorize]
		[HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] GroupCreateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpGet("{groupId:guid}")]
        public async Task<IActionResult> Get(
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var query = new GroupGetByIdQuery(groupId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(
			[FromQuery] TakeData<GroupFilter, Group> take,
			CancellationToken cancellationToken)
        {
            var query = new GroupGetAllQuery(take.Filter?.ToPredicate());
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

		[HttpGet("Take")]
		public async Task<IActionResult> Take(
			[FromQuery] TakeData<GroupFilter, Group> take,
			CancellationToken cancellationToken)
		{
			var query = new GroupTakeQuery(take.StartIndex, take.Count, take.Filter?.ToPredicate());
			var response = await Sender.Send(query, cancellationToken);

			return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
		}

		//[Authorize]
		[HttpDelete("{groupId:guid}")]
        public async Task<IActionResult> Delete(
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var command = new GroupDeleteCommand(groupId);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : HandleFailure(response);
        }
    }
}
