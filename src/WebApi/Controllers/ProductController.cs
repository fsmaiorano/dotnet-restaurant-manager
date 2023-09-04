using Application.Common.Models;
using Application.UseCases.Product.Commands.CreateProduct;
using Application.UseCases.Product.Commands.DeleteProduct;
using Application.UseCases.Product.Commands.UpdateProduct;
using Application.UseCases.Product.Queries.GetProductWithPaginationQuery;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ProductController : BaseController
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> Create(CreateProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<ProductEntity>>> GetWithPagination([FromQuery] GetProductWithPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, UpdateProductCommand command)
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
        await Mediator.Send(new DeleteProductCommand(id));

        return NoContent();
    }
}
