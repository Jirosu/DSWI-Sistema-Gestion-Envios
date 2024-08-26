using SistemaGestionEnvios.Models;
using SistemaGestionEnvios.Repository.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestionEnvios.Repository.Implementacion
{
    public class DistritoRepository : IGenericRepository<Distrito>
    {
        private readonly string _cadenaSQL = "";

        public DistritoRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }


        public async Task<List<Distrito>> Listar()
        {
            List<Distrito> lista = new List<Distrito>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_ListarDistritos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())                
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Distrito
                        {
                            id_distrito = Convert.ToInt32(dr["id_distrito"]),
                            nombre_dist = dr["nombre_dist"].ToString(),
                            refTarifa = new Tarifa()
                            {
                                id_tarifa = Convert.ToInt32(dr["id_tarifa"]),
                                precio_x_KG = Convert.ToDecimal(dr["precio_x_KG"]),
                            }
                        });
                    }
                }
            }
            return lista;
        }

        //public Task<Distrito> BuscarId(int id)
        public Distrito BuscarId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Guardar(Distrito modelo)
        {
            /*
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("FALTA CREAR SP", conexion);
                cmd.Parameters.AddWithValue("FALTA NOMBRE PARAMETRO EM SP", modelo.nombre_dist);
                cmd.Parameters.AddWithValue("FALTA NOMBRE PARAMETRO EM SP", modelo.refTarifa.id_tarifa);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }*/
            throw new NotImplementedException();
        }

        public async Task<bool> Editar(Distrito modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
            }

            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
            }
            throw new NotImplementedException();
        }
    }
}
