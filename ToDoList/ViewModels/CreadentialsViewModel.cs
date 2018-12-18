using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ToDoList.ViewModels
{
    public class CredentialsViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6)]
        [MaxLength(12)]
        public string Password { get; set; }
    }
}
