using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gala.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; internal set; }
    }
}
