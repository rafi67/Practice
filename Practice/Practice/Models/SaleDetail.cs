using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class SaleDetail
    {
        [Key]
        public long SaleDetailId { get; set; }
        public long? SaleId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
    }
}
