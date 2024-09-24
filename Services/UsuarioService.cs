using Domain.Models;
using Domain.Resources;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using Services.Interfaces;

namespace Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly string _pepper;
        private readonly int _iteration = 3;
        private readonly IConfiguration _config;

        public UsuarioService(IUsuarioRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
            _pepper = _config["Hash:pepper"];
        }

        public async Task<UsuarioResource> Registro(RegistroResource resource, CancellationToken cancellationToken)
        {
            var usuario = new Usuario
            {
                Nome = resource.Nome,
                Email = resource.Email,
                SenhaSalt = SenhaHash.GenerateSalt()
            };

            usuario.SenhaHash = SenhaHash.ComputeHash(resource.Senha, usuario.SenhaSalt, _pepper, _iteration);

            await _repository.AdicionarUsuarioAsync(usuario, cancellationToken);

            return new UsuarioResource(usuario.Id, usuario.Nome, usuario.Email);
        }

        public async Task<UsuarioResource> Login(LoginResource resource, CancellationToken cancellationToken)
        {
            var usuario = await _repository.GetUsusarioAsync(resource, cancellationToken);

            if (usuario == null)
                throw new Exception("Nome e senha não conferem.");

            var passwordHash = SenhaHash.ComputeHash(resource.Senha, usuario?.SenhaSalt, _pepper, _iteration);

            if (usuario.SenhaHash != passwordHash)
                throw new Exception("Nome e senha não conferem.");

            return new UsuarioResource(usuario.Id, usuario?.Nome, usuario?.Email);
        }
    }
}