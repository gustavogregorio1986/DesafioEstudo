using DesafioEstudo.Data.Mapping;
using DesafioEstudo.Dominio.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstudo.Dominio.Context
{
    public class DesafioEstudoContext : DbContext
    {
        public DesafioEstudoContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Agenda> Agendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
