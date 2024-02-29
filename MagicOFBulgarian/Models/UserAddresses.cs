using MagicOFBulgarian.Data.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicOFBulgarian.Models
{
    public class UserAddresses { 


        [Key]
        public int Id { get; set; }

        public string CustomerID { get; set; }

        [Required]
        [ForeignKey("CustomerID")]
        public  CustomerUser Customer { get; set; }
       
        public int UserAddressId { get; set; }
        [Required]
        [ForeignKey("AddressID")]
        public Address UserAddress { get; set; }
    }
}
