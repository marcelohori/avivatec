using Avivatec.Business.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avivatec.Business.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> GetUserById(int id);
        Task<(UsuarioDto usuario, string messageReturning)> UpdateUser(UsuarioDto userDto);
    }
}
