using System.Collections.Generic;
using NIBO.DAL.Context;
using NIBO.DAL.Data;
using NIBO.Dominio.Interface;
using NIBO.Modelo;

namespace NIBO.Dominio.Infra
{
    public class EventoInfra : iEvento
    {
        private EventoDAL _eventoDAL = new EventoDAL();

        public void Insert(Evento evento, TorneioContext contexto)
        {
            _eventoDAL.Insert(evento, contexto);
        }

        public void Update(TorneioContext contexto, Evento evento)
        {
            _eventoDAL.Atualizar(contexto, evento);
        }

        public Evento GetByID(TorneioContext contexto, int id)
        {
            return _eventoDAL.GetByID(contexto, id);
        }

        public List<Evento> GetAll(TorneioContext contexto)
        {
            var lista = new List<Evento>();
            lista = _eventoDAL.GetAll(contexto);

            return lista;
        }

        public void Delete(TorneioContext contexto, Evento evento)
        {
            evento.DELETED = 1;
            _eventoDAL.Atualizar(contexto, evento);
        }
    }
}
