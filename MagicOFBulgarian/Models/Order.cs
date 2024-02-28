using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MagicOFBulgarian.Data.Domain;


namespace MagicOFBulgarian.Models
{
    public class Order
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }
        

        [Required]
        public double Price { get; set; }

        [Required]
        public DateTime OrderMadeOn = DateTime.Now.Date;
        [Required]
        public DateTime ExpectedDelivery =DateTime.Now.Date.AddDays(15);
       
    }
}
