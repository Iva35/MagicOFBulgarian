using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicOFBulgarian.Models.NewModelView
{
    public class ProductImage
    {

		[ValidateNever]
        public IFormFile? ImageUrl {  get; set; }
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public double Price { get; set; }
		[Required]
		[Display(Name = "Price for 50+")]
		[Range(1, 100)]
		public double Price50 { get; set; }
		public int CategoryId { get; set; }
		[ForeignKey(("CategoryId"))]
		[ValidateNever]
		public Category Category { get; set; }

		public int AreaId { get; set; }
		[ForeignKey(("AreaId"))]
		[ValidateNever]
		public FolkloreArea FolkloreArea { get; set; }

		public int GenderId { get; set; }
		[ForeignKey(("GenderId"))]
		[ValidateNever]
		public GenderClothes GenderClothes { get; set; }
		
		
	}
}