using MagicOFBulgarian.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace MagicOFBulgarian.Models
{
    public class UserAddresses { 


        [Key]
        public int Id { get; set; }


        [Required] 
        public string CustomerID { get; set; }
       
        
        [Required]
        public string UserAddressId { get; set; }
    }
}
