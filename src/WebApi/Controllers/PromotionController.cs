using Application.UseCases.Promotion.Commands.CreatePromotion;
using Application.UseCases.Promotion.Commands.DeletePromotionCommand;
using Application.UseCases.Promotion.Commands.UpdatePromotionCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class PromotionController : BaseController
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> Create(CreatePromotionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    // [HttpGet]
    // [Authorize]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // public async Task<ActionResult<PaginatedList<PromotionEntity>>> GetWithPagination([FromQuery] GetPromotionWithPaginationQuery query)
    // {
    //     return Ok(await Mediator.Send(query));
    // }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, UpdatePromotionCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeletePromotionCommand(id));

        return NoContent();
    }
}
