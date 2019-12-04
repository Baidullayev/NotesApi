using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gala.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(256)]
        public string Username { get; set; }
        [Required, MaxLength(256)]
        public string Lastrname { get; set; }
        [Required, MaxLength(256)]
        public string Firstname { get; set; }
        [Required, MaxLength(256)]        
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
