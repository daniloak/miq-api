using Microsoft.AspNetCore.Mvc;
using Omini.Miq.Api.Dtos;
using Omini.Miq.Business.Commands;
using Omini.Miq.Business.Queries;
using Omini.Miq.Domain.Repositories;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Api.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
public class PromissoriesController : MainController
{
    private readonly ILogger<PromissoriesController> _logger;

    public PromissoriesController(ILogger<PromissoriesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<PromissoryOutputDto>>> Get([FromQuery] QueryFilter queryFilter, [FromQuery] PaginationFilter paginationFilter)
    {
        var promissories = await Mediator.Send(new GetAllPromissoriesQuery(queryFilter, paginationFilter), default);
        var result = Mapper.Map<PagedResult<PromissoryOutputDto>>(promissories);

        return Ok(ResponseDto.ApiSuccess(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePromissoryCommand createPromissoryCommand)
    {
        var result = await Mediator.Send(createPromissoryCommand, default);

        return ToOk(result, Mapper.Map<PromissoryOutputDto>);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<PromissoryOutputDto>> GetById([FromServices] IPromissoryRepository repository, long id)
    {
        var promissory = await repository.GetById(id, default);

        if (promissory is null)
        {
            return BadRequest();
        }

        var result = Mapper.Map<PromissoryOutputDto>(promissory);

        return Ok(ResponseDto.ApiSuccess(result));
    }

    [HttpPut("{id:long}/cancel")]
    public async Task<IActionResult> Cancel(long id)
    {
        var result = await Mediator.Send(new CancelPromissoryCommand { Id = id }, default);

        return ToNoContent(result);
    }
}
