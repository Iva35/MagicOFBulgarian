using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MagicOFBulgarian.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Категория Дрехи")]
        public string CategoryName { get; set; }

        [DisplayName("Пореден номер")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        [DefaultValue(1)]
        public int DisplayOrder { get; set; }
    }
}
