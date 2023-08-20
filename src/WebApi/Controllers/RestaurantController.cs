﻿using Application.Common.Models;
using Application.UseCases.Restaurant.Commands.CreateRestaurant;
using Application.UseCases.Restaurant.Commands.DeleteRestaurant;
using Application.UseCases.Restaurant.Commands.UpdateRestaurant;
using Application.UseCases.Restaurant.Queries.GetRestaurant;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class RestaurantController : BaseController
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> Create(CreateRestaurantCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<RestaurantEntity>>> GetWithPagination([FromQuery] GetRestaurantWithPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, UpdateRestaurantCommand command)
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
        await Mediator.Send(new DeleteRestaurantCommand(id));

        return NoContent();
    }
}
