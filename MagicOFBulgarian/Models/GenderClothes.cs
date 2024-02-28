using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MagicOFBulgarian.Models
{
    public class GenderClothes
    {
        [Key]
        public int GenderId { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Пол")]
        public string GenderName { get; set; }
    }
}
