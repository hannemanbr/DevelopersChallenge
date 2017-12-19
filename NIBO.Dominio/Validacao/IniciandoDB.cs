using System.Linq;
using NIBO.DAL.Context;
using NIBO.Modelo;

namespace NIBO.Dominio.Validacao
{
    public class IniciandoDB
    {
        public static void Initialize(TorneioContext contexto)
        {

            contexto.Database.EnsureCreated();

            if (contexto.Eventos.Any())
            {
                return;
            }

            var eventos = new Evento[]
            {
                new Evento{ Nome="Evento teste 1"},
                new Evento{ Nome="Evento teste 12"},
                new Evento{ Nome="Evento teste 13"}
            };

            //foreach (Evento evento in eventos)
            //{
            //    EventoView ev = new EventoView();

            //    ev.Nome = evento.Nome;
            //    contexto.Add(ev);
            //}

            contexto.SaveChanges();
            contexto.Dispose();

        }
    }
}
