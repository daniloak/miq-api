using Microsoft.AspNetCore.Mvc;
using Omini.Miq.Api.Dtos;
using Omini.Miq.Business.Commands;

namespace Omini.Miq.Api.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : MainController
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<UserOutputDto>> Create([FromBody] CreateUserCommand createUserCommand)
    {
         var result = await Mediator.Send(createUserCommand, default);

         return Ok(ResponseDto.ApiSuccess(result));
    }
}
