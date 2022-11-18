using System;
using System.Collections.Generic;

namespace Ventasv2.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Ventum>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
