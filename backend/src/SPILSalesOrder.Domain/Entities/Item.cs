using System.ComponentModel.DataAnnotations;

namespace SPILSalesOrder.Domain.Entities
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ItemCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
