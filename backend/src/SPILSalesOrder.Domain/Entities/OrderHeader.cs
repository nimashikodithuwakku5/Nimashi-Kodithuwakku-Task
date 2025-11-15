using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPILSalesOrder.Domain.Entities
{
    public class OrderHeader
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public DateTime OrderDate { get; set; }

        [MaxLength(200)]
        public string? Address1 { get; set; }

        [MaxLength(200)]
        public string? Address2 { get; set; }

        [MaxLength(200)]
        public string? Address3 { get; set; }

        [MaxLength(100)]
        public string? Suburb { get; set; }

        [MaxLength(50)]
        public string? State { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? InvoiceNo { get; set; }

        public DateTime? InvoiceDate { get; set; }

        [MaxLength(100)]
        public string? ReferenceNo { get; set; }

        [MaxLength(1000)]
        public string? Note { get; set; }

        public List<OrderDetail> Details { get; set; } = new();
    }
}
