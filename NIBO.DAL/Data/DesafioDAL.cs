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

            if (contexto.Desafios != null) lista = contexto.Desafios.Where(x => x.DELETED ==0).ToList();

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

    }
}
