using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Avivatec.Data.Context;
using Avivatec.Domain.Entities;
using Avivatec.Business.Interfaces;
using Avivatec.Business.Dto;
using Avivatec.Domain.UoW;
using Avivatec.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace Avivatec.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _iusuariorepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioController(IConfiguration configuration, IUsuarioRepository iusuariorepository, IUnitOfWork unitOfWork) 
        {
            _iusuariorepository = iusuariorepository;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios(int userId)
        {
            try
            {
                var user = await _iusuariorepository.GetAll();
                
                if (user == null)
                    return NotFound();

                return Ok(user);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


       
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarUsuario(int id)
        {
            try
            {
                var user = await _iusuariorepository.GetById(id);


                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody]Usuario usuario)
        {
            try
            { 
                var result_email = (await _iusuariorepository.ValidaEmail(usuario));
                
                if (result_email != null)
                {
                    return BadRequest("E-mail já está cadastrado no sistema");
                }

                var result_login = (await _iusuariorepository.ValidaLogin(usuario));
                if (result_login != null)
                {
                    return BadRequest("Login já está cadastrado no sistema");
                }

                if(result_email == null && result_login == null)
                {
                    usuario.DataCadastro = DateTime.Now;
                    usuario.DataAtualizacao = DateTime.Now;

                    _iusuariorepository.Save(usuario);

                    await _unitOfWork.CommitAsync();

                    return Ok(usuario);

                }
                return BadRequest("Ocorreu um erro ao processar o cadastro");

            }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

        }

        
        [HttpPut("{id}")]
       
            public async Task<IActionResult> UpdateUser([FromForm] Usuario usuario)
        {
            try
            {
                var result_email = (await _iusuariorepository.ValidaEmail(usuario));
                var result_login = (await _iusuariorepository.ValidaLogin(usuario));
                if (result_email != null)
                {
                    return BadRequest("E-mail já está cadastrado no sistema");
                }

                if (result_login != null)
                {
                    return BadRequest("Login já está cadastrado no sistema");
                }

                else
                {
                    usuario.IdUsuario = usuario.IdUsuario;
                    usuario.DataCadastro = DateTime.Now;
                    usuario.DataAtualizacao = DateTime.Now;

                    var result = await _iusuariorepository.UpdateUser(usuario);

                    await _unitOfWork.CommitAsync();

                    return Ok(new { message = result.messageReturning, result.usuario });

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario([FromBody] Usuario usuario)
        {
            try
            {
               
                 _iusuariorepository.Delete(usuario);

                 _unitOfWork.CommitAsync();

                return Ok("Usuario deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
