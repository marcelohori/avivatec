using Avivatec.Business.Utils;
using Avivatec.Data.Context;
using Avivatec.Data.Uow;
using Avivatec.Domain.Entities;
using Avivatec.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avivatec.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>,IUsuarioRepository
    {
        public UsuarioRepository(AvivatecDbContext context) : base(context)
        {
        }

        public async Task<Usuario> AuthenticateUser(string login, string password)
        {
            return (await CustomFind(
                    x => x.Login.Equals(login) && x.Senha.Equals(password))
                )
                .FirstOrDefault();
        }



        public async Task<Usuario> ValidaLogin(Usuario usuario) =>
           (await CustomFind(x => (x.Login.ToUpper().Equals(usuario.Login.ToUpper())) &&
                                  x.Senha.Equals(usuario.Senha)
                                  )).FirstOrDefault();


        public async Task<Usuario> ValidaEmail(Usuario usuario)
        {
            var result = (await CustomFind(x => (x.Email.ToUpper().Equals(usuario.Email.ToUpper())))).FirstOrDefault();
            return result;
        }

        public async Task<(Usuario usuario, string messageReturning)> UpdateUser(Usuario usuario)
        {
            var user = (await CustomFind(x => x.IdUsuario == usuario.IdUsuario)).First();

            if (user == null)
                throw new Exception("Usuario não encontrado");



            if (!string.IsNullOrEmpty(usuario.Senha))
                user.Senha = Crypto.Encrypt(usuario.Senha, Crypto.Key256, 256);
            user.Nome = usuario.Nome;
            user.Sobrenome = usuario.Sobrenome;
            user.Email = usuario.Email;
            user.Login = usuario.Login;
            user.DataAtualizacao = usuario.DataAtualizacao;
            user.DataCadastro = usuario.DataCadastro;
            user.Ativo = usuario.Ativo;


            return (usuario, "Usuario atualizado com sucesso!");
        }
    }
}
