using AutoMapper.Configuration;
using Avivatec.Business.Dto;
using Avivatec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avivatec.Business.Mapping
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(x => x.Senha, o => o.Ignore())

                .ReverseMap()

                .ForMember(x => x.Senha, o => o.Ignore());
        }
    }
}
