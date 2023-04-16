using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class SaleMaster
    {
        [Key]
        public long SaleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string Gender { get; set; }
    }
}
