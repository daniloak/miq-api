using Microsoft.AspNetCore.Mvc;
using Omini.Miq.Api.Dtos;
using Omini.Miq.Business.Queries;

namespace Omini.Miq.Api.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
public class ItemsController : MainController
{
    private readonly ILogger<ItemsController> _logger;
    public ItemsController(ILogger<ItemsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var result = await Mediator.Send(new GetItemByCode { Code = code });

        return ToOk(result, Mapper.Map<ItemOutputDto>);
    }
}
