using Api.Contracts.Links;
using Api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LinksController(ApplicationDbContext context) : ControllerBase
{
    [HttpPost("tg")]
    public async Task<IActionResult> LinkTelegramAsync(LinkTelegramRequest request)
    {
        ///TODO: Проверить, сущесвует ли пользователь а также совпадают пароли
        ///В случае успеха в бд записать айди телеграм пользователя
        ///Будет ли такое что можно перелинковать?()
        return Ok();
    }
}
