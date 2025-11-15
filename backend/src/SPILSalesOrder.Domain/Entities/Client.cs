using System.ComponentModel.DataAnnotations;

namespace SPILSalesOrder.Domain.Entities
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Address1 { get; set; }

        [MaxLength(200)]
        public string? Address2 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }
    
        [MaxLength(200)]
        public string? Address3 { get; set; }

        [MaxLength(100)]
        public string? Suburb { get; set; }

        [MaxLength(50)]
        public string? State { get; set; }

        [MaxLength(20)]
        public string? PostCode { get; set; }
    }
}
