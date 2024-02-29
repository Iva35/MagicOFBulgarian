using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MagicOFBulgarian.Data.Domain;

namespace MagicOFBulgarian.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        
        public string CustomerId { get; set; }
        
        [Required]
        [ForeignKey("CustomerId")]
        public CustomerUser Customer { get; set; }

        [Required]
        public double Price { get; set; }
        
        
    }
}
