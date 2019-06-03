using Avivatec.Data.Repositories;
using Avivatec.Data.Uow;
using Avivatec.Domain.Interfaces.Repositories;
using Avivatec.Domain.UoW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avivatec.CrossCutting.IoC.ScopeInjectors
{
    public static class RepositoryScopeInjector
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            
        }
    }
}
