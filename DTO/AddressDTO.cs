using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AddressDTO
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Please fill in the address")]
        public string AddressContent { get; set; }
        
        [Required(ErrorMessage = "Please fill in the email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please fill in the phonenumber")]    
        public string Phone { get; set; }
        
        public string Phone2 { get; set; }     
    }
}
