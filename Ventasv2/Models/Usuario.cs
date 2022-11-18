using System;
using System.Collections.Generic;

namespace Ventasv2.Models
{
    public partial class Usuario
    {
        public long Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
