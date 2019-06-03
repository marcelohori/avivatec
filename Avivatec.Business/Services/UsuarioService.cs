using AutoMapper;
using Avivatec.Business.Dto;
using Avivatec.Business.Interfaces;
using Avivatec.Business.Utils;
using Avivatec.Domain.Interfaces.Repositories;
using Avivatec.Domain.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Avivatec.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsuarioService( IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
           
        }
        public async Task<UsuarioDto> GetUserById(int id)
        {
            var usuario = (await _usuarioRepository.CustomFind(x => x.IdUsuario == id)).FirstOrDefault();
            var usuarioDto = new UsuarioDto();

            _mapper.Map(usuario, usuarioDto);
            
            return usuarioDto;
        }


        public async Task<(UsuarioDto usuario, string messageReturning)> UpdateUser(UsuarioDto usuarioDto)
        {
            var user = (await _usuarioRepository.CustomFind(x => x.IdUsuario == usuarioDto.IdUsuario)).First();

            if (user == null)
                throw new Exception("Usuario não encontrado");

          

            if (!string.IsNullOrEmpty(usuarioDto.Senha))
            user.Senha = Crypto.Encrypt(usuarioDto.Senha, Crypto.Key256, 256);
            user.Nome = usuarioDto.Nome;
            user.Sobrenome = usuarioDto.Sobrenome;
            user.Email = usuarioDto.Email;
            user.Login = usuarioDto.Login;
            user.DataAtualizacao = usuarioDto.DataAtualizacao;
            user.DataCadastro = usuarioDto.DataCadastro;
            user.Ativo = usuarioDto.Ativo;


            await _unitOfWork.CommitAsync();

            _mapper.Map(user, usuarioDto);

            return (usuarioDto, "Usuario atualizado com sucesso!");
        }

    }
}
