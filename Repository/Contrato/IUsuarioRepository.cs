namespace SistemaGestionEnvios.Repository.Contrato
{
    public interface IUsuarioRepository<Usuario>
    {
        bool validarCredenciales(string username, string password);
    }
}
