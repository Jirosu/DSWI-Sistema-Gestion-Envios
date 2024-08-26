using SistemaGestionEnvios.Models;
using SistemaGestionEnvios.Repository.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestionEnvios.Repository.Implementacion
{
    public class EstadoEnvioRepository : IGenericRepository<EstadoEnvio>
    {
        private readonly string _cadenaSQL = "";

        public EstadoEnvioRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }

        public async Task<List<EstadoEnvio>> Listar()
        {
            List<EstadoEnvio> lista = new List<EstadoEnvio>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_ListarEstadoEnvios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new EstadoEnvio
                        {
                            id_estado_envio = Convert.ToInt32(dr["id_estado_envio"]),
                            descrip_estado_envio = dr["descrip_estado_envio"].ToString(),
                        });
                    }
                }
            }
            return lista;
        }
        public EstadoEnvio BuscarId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(EstadoEnvio modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(EstadoEnvio modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
