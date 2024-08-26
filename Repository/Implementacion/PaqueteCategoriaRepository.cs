using SistemaGestionEnvios.Models;
using SistemaGestionEnvios.Repository.Contrato;
using System.Data.SqlClient;
using System.Data;

namespace SistemaGestionEnvios.Repository.Implementacion
{
    public class PaqueteCategoriaRepository : IGenericRepository<PaqueteCategoria>
    {
        private readonly string _cadenaSQL = "";

        public PaqueteCategoriaRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }


        public async Task<List<PaqueteCategoria>> Listar()
        {
            List<PaqueteCategoria> lista = new List<PaqueteCategoria>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_ListarPaqueteCategorias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new PaqueteCategoria
                        {
                            id_paq_cate = Convert.ToInt32(dr["id_paq_cate"]),
                            descrip_paq_cate = dr["descrip_paq_cate"].ToString(),                                                       
                        });
                    }
                }
            }
            return lista;
        }

        //public Task<PaqueteCategoria> BuscarId(int id)
        public PaqueteCategoria BuscarId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(PaqueteCategoria modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(PaqueteCategoria modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
