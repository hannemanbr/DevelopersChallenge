using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;

namespace NIBO.DAL.Context
{
    public class TorneioContextFactory : IDesignTimeDbContextFactory<TorneioContext> //IDbContextFactory<TorneioContext>
    {
        //TorneioContext IDesignTimeDbContextFactory<TorneioContext>.CreateDbContext(string[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public TorneioContext Create(DbContextFactoryOptions options)
        //{
        //    var builder = new DbContextOptionsBuilder<TorneioContext>();
        //    builder.UseMySql(
        //        "Server=(localdb)\\mssqllocaldb;Database=config;Trusted_Connection=True;MultipleActiveResultSets=true");

        //    return new TorneioContext(builder.Options);
        //}

        public TorneioContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TorneioContext>();

            builder.UseMySql(
                "Server=localhost;DataBase=NiboDB;Uid=root;Pwd="
            );

            return new TorneioContext(builder.Options);
        }
    }
}
