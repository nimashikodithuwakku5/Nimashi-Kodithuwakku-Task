using Microsoft.AspNetCore.Mvc;
using SPILSalesOrder.Application.Services;
using SPILSalesOrder.Domain.Entities;

namespace SPILSalesOrder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ClientService _service;
    public ClientsController(ClientService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }
}
