using Microsoft.AspNetCore.Mvc;
using SPILSalesOrder.API.Models;
using SPILSalesOrder.Application.Services;
using SPILSalesOrder.Domain.Entities;

namespace SPILSalesOrder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesOrdersController : ControllerBase
{
    private readonly OrderService _service;
    public SalesOrdersController(OrderService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var order = await _service.GetByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOrderRequest req)
    {
        if (req.Details == null || !req.Details.Any())
            return BadRequest("Order must have at least one line item.");

        var order = new OrderHeader
        {
            ClientId = req.ClientId,
            OrderDate = req.OrderDate,
            Address1 = req.Address1,
            Address2 = req.Address2,
            Address3 = req.Address3,
            Suburb = req.Suburb,
            State = req.State,
            PostalCode = req.PostalCode,
            InvoiceNo = req.InvoiceNo,
            InvoiceDate = req.InvoiceDate,
            ReferenceNo = req.ReferenceNo,
            Note = req.Note,
            Details = req.Details.Select(d => new OrderDetail
            {
                ItemId = d.ItemId,
                Description = d.Description,
                Note = d.Note,
                Quantity = d.Quantity,
                Price = d.Price,
                TaxRate = d.TaxRate,
                ExclAmount = d.ExclAmount,
                TaxAmount = d.TaxAmount,
                InclAmount = d.InclAmount
            }).ToList()
        };

        try
        {
            await _service.AddAsync(order);
            return CreatedAtAction(nameof(Get), new { id = order.OrderId }, order);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // For local development return the exception message to help debug 500s.
            // In production you'd want a safer, non-sensitive message here.
            var logger = HttpContext.RequestServices.GetService<ILogger<SalesOrdersController>>();
            logger?.LogError(ex, "Error saving order");
            return StatusCode(500, ex.Message);
        }
    }
}
