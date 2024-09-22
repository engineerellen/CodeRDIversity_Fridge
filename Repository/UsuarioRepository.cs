using Domain.Models;
using Domain.Resources;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interface;

namespace Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GeladeiraContext _context;

        public UsuarioRepository(GeladeiraContext context)
        {
            _context = context;
        }

        public async Task AdicionarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            await _context.Usuarios.AddAsync(usuario, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<Usuario> GetUsusarioAsync(LoginResource resource, CancellationToken cancellationToken)=>
            _context.Usuarios.FirstOrDefaultAsync(x => x.Nome == resource.Nome, cancellationToken);

    }
}