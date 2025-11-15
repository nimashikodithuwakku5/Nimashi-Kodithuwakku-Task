namespace SPILSalesOrder.API.Models;

public class OrderDetailDto
{
    public int ItemId { get; set; }
    public string? Description { get; set; }
    public string? Note { get; set; }
    public decimal Quantity { get; set; }
    public decimal? Price { get; set; }
    public decimal? TaxRate { get; set; }
    public decimal? ExclAmount { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? InclAmount { get; set; }
}
