using System.Collections.Generic;
using System.Linq;
using NIBO.Modelo;
using NIBO.DAL.Context;

namespace NIBO.DAL.Data
{
    public class DesafioDAL
    {
        public void Inserir(Desafio desafio, TorneioContext contexto)
        {
            if (desafio != null)
            {
                contexto.Desafios.Add(desafio);
                contexto.SaveChanges();
            }
        }

        public List<Desafio> ConsultarTodos(TorneioContext contexto)
        {
            var lista = new List<Desafio>();

            lista = contexto.Desafios.Where(x => x.DELETED ==0).ToList();

            return lista;
        }

        public Desafio ConsultarPorId(TorneioContext contexto, int id)
        {
            return contexto.Desafios.First(x => x.Id == id && x.DELETED==0);
        }

        public void Atualizar(TorneioContext contexto, Desafio desafio)
        {
            contexto.Desafios.Update(desafio);
            contexto.SaveChanges();
        }

        public List<Equipe> ConsultarEquipesPorDesafio(TorneioContext contexto, int idEvento)
        {
            var lista = from eq in contexto.Equipes
                        join df in contexto.Desafios.Where(x => x.IdEvento == idEvento) on eq.Id equals df.IdEvento
                        select new Equipe
                        {
                            Nome = eq.Nome,
                            Id = eq.Id,
                            DELETED = 0
                        };


            return lista.ToList();
        }

        public List<Desafio> consultarDesafiosPorEvento(TorneioContext contexto, int idEvento)
        {
            return contexto.Desafios.Where(x => x.IdEvento == idEvento).ToList();
        }

    }
}
