using System.Collections.Generic;
using System.Linq;
using NIBO.Modelo;
using NIBO.DAL.Context;

namespace NIBO.DAL.Data
{
    public class EventoDAL
    {

        public void Inserir(Evento evento, TorneioContext contexto)
        {
            if (evento != null)
            {
                contexto.Eventos.Add(evento);
                contexto.SaveChanges();
            }
        }

        public List<Evento> ConsultarTodos(TorneioContext contexto)
        {
            var lista = new List<Evento>();

            if (contexto.Eventos != null) lista = contexto.Eventos.Where(x => x.DELETED == 0).ToList();

            return lista;
        }

        public Evento ConsultarPorId(TorneioContext contexto, int id)
        {
            return contexto.Eventos.First(x => x.Id == id && x.DELETED==0);
        }

        public void Atualizar(TorneioContext contexto, Evento evento){
            contexto.Eventos.Update(evento);
            contexto.SaveChanges();
        }
    }
}
