namespace SistemaGestionEnvios.Models
{
    public class Envio
    {
		public int id_envio {  get; set; }		
        public String fecha_registro_envio { get; set; }
		public PaqueteCategoria refPaqCate { get; set; }
		public int id_paq_cate { get; set; }
        public EstadoEnvio refEstadoEnvio { get; set; }
        public int id_estado_envio { get; set; }
        public decimal peso_paq_KG { get; set; }
		public decimal costo_envio { get; set; }
		public string nom_remitente { get; set; }
		public string dni_remitente { get; set; }
		public string telf_remitente { get; set; }
		public string nom_destinatario { get; set; }
		public string dni_destinatario { get; set; }
		public string telf_destinatario { get; set; }
		public string direccion_destinatario { get; set; }
		public Distrito refDistrito { get; set; }
        public int id_distrito { get; set; }
        public Usuario refUsuario { get; set; }	
        

		public string mostrarValores()
		{
            return "id_envio: " + id_envio + " fecha_registro_envio: " + fecha_registro_envio +
                " id_paq_cate: " + id_paq_cate + " id_estado_envio: " + id_estado_envio +
                " peso_paq_KG: " + peso_paq_KG + " costo_envio: " + costo_envio +
                " nom_remitente: " + nom_remitente + " dni_remitente: " + dni_remitente +
                " telf_remitente: " + telf_remitente + " nom_destinatario: " + nom_destinatario +
                " dni_destinatario: " + dni_destinatario + " telf_destinatario: " + telf_destinatario +
                " direccion_destinatario: " + direccion_destinatario + " id_distrito: " + id_distrito;
        }
    }
	

}

