using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace DTO
{
    public class UserDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please fill in Username")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Please fill in Password")]
        public string Password { get; set; } 
        
        [Required(ErrorMessage = "Please fill in Email")]        
        public string Email { get; set; }

        public string Imagepath { get; set; }

        [Required(ErrorMessage = "Please fill in Name")]
        public string Name { get; set; }

        public bool isAdmin { get; set; }

        [Display(Name = "User Image")]
        public HttpPostedFileBase UserImage { get; set; } 
    }
}
