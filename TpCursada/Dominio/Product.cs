using System;
using System.Collections.Generic;

namespace TpCursada.Dominio
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Imagen { get; set; }
        public decimal? Precio { get; set; }
    }
}
