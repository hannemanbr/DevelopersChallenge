using System.Collections.Generic;
using System.Linq;
using NIBO.Modelo;
using NIBO.DAL.Context;

namespace NIBO.DAL.Data
{
    public class DesafioDAL
    {
        public void Insert(Desafio desafio, TorneioContext context)
        {
            if (desafio != null)
            {
                context.Desafios.Add(desafio);
                context.SaveChanges();
            }
        }

        public List<Desafio> GetAll(TorneioContext context)
        {
            var lista = new List<Desafio>();

            lista = context.Desafios.Where(x => x.DELETED ==0).ToList();

            return lista;
        }

        public Desafio GetByID(TorneioContext context, int id)
        {
            return context.Desafios.First(x => x.Id == id && x.DELETED==0);
        }

        public void Update(TorneioContext context, Desafio desafio)
        {
            context.Desafios.Update(desafio);
            context.SaveChanges();
        }

        // Get all Teams by Challenger Id
        public List<Equipe> GetEquipeByDesafio(TorneioContext context, int idEvento)
        {
            var lista = from eq in context.Equipes
                        join df in context.Desafios.Where(x => x.IdEvento == idEvento) on eq.Id equals df.IdEvento
                        select new Equipe
                        {
                            Nome = eq.Nome,
                            Id = eq.Id,
                            DELETED = 0
                        };


            return lista.ToList();
        }

        // Get Challenger Id Events 
        public List<Desafio> GetDesafiosByEvento(TorneioContext context, int idEvento)
        {
            return context.Desafios.Where(x => x.IdEvento == idEvento).ToList();
        }

    }
}
