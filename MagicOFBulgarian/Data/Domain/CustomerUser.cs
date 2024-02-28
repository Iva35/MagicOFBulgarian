using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MagicOFBulgarian.Data.Domain
{
    public class CustomerUser:IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        
        [MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(10)]
        public string EGN { get; set; }
    }
}
