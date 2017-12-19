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

        public void Inserir(Evento evento, TorneioContext contexto)
        {
            _eventoDAL.Inserir(evento, contexto);
        }

        public void Atualizar(TorneioContext contexto, Evento evento)
        {
            _eventoDAL.Atualizar(contexto, evento);
        }

        public Evento ConsultarPorId(TorneioContext contexto, int id)
        {
            return _eventoDAL.ConsultarPorId(contexto, id);
        }

        public List<Evento> ConsultarTodos(TorneioContext contexto)
        {
            var lista = new List<Evento>();
            lista = _eventoDAL.ConsultarTodos(contexto);

            return lista;
        }

        public void Excluir(TorneioContext contexto, Evento evento)
        {
            evento.DELETED = 1;
            _eventoDAL.Atualizar(contexto, evento);
        }
    }
}
