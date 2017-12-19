using System;
using Microsoft.EntityFrameworkCore;
using NIBO.Modelo;

namespace NIBO.DAL.Context
{
    public class TorneioContext : DbContext
    {
        public TorneioContext(DbContextOptions<TorneioContext> options) : base(options){ }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Desafio> Desafios { get; set; }

    }
}
