using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MagicOFBulgarian.Data.Domain;
using System.ComponentModel;

namespace MagicOFBulgarian.Models
{
    public class CartProduct
    {
        [Key]
        public int Id { get; set; }

        public int ProductId {  get; set; }
        [Required]
        [ForeignKey("ProductId")]
       
        public Product Product { get; set; }
        public int CartId { get; set; }

        [Required]
        [ForeignKey("CartId")]
        public ShoppingCart Cart { get; set;}
       

        [Required]
        [Range(1, 100,ErrorMessage="Enter valid number between 1 and 100")]
        public int Quantity { get; set; }

        [Required]
        [DefaultValue(0)]
        public double Price { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool Included { get; set; }
    }
}
