using SistemaGestionEnvios.Models;
using SistemaGestionEnvios.Repository.Contrato;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SistemaGestionEnvios.Repository.Implementacion
{
    public class EnvioRepository : IGenericRepository<Envio>
    {
        private readonly string _cadenaSQL = "";

        public EnvioRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }


        public async Task<List<Envio>> Listar()
        {
            List<Envio> lista = new List<Envio>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_ListarEnvios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Envio
                        {
                            id_envio = Convert.ToInt32(dr["id_envio"]),
                            fecha_registro_envio = dr["fecha_registro_envio"].ToString(),
                            refEstadoEnvio = new EstadoEnvio
                            {
                                id_estado_envio = Convert.ToInt32(dr["id_estado_envio"]),
                                descrip_estado_envio = dr["descrip_estado_envio"].ToString(),
                            },
                            peso_paq_KG = Convert.ToDecimal(dr["peso_paq_KG"]),
                            direccion_destinatario = dr["direccion_destinatario"].ToString(),
                            refDistrito = new Distrito
                            {
                                id_distrito = Convert.ToInt32(dr["id_distrito"]),
                                nombre_dist = dr["nombre_dist"].ToString(),                                
                            }
                        });
                    }
                }
            }
            return lista;
        }


        //public async Task<Envio> BuscarId(int id)
        public Envio BuscarId(int id)
        {
            Envio envio = null;
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_BuscarEnvioId", conexion);
                cmd.Parameters.AddWithValue("idEnvioBuscar", id);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                
                if ( dr.Read())
                {
                    envio = new Envio();

                    envio.id_envio = Convert.ToInt32(dr["id_envio"]);
                    envio.id_paq_cate = Convert.ToInt32(dr["id_paq_cate"]);
                    envio.refPaqCate = new PaqueteCategoria
                    {
                        id_paq_cate = Convert.ToInt32(dr["id_paq_cate"]),
                        descrip_paq_cate = dr["descrip_paq_cate"].ToString()
                    };
                    envio.peso_paq_KG = Convert.ToDecimal(dr["peso_paq_KG"]);
                    envio.direccion_destinatario = dr["direccion_destinatario"].ToString();
                    envio.id_distrito = Convert.ToInt32(dr["id_distrito"]);
                    envio.refDistrito = new Distrito
                    {
                        id_distrito = Convert.ToInt32(dr["id_distrito"]),
                        nombre_dist = dr["nombre_dist"].ToString(),
                        refTarifa = new Tarifa
                        {
                            id_tarifa = Convert.ToInt32(dr["id_tarifa"]),
                            precio_x_KG = Convert.ToDecimal(dr["precio_x_KG"]),
                        }
                    };
                    envio.costo_envio = Convert.ToDecimal(dr["costo_envio"]);
                    envio.id_estado_envio = Convert.ToInt32(dr["id_estado_envio"]);
                    envio.refEstadoEnvio = new EstadoEnvio
                    {
                        id_estado_envio = Convert.ToInt32(dr["id_estado_envio"]),
                        descrip_estado_envio = dr["descrip_estado_envio"].ToString()
                    };
                    envio.nom_remitente = dr["nom_remitente"].ToString();
                    envio.dni_remitente = dr["dni_remitente"].ToString();
                    envio.telf_remitente = dr["telf_remitente"].ToString();
                    envio.nom_destinatario = dr["nom_destinatario"].ToString();
                    envio.dni_destinatario = dr["dni_destinatario"].ToString();
                    envio.telf_destinatario = dr["telf_destinatario"].ToString();                    
                }
            }
            return envio;
        }

        public async Task<bool> Guardar(Envio modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_GrabarEnvio", conexion);
                cmd.Parameters.AddWithValue("fecha_registro_envio", modelo.fecha_registro_envio);
                cmd.Parameters.AddWithValue("id_paq_cate", modelo.id_paq_cate);
                cmd.Parameters.AddWithValue("id_estado_envio", modelo.id_estado_envio);
                cmd.Parameters.AddWithValue("peso_paq_KG", modelo.peso_paq_KG);
                cmd.Parameters.AddWithValue("costo_envio", modelo.costo_envio);
                cmd.Parameters.AddWithValue("nom_remitente", modelo.nom_remitente);
                cmd.Parameters.AddWithValue("dni_remitente", modelo.dni_remitente);
                cmd.Parameters.AddWithValue("telf_remitente", modelo.telf_remitente);
                cmd.Parameters.AddWithValue("nom_destinatario", modelo.nom_destinatario);
                cmd.Parameters.AddWithValue("dni_destinatario", modelo.dni_destinatario);
                cmd.Parameters.AddWithValue("telf_destinatario", modelo.telf_destinatario);
                cmd.Parameters.AddWithValue("direccion_destinatario", modelo.direccion_destinatario);
                cmd.Parameters.AddWithValue("id_distrito", modelo.id_distrito);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }            
        }

        public async Task<bool> Editar(Envio modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_EditarEnvio", conexion);
                cmd.Parameters.AddWithValue("id_envio", modelo.id_envio);
                cmd.Parameters.AddWithValue("id_paq_cate", modelo.id_paq_cate);
                cmd.Parameters.AddWithValue("id_estado_envio", modelo.id_estado_envio);
                cmd.Parameters.AddWithValue("peso_paq_KG", modelo.peso_paq_KG);
                cmd.Parameters.AddWithValue("costo_envio", modelo.costo_envio);
                cmd.Parameters.AddWithValue("nom_remitente", modelo.nom_remitente);
                cmd.Parameters.AddWithValue("dni_remitente", modelo.dni_remitente);
                cmd.Parameters.AddWithValue("telf_remitente", modelo.telf_remitente);
                cmd.Parameters.AddWithValue("nom_destinatario", modelo.nom_destinatario);
                cmd.Parameters.AddWithValue("dni_destinatario", modelo.dni_destinatario);
                cmd.Parameters.AddWithValue("telf_destinatario", modelo.telf_destinatario);
                cmd.Parameters.AddWithValue("direccion_destinatario", modelo.direccion_destinatario);
                cmd.Parameters.AddWithValue("id_distrito", modelo.id_distrito);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_EliminarEnvio", conexion);
                cmd.Parameters.AddWithValue("id_envio", id);

                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }
        


    }
}
