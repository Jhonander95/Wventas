using System;
using System.Collections.Generic;

namespace Ventasv2.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Conceptos = new HashSet<Concepto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal? PrecioUnitario { get; set; }
        public decimal? Costo { get; set; }

        public virtual ICollection<Concepto> Conceptos { get; set; }
    }
}
