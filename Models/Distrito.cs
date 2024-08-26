namespace SistemaGestionEnvios.Models
{
    public class Distrito
    {
        public int id_distrito { get; set; }
        public string nombre_dist { get; set; }
        public Tarifa refTarifa { get; set; }
    }
}
