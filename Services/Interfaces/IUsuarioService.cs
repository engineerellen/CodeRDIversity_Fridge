using Domain.Resources;

namespace Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResource> Registro(RegistroResource resource, CancellationToken cancellationToken);

        Task<UsuarioResource> Login(LoginResource resource, CancellationToken cancellationToken);
    }
}