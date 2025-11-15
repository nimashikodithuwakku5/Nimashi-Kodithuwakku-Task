using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPILSalesOrder.Domain.Entities
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [ForeignKey("OrderHeader")]
        public int OrderId { get; set; }
        public OrderHeader? OrderHeader { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(300)]
        public string? Note { get; set; }

        public decimal Quantity { get; set; }

            public decimal? Price { get; set; }

            public decimal? TaxRate { get; set; }

        public decimal? ExclAmount { get; set; }

        public decimal? TaxAmount { get; set; }

        public decimal? InclAmount { get; set; }
    }
}
