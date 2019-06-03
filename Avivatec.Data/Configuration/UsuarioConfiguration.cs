using Avivatec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avivatec.Data.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> modelBuilder)
        {
            modelBuilder.ToTable("Usuario");

            modelBuilder
                .Property(a => a.IdUsuario)
                .HasColumnName("IDUSUARIO");

            modelBuilder
                .Property(a => a.Nome)
                .HasColumnName("NOME");

            modelBuilder
                .Property(a => a.Sobrenome)
                .HasColumnName("SOBRENOME");

            modelBuilder
                .Property(a => a.Login)
                .HasColumnName("LOGIN");

            modelBuilder
                .Property(a => a.Email)
                .HasColumnName("EMAIL");

            modelBuilder
                .Property(a => a.Senha)
                .HasColumnName("SENHA");

            modelBuilder
                .Property(a => a.DataCadastro)
                .HasColumnName("DATACADASTRO");

            modelBuilder
                .Property(a => a.DataAtualizacao)
                .HasColumnName("DATAATUALIZACAO");

            modelBuilder
                .Property(a => a.Ativo)
                .HasColumnName("ATIVO");

            


        }
    }
}
