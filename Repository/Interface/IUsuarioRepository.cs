using Domain.Models;
using Domain.Resources;

namespace Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task AdicionarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken);
        Task<Usuario> GetUsusarioAsync(LoginResource resource, CancellationToken cancellationToken);
    }
}