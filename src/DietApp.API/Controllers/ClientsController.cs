using DietApp.Application.Common.Models;
using DietApp.Application.Features.Clients.Commands.CreateClient;
using DietApp.Application.Features.Clients.Commands.DeleteClient;
using DietApp.Application.Features.Clients.Commands.UpdateClient;
using DietApp.Application.Features.Clients.DTOs;
using DietApp.Application.Features.Clients.Queries.GetClientById;
using DietApp.Application.Features.Clients.Queries.GetClients;
using DietApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DietApp.API.Controllers;

[Authorize]
public class ClientsController : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ClientListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClients(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] ClientStatus? status = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool sortDescending = false)
    {
        var query = new GetClientsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status,
            SearchTerm = searchTerm,
            SortBy = sortBy,
            SortDescending = sortDescending
        };

        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClient(Guid id)
    {
        var query = new GetClientByIdQuery { ClientId = id };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientRequest request)
    {
        var command = new CreateClientCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Gender = request.Gender,
            BirthYear = request.BirthYear,
            BirthMonth = request.BirthMonth,
            Height = request.Height,
            TargetWeight = request.TargetWeight,
            GoalDescription = request.GoalDescription,
            Allergies = request.Allergies,
            HealthNotes = request.HealthNotes,
            Tags = request.Tags,
            Notes = request.Notes
        };

        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetClient), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateClient(Guid id, [FromBody] UpdateClientRequest request)
    {
        var command = new UpdateClientCommand
        {
            ClientId = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Gender = request.Gender,
            BirthYear = request.BirthYear,
            BirthMonth = request.BirthMonth,
            Height = request.Height,
            TargetWeight = request.TargetWeight,
            GoalDescription = request.GoalDescription,
            Allergies = request.Allergies,
            HealthNotes = request.HealthNotes,
            Tags = request.Tags,
            Status = request.Status,
            Notes = request.Notes
        };

        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteClient(Guid id)
    {
        var command = new DeleteClientCommand { ClientId = id };
        await Mediator.Send(command);
        return NoContent();
    }
}
