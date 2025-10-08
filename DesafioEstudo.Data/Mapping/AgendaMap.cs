using DesafioEstudo.Dominio.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstudo.Data.Mapping
{
    public class AgendaMap : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.ToTable("tb_Agenda");

            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id)
                 .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Titulo)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            builder.Property(x => x.DataInicio)
               .HasColumnType("DATETIME")
               .IsRequired();

            builder.Property(x => x.DataFim)
              .HasColumnType("DATETIME")
              .IsRequired();

            builder.Property(x => x.enumSituacao)
               .HasConversion<string>() 
               .HasColumnType("VARCHAR(50)") 
               .HasColumnName("Situacao")    
               .IsRequired(false);

            builder.Property(x => x.Descricao)
              .HasColumnType("VARCHAR(MAX)")
              .IsRequired();

        }
    }
}
