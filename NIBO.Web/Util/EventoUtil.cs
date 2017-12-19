using System;
using System.Collections.Generic;
using NIBO.Dominio.Infra;
using NIBO.Web.ViewModels;
using NIBO.DAL.Context;
using NIBO.Modelo;

namespace NIBO.Web.Util
{
    public class EventoUtil
    {
        private EventoInfra _eventoInfra = new EventoInfra();

        public List<EventoView> ConsultarEventos(TorneioContext contexto)
        {

            var lista = new List<EventoView>();

            foreach (Evento evento in _eventoInfra.GetAll(contexto))
            {
                lista.Add(
                    new EventoView { Id = evento.Id, Nome = evento.Nome }
                );
            }

            return lista;
        }

        public EventoView ConsultaPorId(int id, TorneioContext contexto)
        {

            var evento = _eventoInfra.GetByID(contexto, id);
            var eventoView = new EventoView
            {
                Id = evento.Id,
                Nome = evento.Nome
            };

            return eventoView;

        }

        public Evento ConversaoParaEvento(EventoView eventoView)
        {

            int IdView = 0;

            if (eventoView.Id > 0) IdView = eventoView.Id;

            var evento = new Evento
            {
                Nome = eventoView.Nome,
                Id = IdView
            };

            return evento;

        }

        public EventoView ConversaoParaEventoViewPorId(int id, TorneioContext _contexto)
        {
            var evento = _eventoInfra.GetByID(_contexto, id);

            var eventoView = new EventoView
            {
                Nome = evento.Nome,
                Id = evento.Id
            };

            return eventoView;

        }
    }
}
