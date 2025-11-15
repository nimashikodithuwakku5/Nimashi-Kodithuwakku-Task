using Microsoft.AspNetCore.Mvc;
using SPILSalesOrder.Application.Services;

namespace SPILSalesOrder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ItemService _service;
    public ItemsController(ItemService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }
}
