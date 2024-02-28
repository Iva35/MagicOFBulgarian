﻿using System.ComponentModel.DataAnnotations.Schema;
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

        [Required]
        [ForeignKey("ProductId")]
        public int ProductId {  get; set; }
       


        [Required]
        [ForeignKey("CartId")]
        public int CartId { get; set; }
       

        [Required]
        [Range(1, 100,ErrorMessage="Enter valid number between 1 and 100")]
        public int Quantity;

        [Required]
        [DefaultValue(0)]
        public double Price { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool Included { get; set; }
    }
}
