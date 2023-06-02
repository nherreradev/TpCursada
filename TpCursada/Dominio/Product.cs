using System;
using System.Collections.Generic;

namespace TpCursada.Dominio
{
    public partial class Product
    {
        public Product()
        {
            HistoricalIdCoproductoNavigations = new HashSet<Historical>();
            HistoricalIdProductoNavigations = new HashSet<Historical>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Imagen { get; set; }
        public decimal? Precio { get; set; }

        public virtual ICollection<Historical> HistoricalIdCoproductoNavigations { get; set; }
        public virtual ICollection<Historical> HistoricalIdProductoNavigations { get; set; }
    }
}
