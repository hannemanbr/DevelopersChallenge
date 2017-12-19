using System;
using Microsoft.EntityFrameworkCore;
using NIBO.Modelo;

namespace NIBO.DAL.Data
{
    public class TorneioBkpContext : DbContext
    {
        public TorneioBkpContext(DbContextOptions<TorneioBkpContext> options) : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Desafio> Desafios { get; set; }
    }
}
