using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MagicOFBulgarian.Models
{
    public class FolkloreArea
    {
        [Key]
        public int AreaId { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Категория Дрехи")]
        public string AreaName { get; set; }

        [DisplayName("Пореден номер")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
