namespace SPILSalesOrder.API.Models;

public class ClientDto
{
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
}
