namespace SPILSalesOrder.API.Models;

public class CreateOrderRequest
{
    public int ClientId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? Suburb { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    
    public string? InvoiceNo { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public string? ReferenceNo { get; set; }
    public string? Note { get; set; }
    public List<OrderDetailDto> Details { get; set; } = new();
}
