using System;
using System.Collections.Generic;

namespace TpCursada.Dominio
{
    public partial class Historical
    {
        public int Id { get; set; }
        public int? IdProducto { get; set; }
        public int? IdCoproducto { get; set; }
        public double? Score { get; set; }

        public virtual Product? IdCoproductoNavigation { get; set; }
        public virtual Product? IdProductoNavigation { get; set; }
    }
}
