using System.Collections.Generic;
using System.Linq;
using NIBO.Modelo;
using NIBO.DAL.Context;

namespace NIBO.DAL.Data
{
    public class EventoDAL
    {

        public void Insert(Evento evento, TorneioContext context)
        {
            if (evento != null)
            {
                context.Eventos.Add(evento);
                context.SaveChanges();
            }
        }

        public List<Evento> GetAll(TorneioContext context)
        {
            var lista = new List<Evento>();

            if (context.Eventos != null) lista = context.Eventos.Where(x => x.DELETED == 0).ToList();

            return lista;
        }

        public Evento GetByID(TorneioContext context, int id)
        {
            return context.Eventos.First(x => x.Id == id && x.DELETED==0);
        }

        public void Atualizar(TorneioContext context, Evento evento){
            context.Eventos.Update(evento);
            context.SaveChanges();
        }
    }
}
