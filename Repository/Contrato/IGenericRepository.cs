using SistemaGestionEnvios.Models;

namespace SistemaGestionEnvios.Repository.Contrato
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Listar();

        T BuscarId(int id);

        Task<bool> Guardar(T modelo);

        Task<bool> Editar(T modelo);

        Task<bool> Eliminar(int id);
    }
}

