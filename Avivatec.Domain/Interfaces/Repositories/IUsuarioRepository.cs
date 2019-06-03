using Avivatec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avivatec.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> AuthenticateUser(string login, string senha);
        Task<Usuario> ValidaLogin(Usuario usuario);
        Task<Usuario> ValidaEmail(Usuario usuario);
        Task<(Usuario usuario, string messageReturning)> UpdateUser(Usuario usuario);
    }
}
