using System;
using System.Collections.Generic;

namespace TpCursada.Dominio
{
    public partial class Historical
    {
        public int Id { get; set; }
        public int? IdProducto { get; set; }
        public int? IdCoproducto { get; set; }
        public int? Puntaje { get; set; }
    }
}
