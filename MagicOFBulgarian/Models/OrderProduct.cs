using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicOFBulgarian.Models
{
    public class OrderProduct
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        [ForeignKey("OrderId")]
        public int OrderID { get; set; }
        

        [Required]
        [ForeignKey("ProductId")]
        public int ProductID { get; set; }
           

    }
}
