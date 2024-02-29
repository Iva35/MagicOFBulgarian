using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicOFBulgarian.Models
{
    public class OrderProduct
    {
        [Key] 
        public int Id { get; set; }
        public int OrderId { get; set; }
        [Required]
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        
        public int ProductId { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
           

    }
}
