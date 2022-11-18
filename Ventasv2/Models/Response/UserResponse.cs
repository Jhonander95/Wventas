using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ventasv2.Models.Response
{
    public class UserResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
