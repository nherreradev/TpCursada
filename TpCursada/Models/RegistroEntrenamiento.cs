namespace TpCursada.Models
{
        public class RegistroEntrenamiento
        {
            public string Fecha { get; set; }
            public int RegistrosLeidos { get; set; }
            public Estadisticas Estadisticas { get; set; }
        }

        public class Estadisticas
        {
            public double RMSE { get; set; }
            public double MAE { get; set; }
            public EstadisticasDescriptivas EstadisticasDescriptivas { get; set; }
        }

        public class EstadisticasDescriptivas
        {
            public double Media { get; set; }
            public double DesviacionEstandar { get; set; }
        }

}
